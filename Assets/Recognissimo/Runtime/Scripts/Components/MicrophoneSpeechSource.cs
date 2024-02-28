using System;
using System.Collections;
using UnityEngine;
using Microphone = Estrada.Microphone;

namespace Recognissimo.Components
{
    /// <summary>
    ///     <see cref="SpeechSource" /> that provides audio data from a microphone.
    /// </summary>
    [AddComponentMenu("Recognissimo/Speech Sources/Microphone Speech Source")]
    public sealed class MicrophoneSpeechSource : SpeechSource
    {
        private const int DefaultMaxRecordingTime = 2;

        private const float DefaultSilenceAdditionDuration = 1.5f;

        [SerializeField]
        private RecordingSettings recordingSettings = new()
        {
            deviceName = null,
            timeSensitivity = 0.25f
        };

        private float[] _buffer;

        private bool _isPaused;

        private int _prevPos;

        private int _recordingLength;

        private AudioClip _clip;

        /// <summary>
        ///     Microphone name. Use null or empty string to use default microphone.
        /// </summary>
        public string DeviceName
        {
            get => recordingSettings.deviceName;
            set => recordingSettings.deviceName = value;
        }

        /// <summary>
        ///     How often audio frames should be submitted to the recognizer (seconds).
        ///     Use smaller values to submit audio samples more often.
        ///     Recommended value is 0.25 seconds.
        /// </summary>
        public float TimeSensitivity
        {
            get => recordingSettings.timeSensitivity;
            set => recordingSettings.timeSensitivity = value;
        }

        /// <summary>
        ///     Whether recording is active.
        /// </summary>
        public bool IsRecording { get; private set; }

        /// <summary>
        ///     Whether recording is paused.
        /// </summary>
        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (_isPaused == value)
                {
                    return;
                }

                if (value && IsRecording)
                {
                    WriteAvailableSamples(true);
                    WriteSilence(DefaultSilenceAdditionDuration);
                }

                _isPaused = value;
            }
        }

        private void Update()
        {
            if (!IsRecording)
            {
                return;
            }

            if (!Microphone.IsRecording(recordingSettings.deviceName))
            {
                OnRuntimeFailure(
                    new RuntimeFailureEventArgs(new SpeechSourceRuntimeException("Cannot access microphone")));
                return;
            }

            WriteAvailableSamples();
        }

        private void OnEnable()
        {
            if (Microphone.RequiresPermission())
            {
                RegisterInitializationTask("Check microphone permissions", CheckMicrophonePermissions,
                    CallCondition.Always);
            }

            RegisterInitializationTask("Detect sample rate", DetectSampleRate,
                CallCondition.ValueChanged(() => DeviceName));

            RegisterInitializationTask("Initialize microphone", InitializeMicrophone,
                CallCondition.Always);
        }

        private IEnumerator CheckMicrophonePermissions()
        {
            yield return Microphone.RequestPermission();

            if (!Microphone.HasPermission())
            {
                FailInitialization(new InvalidOperationException("Permission to use a microphone is denied"));
            }
        }

        private void DetectSampleRate()
        {
            const int minSupportedSampleRate = 16000;
            const int targetSampleRate = 16000;

            Microphone.GetDeviceCaps(DeviceName, out var minFreq, out var maxFreq);

            var supportsAnySampleRate = minFreq == 0 && maxFreq == 0;

            if (supportsAnySampleRate)
            {
                SampleRate = targetSampleRate;
                return;
            }

            if (minFreq < minSupportedSampleRate && maxFreq < minSupportedSampleRate)
            {
                FailInitialization(
                    new InvalidOperationException(
                        "Available sample rates are less than the minimum supported 16000 Hz"));
                return;
            }

            SampleRate = minFreq <= targetSampleRate && targetSampleRate <= maxFreq
                ? targetSampleRate
                : minFreq;
        }

        private IEnumerator InitializeMicrophone()
        {
            if (TimeSensitivity == 0f)
            {
                throw new InvalidOperationException($"{nameof(TimeSensitivity)} must be greater than zero");
            }

            const int secondsReserve = 1;

            var maxRecordingTime = (int) Math.Max(TimeSensitivity + secondsReserve, DefaultMaxRecordingTime);

            _clip = Microphone.Start(DeviceName, true, maxRecordingTime,
                SampleRate);

            _recordingLength = _clip.samples;

            var bufferLength = (int) (TimeSensitivity * SampleRate);

            if (_buffer == null || _buffer.Length != bufferLength)
            {
                _buffer = new float[bufferLength];
            }

            while (Microphone.GetPosition(recordingSettings.deviceName) == 0)
            {
                yield return null;
            }
        }

        private void WriteAvailableSamples(bool greedy = false)
        {
            var currPos = Microphone.GetPosition(recordingSettings.deviceName);

            if (IsPaused)
            {
                _prevPos = currPos;
                return;
            }

            var bufferLength = _buffer.Length;

            var availableSamples = (currPos - _prevPos + _recordingLength) % _recordingLength;

            while (availableSamples >= bufferLength || (greedy && availableSamples > 0))
            {
                if (!GetMicrophoneData())
                {
                    OnRuntimeFailure(
                        new RuntimeFailureEventArgs(new SpeechSourceRuntimeException("Cannot access microphone data")));

                    return;
                }

                var written = Math.Min(availableSamples, bufferLength);

                OnSamplesReady(new SamplesReadyEventArgs(_buffer, written));

                _prevPos = (_prevPos + written) % _recordingLength;

                availableSamples -= written;
            }
        }

        private bool GetMicrophoneData()
        {
            return Application.platform == RuntimePlatform.WebGLPlayer 
                ? Microphone.GetCurrentData(_buffer, _prevPos) 
                : _clip.GetData(_buffer, _prevPos);
        }
        
        private void WriteSilence(float duration)
        {
            if (duration == 0)
            {
                return;
            }

            var bufferLength = _buffer.Length;

            var silenceSamples = Mathf.CeilToInt(duration * SampleRate);

            Array.Fill(_buffer, 0);

            while (silenceSamples > 0)
            {
                OnSamplesReady(new SamplesReadyEventArgs(_buffer, _buffer.Length));
                silenceSamples -= bufferLength;
            }
        }

        /// <inheritdoc />
        public override void StartProducing()
        {
            IsRecording = true;

            _prevPos = Microphone.GetPosition(recordingSettings.deviceName);
        }

        /// <inheritdoc />
        public override void StopProducing()
        {
            IsRecording = false;

            Microphone.End(recordingSettings.deviceName);
        }

        /// <summary>
        ///     Lists available microphone names.
        /// </summary>
        /// <returns>Available devices.</returns>
        public string[] Devices()
        {
            return Microphone.devices;
        }

        [Serializable]
        private struct RecordingSettings
        {
            [Tooltip("Microphone name. Leave empty to use default microphone.")]
            public string deviceName;

            [Tooltip("How often audio frames should be submitted to the recognizer (seconds). " +
                     "Use smaller values to submit audio samples more often. ")]
            public float timeSensitivity;
        }
    }
}
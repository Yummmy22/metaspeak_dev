using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.Common.AudioRecorder
{
	public class MicRecorderManager : MonoBehaviour
	{
		private const string TAG = "[MicRecorderManager]: ";
		private int _pintBufferSize;
		private int _pintNumBuffers;
		private int _outputRate = 44100;
		private string _filepath;
		private int _headerSize = 44; //default for uncompressed wa
		private float _startTime;
		
		private AudioClip _recording;
		private bool _isPlaying;

		// Audio Source
		public TextMeshProUGUI StatusText;
		public TextMeshProUGUI RecordButtonText;
		public TextMeshProUGUI PlayButtonText;
		public Button RecordButton;
		public Button PlayButton;
		
		private AudioSource _audioSource;
		private bool _isRecording;
		private UtilsPlugin _utilsPlugin;
		private bool _isAudioClipLoaded;
		private AudioClip _loadedAudioClip;

		private void Awake()
		{
			_utilsPlugin = UtilsPlugin.GetInstance();
			_utilsPlugin.Init();
			_utilsPlugin.SetDebug(0);

			AudioConfiguration configuration = AudioSettings.GetConfiguration();
			configuration.sampleRate = _outputRate;
		}

		// Start is called before the first frame update
		void Start()
		{
			EnableDisablePlayButton(false);
			_audioSource = gameObject.GetComponent<AudioSource>();
		}

		public void ClickStartStopRecord()
		{
			if (!_isRecording)
			{
				ToggleRecord();
				StartRecord();
			}
			else
			{
				ToggleRecord();
				StopRecord();
			}
		}

		private void ToggleRecord()
		{
			_isRecording = !_isRecording;
			RecordButtonText.SetText((_isRecording) ? "Stop" : "Record");
		}
		
		private void TogglePlay()
		{
			_isPlaying = !_isPlaying;
			PlayButtonText.SetText((_isPlaying) ? "Stop" : "Play");
		}

		public void StartRecord()
		{
			EnableDisablePlayButton(false);
			_isAudioClipLoaded = false;
			string filename =$"micRecord_{Utils.GetTimeSinceEpoch()}.wav";
#if UNITY_ANDROID && !UNITY_EDITOR
			string folderPath = _utilsPlugin.CreateFolder("AUPAudioRecorded", 1);
			if (!folderPath.Equals("", StringComparison.Ordinal))
			{
				_filepath = folderPath + "/" + filename;
			}
#else
			_filepath = $"{Application.persistentDataPath}/{filename}";
#endif
			
			StatusText.SetText($"Recording at {_filepath}");
			_startTime = Time.time;
			// Call this to start recording. 'null' in the first argument selects the default microphone. Add some mic checking later
			// 5 mins
			_recording = Microphone.Start(null, false, 300, _outputRate);
		}

		private void StopRecord()
		{
			Microphone.End("");
			EnableDisablePlayButton(true);

			//Trim the audioclip by the length of the recording
			AudioClip recordingNew = AudioClip.Create(_recording.name,
				(int) ((Time.time - _startTime) * _recording.frequency), _recording.channels, _recording.frequency,
				false);
			float[] data = new float[(int) ((Time.time - _startTime) * _recording.frequency)];
			_recording.GetData(data, 0);
			recordingNew.SetData(data, 0);
			_recording = recordingNew;
			Save(_filepath, _recording);
		}
		
		private void EnableDisablePlayButton(bool val)
		{
			PlayButton.interactable = val;
		}
		
		private void EnableDisableRecordButton(bool val)
		{
			RecordButton.interactable = val;
		}
		
		public void ClickPlayStop()
		{
			if (!_isPlaying)
			{
				EnableDisableRecordButton(false);
				TogglePlay();
				if (!_isAudioClipLoaded)
				{
					#if UNITY_ANDROID && !UNITY_EDITOR
				        if (!String.IsNullOrEmpty(_filepath))
				        {
							_filepath = $"file://{_filepath}";
				        }
#endif
					LoadAudio(_filepath);
				}
				else
				{
					PlayRecordedAudio();
				}
			}
			else
			{
				TogglePlay();
				_audioSource.Stop();
				EnableDisableRecordButton(true);
			}
		}
		
		private void PlayRecordedAudio()
		{
			//Play _recording
			_audioSource.clip = _loadedAudioClip;
			_audioSource.Play();
		}
		
		private void LoadAudio(String audioFilepath)
		{
			if (!string.IsNullOrEmpty(audioFilepath))
			{
				StatusText.text = $"load audio path: {audioFilepath}";
				string extension = Path.GetExtension(audioFilepath);
				AudioType audioType = Utils.GetAudioType(extension);
				if (audioType != AudioType.UNKNOWN)
				{
					StatusText.text = $"trying to load audio clip path: {audioFilepath} extension: {extension}";
					StartCoroutine(Utils.LoadAudio2(audioFilepath, audioType, LoadAudioClipHandler,
						LoadAudioClipFailedHandler));
				}
				else
				{
					StatusText.text =
						$"failed to load audioClip file format not supported path: {audioFilepath} extension: {extension}";
				}	
			}
			else
			{
				StatusText.text =
					$"failed to load audio file, filepath is empty or null";
			}
		}

		private bool Save(string filename, AudioClip clip)
		{
			if (!string.IsNullOrEmpty(filename) && clip != null)
			{
				var filepath = Path.Combine(Application.persistentDataPath, filename);
				var directoryName = Path.GetDirectoryName(filepath);
				if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(directoryName))
				{
					Debug.Log(filepath);
					// Make sure directory exists if user is saving to sub dir.
					Directory.CreateDirectory(directoryName);

					using (var fileStream = CreateEmpty(filepath))
					{
						Utils.ConvertAndWrite(fileStream, clip);
						Utils.WriteHeader(fileStream, clip);
					}

					StatusText.SetText($"Save at {filepath}");
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private FileStream CreateEmpty(string filepath)
		{
			var fileStream = new FileStream(filepath, FileMode.Create);
			byte emptyByte = new byte();

			for (int i = 0; i < _headerSize; i++) //preparing the header
			{
				fileStream.WriteByte(emptyByte);
			}

			return fileStream;
		}

		private void LoadAudioClipFailedHandler()
		{
			Debug.Log($"{TAG} Failed to load AudioClip filepath: {_filepath}");
			StatusText.text = $"Failed to load AudioClip filepath: {_filepath}";
		}

		private void LoadAudioClipHandler(AudioClip audioClip)
		{
			StatusText.text = $"Successfully Load AudioClip filepath: {_filepath}";
			_isAudioClipLoaded = true;
			_loadedAudioClip = audioClip;
			PlayRecordedAudio();
		}
	}
}

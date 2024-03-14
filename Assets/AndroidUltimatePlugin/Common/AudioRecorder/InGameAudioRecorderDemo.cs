using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.Common.AudioRecorder
{
	public class InGameAudioRecorderDemo : MonoBehaviour
	{
		private const string TAG = "[InGameAudioRecorderDemo]: ";
		private string _filepath;

		// Audio Source
		public TextMeshProUGUI StatusText;
		public TextMeshProUGUI RecordButtonText;
		public TextMeshProUGUI PlayButtonText;
		public Button RecordButton;
		public Button PlayButton;
		public Button PlayExtra1Button;
		public Button PlayExtra2Button;
		public Button PlayExtra3Button;
		public AudioClip[] AudioClipCollection;
		
		private AudioSource _audioSource;
		private bool _isRecording;
		private bool _isPlaying;
		private AudioRenderer _audioRenderer;
		
		private UtilsPlugin _utilsPlugin;
		private bool _isAudioClipLoaded;
		private AudioClip _loadedAudioClip;

		private void Awake()
		{
			_utilsPlugin = UtilsPlugin.GetInstance();
			_utilsPlugin.Init();
			_utilsPlugin.SetDebug(0);
			EnableDisablePlayButton(false);
			_audioRenderer = new AudioRenderer();
		}

		// Start is called before the first frame update
		void Start()
		{
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

		private void EnableDisablePlayButton(bool val)
		{
			PlayButton.interactable = val;
		}
		
		private void EnableDisableRecordButton(bool val)
		{
			RecordButton.interactable = val;
			PlayExtra1Button.interactable = val;
			PlayExtra2Button.interactable = val;
			PlayExtra3Button.interactable = val;
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
			string filename =$"recordedInGameAudio_{Utils.GetTimeSinceEpoch()}.wav";
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
			PlayOne();
		}

		private void StopRecord()
		{
			_audioSource.Stop();
			AudioRenderer.Result result = _audioRenderer.Save(_filepath);
			StatusText.SetText($"Save at {result.Message}");
			_audioRenderer.Clear();
			EnableDisablePlayButton(true);
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

		private void OnAudioFilterRead(float[] data, int channels)
		{
			if (_isRecording)
			{
				_audioRenderer.Channels = channels;
				_audioRenderer.Write(data);
			}
		}

		private void PlayOne()
		{
			if (_isRecording)
			{
				_audioSource.PlayOneShot(AudioClipCollection[0]);
				Invoke("PlayTwo", 3f);
			}
		}

		private void PlayTwo()
		{
			if (_isRecording)
			{
				_audioSource.PlayOneShot(AudioClipCollection[1]);
				Invoke("PlayThree", 20f);
			}
		}

		private void PlayThree()
		{
			if (_isRecording)
			{
				_audioSource.PlayOneShot(AudioClipCollection[2]);
				Invoke("PlayFour", 13);
			}
		}

		private void PlayFour()
		{
			if (_isRecording)
			{
				_audioSource.PlayOneShot(AudioClipCollection[3]);
				Invoke("PlayOne", 10f);
			}
		}

		public void PlayExtra1()
		{
			if (_isRecording && !_isPlaying && AudioClipCollection[4]!=null)
			{
				_audioSource.PlayOneShot(AudioClipCollection[4]);	
			}
		}
		
		public void PlayExtra2()
		{
			if (_isRecording &&  !_isPlaying && AudioClipCollection[5]!=null)
			{
				_audioSource.PlayOneShot(AudioClipCollection[5]);	
			}
		}
		
		public void PlayExtra3()
		{
			if (_isRecording &&  !_isPlaying && AudioClipCollection[6]!=null)
			{
				_audioSource.PlayOneShot(AudioClipCollection[6]);	
			}
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
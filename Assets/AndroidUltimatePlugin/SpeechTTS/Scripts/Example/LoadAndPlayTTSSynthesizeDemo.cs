using System;
using System.Text;
using Gigadrillgames.AUP.Common;
using TMPro;
using UnityEngine;

namespace Gigadrillgames.AUP.SpeechTTS
{
    public class LoadAndPlayTTSSynthesizeDemo : MonoBehaviour
    {
        #region  Fields
        private const string _tag = "[LoadAndPlayTTSSynthesizeDemo]: ";
        public TextMeshProUGUI StatusText;
        public TextMeshProUGUI ResultText;
        private TextToSpeechPlugin _textToSpeechPlugin;
        private Dispatcher _dispatcher;
        private UtilsPlugin _utilsPlugin;
        private string _ttsEngine = "com.google.android.tts";
        private string[] _synthesizeFileNames;
        private StringBuilder _synthesizeFilenames;
        private string _synthesizeFileName;
        #endregion Fields

        #region Methods
        private void Awake()
        {
            _synthesizeFilenames = new StringBuilder();
            _dispatcher = Dispatcher.GetInstance();
            
            _utilsPlugin = UtilsPlugin.GetInstance();
            _utilsPlugin.Init();
            _utilsPlugin.SetDebug(0);

            _textToSpeechPlugin = TextToSpeechPlugin.GetInstance();
            _textToSpeechPlugin.Init();
            _textToSpeechPlugin.SetDebug(0);
        }

        // Use this for initialization
        void Start()
        {
            Invoke("DelayStartTTSEngine", 1f);
        }

        private void DelayStartTTSEngine()
        {
            _textToSpeechPlugin.StartTTSEngine(_ttsEngine);
            AddEventListener();
            CheckTTSDataActivity();
        }
        
        private void OnApplicationPause(bool val)
        {
            //for text to speech events
            if (_textToSpeechPlugin != null)
            {
                if (_textToSpeechPlugin.isInitialized())
                {
                    if (val)
                    {
                        _textToSpeechPlugin.UnRegisterBroadcastEvent();
                    }
                    else
                    {
                        _textToSpeechPlugin.RegisterBroadcastEvent();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            //call this of your not going to used TextToSpeech Service anymore
            RemoveEventListener();

            if (_textToSpeechPlugin != null)
            {
                _textToSpeechPlugin.ShutDownTextToSpeechService();
            }
        }

        private void AddEventListener()
        {
            if (_textToSpeechPlugin != null)
            {
                _textToSpeechPlugin.OnInitialize += HandleInit;
                _textToSpeechPlugin.OnGetTTSSynthesizeFiles += HandleTTSSynthesizeFiles;
            }
        }

        private void RemoveEventListener()
        {
            if (_textToSpeechPlugin != null)
            {
                _textToSpeechPlugin.OnInitialize -= HandleInit;
                _textToSpeechPlugin.OnGetTTSSynthesizeFiles -= HandleTTSSynthesizeFiles;
            }
        }

        private void UpdateStatus(string status)
        {
            if (StatusText != null)
            {
                StatusText.SetText( String.Format("Status: {0}", status));
            }
        }

        private void CheckTTSDataActivity()
        {
            if (_textToSpeechPlugin != null)
            {
                if (_textToSpeechPlugin.CheckTTSDataActivity())
                {
                    UpdateStatus("TTS Data Activity Status: Available");
                }
                else
                {
                    UpdateStatus("TTS Data Activity Status: Not Available");
                }
            }
        }

        public void GetSynthesizeFileNames()
        {
            string outputPath = Application.persistentDataPath;
            _textToSpeechPlugin.GetSynthesizeFiles(outputPath);
        }
        
        public void LoadSynthesizeFileName()
        {
            if (_synthesizeFileNames!=null)
            {
                if (_synthesizeFileNames.Length > 0)
                {
                    // if you want to load others synthesize file just change the index
                    // load the last save synthesize file hence "_synthesizeFileNames[_synthesizeFileNames.Length - 1]"
                    _synthesizeFileName = _synthesizeFileNames[_synthesizeFileNames.Length - 1];
                    UpdateStatus($"Load Synthesize FileName: {_synthesizeFileName}");
                    _textToSpeechPlugin.LoadSynthesizeFile(_synthesizeFileName);
                }
                else
                {
                    Debug.LogError("_synthesizeFileNames is empty!");
                    UpdateStatus("_synthesizeFileNames is empty!");
                }
            }
            else
            {
                Debug.LogError("_synthesizeFileNames is null");
                UpdateStatus("_synthesizeFileNames is null");
            }
        }
        
        public void PlaySynthesizeFile()
        {
            if (_synthesizeFileNames!=null)
            {
                if (_synthesizeFileNames.Length > 0)
                {
                    UpdateStatus($"Play Synthesize FileName: {_synthesizeFileName}");
                    _textToSpeechPlugin.PlaySynthesizeFile();
                }
                else
                {
                    Debug.LogError("_synthesizeFileNames is empty!");
                    UpdateStatus("_synthesizeFileNames is empty!");
                }
            }
            else
            {
                Debug.LogError("_synthesizeFileNames is null");
                UpdateStatus("_synthesizeFileNames is null");
            }
        }

        private void UpdateResult()
        {
            if (_synthesizeFileNames.Length > 0 && ResultText !=null)
            {
                _synthesizeFilenames.Clear();
                foreach (var filename in _synthesizeFileNames)
                {
                    _synthesizeFilenames.Append($"{filename}\n");
                }
                ResultText.SetText(_synthesizeFilenames.ToString());
            }
        }
        #endregion Methods

        #region  Events
        private void HandleInit(int status)
        {
            _dispatcher.InvokeAction(
                () =>
                {
                    Debug.Log(_tag + "HandleInit status: " + status);

                    if (status == 1)
                    {
                        UpdateStatus("init speech service successful!");
                    }
                    else
                    {
                        UpdateStatus("init speech service failed!");
                    }
                }
            );
        }
        
        private void HandleTTSSynthesizeFiles(string[] synthesizeFileNames)
        {
            if (synthesizeFileNames!=null)
            {
                if (synthesizeFileNames.Length > 0)
                {
                    UpdateStatus($"Get Synthesize file names successful count: {synthesizeFileNames.Length}");
                    _synthesizeFileNames = synthesizeFileNames;
                    UpdateResult();
                }
                else
                {
                    UpdateStatus("Get Synthesize file names failed, synthesizeFileNames empty!"); 
                }
            }
            else
            {
                UpdateStatus("Get Synthesize file names failed, synthesizeFileNames is null");
            }
        }
        #endregion Events
    }
}
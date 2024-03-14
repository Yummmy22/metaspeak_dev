using System.Text;
using Gigadrillgames.AUP.Common;
using Gigadrillgames.AUP.Helpers;
using Gigadrillgames.AUP.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.SpeechTTS
{
    public class SpeechRecognitionDemo : MonoBehaviour
    {
        #region Fields

        private const string _tag = "[SpeechRecognitionDemo]: ";
        private AutomaticSpeechRecognitionPlugin _automaticSpeechRecognitionPlugin;
        private PermissionPlugin _permissionPlugin;
        private Dispatcher _dispatcher;

#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private TextMeshProUGUI _microphoneButtonText;
        [SerializeField] private ScrollRect _scrollrect;
        [SerializeField] private Button _recognizeMicButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _clearButton;
        [SerializeField] private Button _englishButton;
        [SerializeField] private Button _spanishButton;
        [SerializeField] private Button _portugueseButton;
        [SerializeField] private Button _chineseButton;
#pragma warning restore 0649
        private bool _gotPermission;
        private bool _isReady;
        private StringBuilder _results = new StringBuilder();

        private readonly string[] _permissions =
        {
            "android.permission.WRITE_EXTERNAL_STORAGE",
            "android.permission.RECORD_AUDIO"
        };

        #endregion

        #region Methods

        private void Awake()
        {
            EnableDisableASRButton(false);
            // needed to run the callback on the main thread
            _dispatcher = Dispatcher.GetInstance();

            _permissionPlugin = PermissionPlugin.GetInstance();
            _permissionPlugin.Init();
            _permissionPlugin.SetDebug(0);
            _permissionPlugin.OnHandlePermission += HandlePermissions;
            

            _automaticSpeechRecognitionPlugin = AutomaticSpeechRecognitionPlugin.GetInstance();
            _automaticSpeechRecognitionPlugin.Init();
            _automaticSpeechRecognitionPlugin.SetDebug(0);
            _automaticSpeechRecognitionPlugin.OnHandlePostResult += OnHandlePostResult;
            _automaticSpeechRecognitionPlugin.OnHandlePartialResult += OnHandlePartialResult;
            _automaticSpeechRecognitionPlugin.OnHandleResult += OnHandleResult;
            _automaticSpeechRecognitionPlugin.OnHandleError += OnHandleError;

            _automaticSpeechRecognitionPlugin.OnHandleStart += OnHandleStart;
            _automaticSpeechRecognitionPlugin.OnHandleReady += OnHandleReady;
            _automaticSpeechRecognitionPlugin.OnHandleFile += OnHandleFile;
            _automaticSpeechRecognitionPlugin.OnHandleMic += OnHandleMic;
            _automaticSpeechRecognitionPlugin.OnHandleTimeout += OnHandleTimeout;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!_permissionPlugin.CheckPermision(_permissions))
            {
                _permissionPlugin.AskPermission(_permissions);
            }
            else
            {
                _gotPermission = true;
                _automaticSpeechRecognitionPlugin.StartASR(ASRLanguage.en);
            }
        }

        private void OnDestroy()
        {
            _permissionPlugin.OnHandlePermission -= HandlePermissions;

            _automaticSpeechRecognitionPlugin.OnHandlePostResult -= OnHandlePostResult;
            _automaticSpeechRecognitionPlugin.OnHandlePartialResult -= OnHandlePartialResult;
            _automaticSpeechRecognitionPlugin.OnHandleResult -= OnHandleResult;
            _automaticSpeechRecognitionPlugin.OnHandleError -= OnHandleError;

            _automaticSpeechRecognitionPlugin.OnHandleStart -= OnHandleStart;
            _automaticSpeechRecognitionPlugin.OnHandleReady -= OnHandleReady;
            _automaticSpeechRecognitionPlugin.OnHandleFile -= OnHandleFile;
            _automaticSpeechRecognitionPlugin.OnHandleMic -= OnHandleMic;
            _automaticSpeechRecognitionPlugin.OnHandleTimeout -= OnHandleTimeout;
            
            _automaticSpeechRecognitionPlugin.Kill();
        }

        private void EnableDisableASRButton(bool isEnable)
        {
            if (_recognizeMicButton!=null)
            {
                _recognizeMicButton.interactable = isEnable;    
            }
            
            if (_englishButton!=null)
            {
                _englishButton.interactable = isEnable;    
            }
            
            if (_spanishButton!=null)
            {
                _spanishButton.interactable = isEnable;    
            }
            
            if (_portugueseButton!=null)
            {
                _portugueseButton.interactable = isEnable;    
            }
            
            if (_chineseButton!=null)
            {
                _chineseButton.interactable = isEnable;
            }
            
            if (_stopButton!=null)
            {
                _stopButton.interactable = isEnable;    
            }
            
            if (_clearButton!=null)
            {
                _clearButton.interactable = isEnable;    
            }
        }

        private void ClearResult()
        {
            if (_resultText != null)
            {
                _results.Clear();
                _resultText.SetText("");
            }
        }

        private void AppendResult(string result)
        {
            if (_resultText != null)
            {
                _results.Append(result);
                _resultText.SetText(_results.ToString());
            }
        }

        private void UpdateStatus(string result)
        {
            if (_statusText != null)
            {
                _statusText.SetText(result);
            }
        }

        public void ClickRecognizeRecognizeFile()
        {
            if (_gotPermission && _isReady)
            {
                ClearResult();
                _scrollrect.ScrollToTop();
                _automaticSpeechRecognitionPlugin.RecognizeFile();
            }
            else
            {
                UpdateStatus("missing permissions");
            }

        }

        public void ClickRecognizeMicrophone()
        {
            if (_gotPermission && _isReady)
            {
                _automaticSpeechRecognitionPlugin.RecognizeMicrophone();
            }
            else
            {
                UpdateStatus("missing permissions");
            }
        }

        public void ClickClear()
        {
            if (_gotPermission)
            {
                ClearResult();
                _scrollrect.ScrollToTop();
            }
            else
            {
                UpdateStatus("missing permissions");
            }
        }

        public void ClickStop()
        {
            if (_gotPermission)
            {
                _automaticSpeechRecognitionPlugin.Stop();
                ClearResult();
                _scrollrect.ScrollToTop();
            }
            else
            {
                UpdateStatus("missing permissions");
            }
        }

        public void ClickKill()
        {
            if (_gotPermission)
            {
                _automaticSpeechRecognitionPlugin.Kill();
                ClearResult();
                _scrollrect.ScrollToTop();
                UpdateStatus("Recognition Shutdown!");
            }
            else
            {
                UpdateStatus("missing permissions");
            }
        }

        public void ClickLoadEnglish()
        {
            EnableDisableASRButton(false);
            _isReady = false;
            _automaticSpeechRecognitionPlugin.StartASR(ASRLanguage.en);
        }
        
        public void ClickLoadSpanish()
        {
            EnableDisableASRButton(false);
            _isReady = false;
            _automaticSpeechRecognitionPlugin.StartASR(ASRLanguage.es);
        }
        
        public void ClickLoadPortuguese()
        {
            EnableDisableASRButton(false);
            _isReady = false;
            _automaticSpeechRecognitionPlugin.StartASR(ASRLanguage.pt);
        }
        
        public void ClickLoadChinese()
        {
            EnableDisableASRButton(false);
            _isReady = false;
            _automaticSpeechRecognitionPlugin.StartASR(ASRLanguage.zh);
        }

        #endregion

        #region Events

        private void HandlePermissions(bool val)
        {
            Debug.Log($"HandlePermissions: {val}");
            _dispatcher.InvokeAction(
                () =>
                {
                    if (val)
                    {
                        // do something here based on user response
                        UpdateStatus("Permission allowed by user!");
                        _gotPermission = true;
                        _automaticSpeechRecognitionPlugin.StartASR(ASRLanguage.en);
                    }
                    else
                    {
                        UpdateStatus("Permission deny by user!");
                        _permissionPlugin.AskPermission(_permissions);
                    }
                }
            );
        }

        private void OnHandlePostResult(string result)
        {
            Debug.Log($"{_tag} OnHandlePostResult: {result}");
            _dispatcher.InvokeAction(
                () => { AppendResult(result); }
            );
        }

        private void OnHandlePartialResult(string result)
        {
            Debug.Log($"{_tag} OnHandlePartialResult");
        }

        private void OnHandleResult(string result)
        {
            Debug.Log($"{_tag} OnHandleResult");
            if (!string.IsNullOrEmpty(result))
            {
                ResultData resultData = JsonUtility.FromJson<ResultData>(result);
                if (resultData != null)
                {
                    if (resultData.result.Length > 0)
                    {
                        // checks if the result data is null or empty before we show it
                        if (!string.IsNullOrEmpty(resultData.text))
                        {
                            AppendResult(result);
                            StartCoroutine(_scrollrect.PushToBottom());
                            UpdateStatus("Handle Result Complete, please speak again");
                        }
                    }
                }
            }
        }

        private void OnHandleError(string error)
        {
            Debug.Log($"{_tag} OnHandleError: {error}");
            _dispatcher.InvokeAction(
                () => { AppendResult(error); }
            );
        }

        private void OnHandleReady()
        {
            Debug.Log($"{_tag} OnHandleReady");
            _dispatcher.InvokeAction(
                () =>
                {
                    _isReady = true;
                    UpdateStatus("Ready, please click Recognize Microphone to start listening");
                    _microphoneButtonText.SetText("Recognize Microphone");
                    ClearResult();
                    _scrollrect.ScrollToTop();
                    EnableDisableASRButton(true);
                }
            );
        }

        private void OnHandleStart()
        {
            Debug.Log($"{_tag} OnHandleStart");
            _dispatcher.InvokeAction(
                () => { UpdateStatus("started loading language model"); }
            );
        }

        private void OnHandleFile()
        {
            Debug.Log($"{_tag} OnHandleFile");
            _dispatcher.InvokeAction(
                () => { UpdateStatus("HandleFile it will read the ready made data sample.."); }
            );
        }

        private void OnHandleMic()
        {
            Debug.Log($"{_tag} OnHandleMic");
            _dispatcher.InvokeAction(
                () =>
                {
                    UpdateStatus("HandleMic, Please Speak now and wait for the result");
                    if (_microphoneButtonText != null)
                    {
                        _microphoneButtonText.SetText("Stop Microphone");
                    }
                }
            );
        }

        private void OnHandleTimeout()
        {
            Debug.Log($"{_tag} OnHandleTimeout");
            _dispatcher.InvokeAction(
                () => { UpdateStatus("Timeout"); }
            );
        }

        #endregion
    }
}

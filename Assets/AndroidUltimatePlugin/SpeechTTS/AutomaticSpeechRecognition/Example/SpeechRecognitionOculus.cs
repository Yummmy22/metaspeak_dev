using System.Collections.Generic;
using System.Text;
using Gigadrillgames.AUP.Common;
using Gigadrillgames.AUP.Helpers;
using Gigadrillgames.AUP.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.SpeechTTS
{
    public class SpeechRecognitionOculus : MonoBehaviour
    {
        #region Fields

        private const string _tag = "[SpeechRecognitionOculus]: ";
        private AutomaticSpeechRecognitionPlugin _automaticSpeechRecognitionPlugin;
        private PermissionPlugin _permissionPlugin;
        private Dispatcher _dispatcher;
#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private TextMeshProUGUI _commandsDetectText;
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private ScrollRect _scrollrect;
#pragma warning restore 0649
        private bool _gotPermission;
        private StringBuilder _results = new StringBuilder();

        private readonly string[] _permissions =
        {
            "android.permission.WRITE_EXTERNAL_STORAGE",
            "android.permission.RECORD_AUDIO"
        };

        private string[] _commandCollection =
        {
            "clear", "fly", "jump", "punch", "kick", "dock", "eat", "sleep", "scroll up", "scroll down", "open the window"
        };

        #endregion

        #region Methods

        private void Awake()
        {
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
            if (_resultText != null && !string.IsNullOrEmpty(result))
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

        private void CheckFile()
        {
            if (_gotPermission)
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

        private void ActivateMicrophone()
        {
            if (_gotPermission)
            {
                _automaticSpeechRecognitionPlugin.RecognizeMicrophone();
            }
            else
            {
                UpdateStatus("missing permissions");
            }
        }

        private void ActivateClear()
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

        private void CheckForResult(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                ResultData resultData = JsonUtility.FromJson<ResultData>(json);
                if (resultData != null)
                {
                    if (resultData.result.Length > 0)
                    {
                        // checks if the result data is null or empty before we show it
                        if (!string.IsNullOrEmpty(resultData.text))
                        {
                            AppendResult(json);
                            StartCoroutine(_scrollrect.PushToBottom());
                        }

                        // splits the result
                        string[] possibleCommandCollection =
                            resultData.text.Split(' ');
                        
                        /*WordCheckerJob myJob = new WordCheckerJob();
                        myJob.SearchMe = possibleCommandCollection;
                        myJob.FindMe = _commandCollection;
                        myJob.StartASR();
                        StartCoroutine(myJob.WaitFor());
                        List<CommandData> commandsCollection = myJob.FoundCommandCollection;*/
                        
                        // checks the commands and collect it
                        List<CommandData> commandsCollection =
                            WordChecker.CheckForCommands(possibleCommandCollection, _commandCollection);

                        // you can use this to checks all the commands that you detected
                        // un comment this if  you want to check it
                        /*foreach (var commandFound in commandsCollection)
                        {
                            Debug.Log($" commandFound: {commandFound.Command}");
                        }*/

                        // only checks command if there's at least one command 
                        if (commandsCollection.Count > 0)
                        {
                            // on this example we only checks the 1st command that we detected
                            CheckForCommands(commandsCollection[0]);
                        }
                        else
                        {
                            UpdateCommandsDetect("no command detected!", false);
                        }
                    }
                    else
                    {
                        UpdateCommandsDetect("result length is zero!", false);
                    }
                }
                else
                {
                    UpdateCommandsDetect("result data is null", false);
                }
            }
            else
            {
                UpdateCommandsDetect("json result data is null or empty!", false);
            }
        }

        private void CheckForCommands(CommandData commandData)
        {
            switch (commandData.Command)
            {
                case "clear":
                    ActivateClear();
                    UpdateCommandsDetect("detect command: clear", true);
                    break;
                case "scroll up":
                    StartCoroutine(_scrollrect.PushToTop());
                    UpdateCommandsDetect("detect command: scroll up", true);
                    break;
                case "scroll down":
                    StartCoroutine(_scrollrect.PushToBottom());
                    UpdateCommandsDetect("detect command: scroll down", true);
                    break;
                case "open the window":
                    UpdateCommandsDetect("detect command: open the window", true);
                    break;
                case "fly":
                    UpdateCommandsDetect("detect command: fly", true);
                    break;
                case "jump":
                    UpdateCommandsDetect("detect command: jump", true);
                    break;
                case "punch":
                    UpdateCommandsDetect("detect command: punch", true);
                    break;
                case "kick":
                    UpdateCommandsDetect("detect command: kick", true);
                    break;
                case "dock":
                    UpdateCommandsDetect("detect command: dock", true);
                    break;
                case "eat":
                    UpdateCommandsDetect("detect command: eat", true);
                    break;
                case "sleep":
                    UpdateCommandsDetect("detect command: sleep", true);
                    break;
            }
        }

        private void UpdateCommandsDetect(string command, bool hasFoundCommand)
        {
            if (_commandsDetectText != null)
            {
                if (hasFoundCommand)
                {
                    _commandsDetectText.SetText($"<color=green>{command}</color>");
                }
                else
                {
                    _commandsDetectText.SetText($"<color=red>{command}</color>");
                }
            }
        }

        public void ClickRecognizeRecognizeFile()
        {
            CheckFile();
        }

        public void ClickRecognizeMicrophone()
        {
            ActivateMicrophone();
        }

        public void ClickClear()
        {
            ActivateClear();
        }

        public void ClickStop()
        {
            CheckForResult("do fly and clear");
            /*if (_gotPermission)
            {
                _speechRecognitionPlugin.Stop();
                ClearResult();
                _scrollrect.ScrollToTop();
            }
            else
            {
                UpdateStatus("missing permissions");
            }*/
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

        public void ClickExit()
        {
            Application.Quit();
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
            Debug.Log($"{_tag} OnHandlePostResult");
            _dispatcher.InvokeAction(
                () => { AppendResult(result); }
            );
        }

        private void OnHandlePartialResult(string result)
        {
            Debug.Log($"{_tag} OnHandlePartialResult");
            /* _dispatcher.InvokeAction(
                 () =>
                 {
                     AppendResult(result);
                 }
             );*/
        }

        private void OnHandleResult(string result)
        {
            Debug.Log($"{_tag} OnHandleResult");
            CheckForResult(result);
        }

        private void OnHandleError(string error)
        {
            Debug.Log($"{_tag} OnHandleError: {error}");
            _dispatcher.InvokeAction(
                () =>
                {
                    ClearResult();
                    AppendResult(error);
                    _scrollrect.ScrollToTop();
                }
            );
        }

        private void OnHandleReady()
        {
            Debug.Log($"{_tag} OnHandleReady");
            _dispatcher.InvokeAction(
                () =>
                {
                    UpdateStatus("Ready");
                    ClearResult();
                    _scrollrect.ScrollToTop();
                    ActivateMicrophone();
                }
            );
        }

        private void OnHandleStart()
        {
            Debug.Log($"{_tag} OnHandleStart");

            _dispatcher.InvokeAction(
                () => { UpdateStatus("StartASR"); }
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
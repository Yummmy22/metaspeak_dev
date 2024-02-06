using System;
using Gigadrillgames.AUP.Common;
using UnityEngine;

namespace Gigadrillgames.AUP.SpeechTTS
{
    public class AutomaticSpeechRecognitionPlugin : MonoBehaviour
    {
        private static AutomaticSpeechRecognitionPlugin _instance;
        private static GameObject _container;
        private const string _tag = "[AutomaticSpeechRecognitionPlugin]: ";
        private static AUPHolder _aupHolder;

        private Action<string> _handlePostResult;
        public event Action<string> OnHandlePostResult
        {
            add { _handlePostResult += value; }
            remove { _handlePostResult += value; }
        }
        
        private Action<string> _handleResult;
        public event Action<string> OnHandleResult
        {
            add { _handleResult += value; }
            remove { _handleResult += value; }
        }
        
        private Action<string> _handlePartialResult;
        public event Action<string> OnHandlePartialResult
        {
            add { _handlePartialResult += value; }
            remove { _handlePartialResult += value; }
        }
        
        private Action<string> _handleError;
        public event Action<string> OnHandleError
        {
            add { _handleError += value; }
            remove { _handleError += value; }
        }
        
        private Action _handleStart;
        public event Action OnHandleStart
        {
            add { _handleStart += value; }
            remove { _handleStart += value; }
        }
        
        private Action _handleReady;
        public event Action OnHandleReady
        {
            add { _handleReady += value; }
            remove { _handleReady += value; }
        }
        
        private Action _handleFile;
        public event Action OnHandleFile
        {
            add { _handleFile += value; }
            remove { _handleFile += value; }
        }
        
        private Action _handleMic;
        public event Action OnHandleMic
        {
            add { _handleMic += value; }
            remove { _handleMic += value; }
        }
        
        private Action _handleTimeout;
        public event Action OnHandleTimeout
        {
            add { _handleTimeout += value; }
            remove { _handleTimeout += value; }
        }

#if UNITY_ANDROID
        private static AndroidJavaObject _jo;
#endif

        public bool IsDebug = true;
        private bool _isInit = false;

        public static AutomaticSpeechRecognitionPlugin GetInstance()
        {
            if (_instance == null)
            {
                _container = new GameObject();
                _container.name = "AutomaticSpeechRecognitionPlugin";
                _instance = _container.AddComponent(typeof(AutomaticSpeechRecognitionPlugin)) as AutomaticSpeechRecognitionPlugin;
                DontDestroyOnLoad(_instance.gameObject);
                _aupHolder = AUPHolder.GetInstance();
                _instance.gameObject.transform.SetParent(_aupHolder.gameObject.transform);
            }

            return _instance;
        }

        private void Awake()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo = new AndroidJavaObject("com.gigadrillgames.androidplugin.automaticspeechrecognition.AutomaticSpeechRecognitionPlugin");
            }
#endif
        }

        /// <summary>
        /// Sets the debug.
        /// 0 - false, 1 - true
        /// </summary>
        /// <param name="debug">Debug.</param>
        public void SetDebug(int debug)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo.CallStatic("SetDebug", debug);
                Utils.Message(_tag, "SetDebug");
            }
            else
            {
                Utils.Message(_tag, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// initialize the Image Picker Plugin
        /// </summary>
        public void Init()
        {
            if (_isInit)
            {
                return;
            }

#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
                AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
                
                AutomaticSpeechRecognitionCallback automaticSpeechRecognitionCallback = new AutomaticSpeechRecognitionCallback();
                automaticSpeechRecognitionCallback.onHandlePostResult = onHandlePostResult;
                automaticSpeechRecognitionCallback.onHandleResult = onHandleResult;
                automaticSpeechRecognitionCallback.onHandlePartialResult = onHandlePartialResult;
                automaticSpeechRecognitionCallback.onHandleError = onHandleError;
                
                automaticSpeechRecognitionCallback.onHandleStart = onHandleStart;
                automaticSpeechRecognitionCallback.onHandleReady = onHandleReady;
                automaticSpeechRecognitionCallback.onHandleFile = onHandleFile;
                automaticSpeechRecognitionCallback.onHandleMic = onHandleMic;
                automaticSpeechRecognitionCallback.onHandleTimeOut = onHandleTimeout;
                _jo.CallStatic("init",currentActivity,automaticSpeechRecognitionCallback);
                _isInit = true;
                Utils.Message(_tag, "init");
            }
            else
            {
                Utils.Message(_tag, "warning: must run in actual android device");
            }
#endif
        }


        public void StartASR(ASRLanguage language)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo.CallStatic("start",language.ToString());
                Utils.Message(_tag, "start");
            }
            else
            {
                Utils.Message(_tag, "warning: must run in actual android device");
            }
#endif
        }
        
        public void Stop()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo.CallStatic("stop");
                Utils.Message(_tag, "stop");
            }
            else
            {
                Utils.Message(_tag, "warning: must run in actual android device");
            }
#endif
        }

        public void Kill()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo.CallStatic("kill");
                Utils.Message(_tag, "kill");
            }
            else
            {
                Utils.Message(_tag, "warning: must run in actual android device");
            }
#endif
        }
        
        public void RecognizeFile()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo.CallStatic("recognizeFile");
                Utils.Message(_tag, "recognizeFile");
            }
            else
            {
                Utils.Message(_tag, "warning: must run in actual android device");
            }
#endif
        }
        
        public void RecognizeMicrophone()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo.CallStatic("recognizeMicrophone");
                Utils.Message(_tag, "recognizeMicrophone");
            }
            else
            {
                Utils.Message(_tag, "warning: must run in actual android device");
            }
#endif
        }
        

        private void onHandlePostResult(string result)
        {
            if (null != _handlePostResult)
            {
                _handlePostResult(result);
            }
        }
        
        private void onHandleResult(string result)
        {
            if (null != _handleResult)
            {
                _handleResult(result);
            }
        }
        
        private void onHandlePartialResult(string result)
        {
            if (null != _handlePartialResult)
            {
                _handlePartialResult(result);
            }
        }
        
        private void onHandleError(string result)
        {
            if (null != _handleError)
            {
                _handleError(result);
            }
        }
        
        private void onHandleStart()
        {
            if (null != _handleStart)
            {
                _handleStart();
            }
        }
        
        private void onHandleReady()
        {
            if (null != _handleReady)
            {
                _handleReady();
            }
        }
        
        private void onHandleFile()
        {
            if (null != _handleFile)
            {
                _handleFile();
            }
        }
        
        private void onHandleMic()
        {
            if (null != _handleMic)
            {
                _handleMic();
            }
        }
        
        private void onHandleTimeout()
        {
            if (null != _handleTimeout)
            {
                _handleTimeout();
            }
        }
    }
}


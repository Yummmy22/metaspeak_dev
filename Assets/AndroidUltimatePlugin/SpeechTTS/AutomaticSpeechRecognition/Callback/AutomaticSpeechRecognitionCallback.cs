using System;
using UnityEngine;

namespace Gigadrillgames.AUP.SpeechTTS
{
    public class AutomaticSpeechRecognitionCallback : AndroidJavaProxy
    {
        public Action<string> onHandlePostResult;
        public Action<string> onHandleResult;
        public Action<string> onHandlePartialResult;
        public Action<string> onHandleError;
        public Action onHandleStart;
        public Action onHandleReady;
        public Action onHandleFile;
        public Action onHandleMic;
        public Action onHandleTimeOut;
        
        public AutomaticSpeechRecognitionCallback() : base("com.gigadrillgames.androidplugin.automaticspeechrecognition.IAutomaticSpeechRecognition")
        {
        }

        void HandlePostResult(String result)
        {
            onHandlePostResult(result);
        }

        void HandleResult(String result)
        {
            onHandleResult(result);
        }

        void HandlePartialResult(String result)
        {
            onHandlePartialResult(result);
        }

        void HandleError(String error)
        {
            onHandleError(error);
        }

        void HandleStart()
        {
            onHandleStart();
        }

        void HandleReady()
        {
            onHandleReady();
        }

        void HandleFile()
        {
            onHandleFile();
        }

        void HandleMic()
        {
            onHandleMic();
        }
        
        void HandleTimeout()
        {
            onHandleTimeOut();
        }
    }
}

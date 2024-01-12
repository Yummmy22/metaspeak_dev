/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk deteksi suara dalam bahasa Inggris. Base scriptnya dapet nemu di Github. Asyaaap!!
 *          Mari kita utak atik sodara-sodaraah!
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

namespace Zetcil
{
    public class WindowsSpeechRecognition : MonoBehaviour
    {
        public enum CRecognitionInterval { None, Once, Repeat }
        public enum CRecognitionBehavior { None, ClearAfterRecognized }

        [Space(10)]
        public bool isEnabled;

        [Header("Speech Recognition Settings")]
        public CRecognitionInterval RecognitionInterval = CRecognitionInterval.Repeat;
        public CRecognitionBehavior RecognitionBehavior = CRecognitionBehavior.ClearAfterRecognized;
        public ConfidenceLevel Confidence = ConfidenceLevel.Low;

        [Header("Windows Keywords Settings")]
        public string[] Keywords;
        [HideInInspector]
        public string[] dictionary;
        protected PhraseRecognizer recognizer;

        [System.Serializable]
        public class CEventSpeech
        {
            [Header("Speech List")]
            public List<string> SpeechRecognized;
            [Header("Speech Event")]
            public UnityEvent SpeechEvent;
        }

        [Header("Events Settings")]
        public bool usingEventSetting;
        public List<CEventSpeech> EventSpeech;

        [Header("Variable Settings")]
        public string TargetString;

        public void EnableUnitySetting()
        {
            isEnabled = true;
            usingEventSetting = true;
        }

        public void DisableUnitySetting()
        {
            isEnabled = false;
            usingEventSetting = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                dictionary = new string[Keywords.Length];
                for (int i = 0; i < Keywords.Length; i++)
                {
                    dictionary[i] = Keywords[i];
                }

                recognizer = new KeywordRecognizer(dictionary, Confidence);
                recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
                recognizer.Start();
            }
        }

        void FixedUpdate()
        {
            if (this.gameObject.activeSelf)
            {
                if (usingEventSetting)
                {
                    foreach (CEventSpeech temp in EventSpeech)
                    {
                        foreach (string speechcompare in temp.SpeechRecognized)
                        {
                            if (speechcompare == TargetString)
                            {
                                if (RecognitionInterval == CRecognitionInterval.Once)
                                {
                                    RecognitionInterval = CRecognitionInterval.None;
                                    temp.SpeechEvent.Invoke();
                                }
                                else if (RecognitionInterval == CRecognitionInterval.Repeat)
                                {
                                    temp.SpeechEvent.Invoke();
                                }
                                if (RecognitionBehavior == CRecognitionBehavior.ClearAfterRecognized)
                                {
                                    TargetString = "";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            if (this.gameObject.activeSelf)
            {
                TargetString = args.text;

                if (usingEventSetting)
                {
                    foreach (CEventSpeech temp in EventSpeech)
                    {
                        foreach (string speechcompare in temp.SpeechRecognized)
                        {
                            if (speechcompare == TargetString)
                            {
                                if (RecognitionInterval == CRecognitionInterval.Once)
                                {
                                    RecognitionInterval = CRecognitionInterval.None;
                                    temp.SpeechEvent.Invoke();
                                }
                                else if (RecognitionInterval == CRecognitionInterval.Repeat)
                                {
                                    temp.SpeechEvent.Invoke();
                                }
                                if (RecognitionBehavior == CRecognitionBehavior.ClearAfterRecognized)
                                {
                                    TargetString = "";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            if (recognizer != null && recognizer.IsRunning)
            {
                recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
                recognizer.Stop();
            }
        }
    }
}

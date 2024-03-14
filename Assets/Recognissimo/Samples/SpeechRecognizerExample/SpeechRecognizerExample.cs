using System;
using System.Collections.Generic;
using System.Linq;
using Recognissimo.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Recognissimo.Samples.SpeechRecognizerExample
{
    [AddComponentMenu("")]
    public class SpeechRecognizerExample : MonoBehaviour
    {
        private const string LoadingMessage = "Loading...";

        [SerializeField]
        private SpeechRecognizer recognizer;

        [SerializeField]
        private StreamingAssetsLanguageModelProvider languageModelProvider;

        [SerializeField]
        private Dropdown languageDropdown;

        [SerializeField]
        private Button startButton;

        [SerializeField]
        private InputField status;

        private readonly RecognizedText _recognizedText = new();

        private List<SystemLanguage> _availableLanguages;

        private void OnEnable()
        {
            // Make sure language models exist.
            if (languageModelProvider.languageModels.Count == 0)
            {
                throw new InvalidOperationException("No language models.");
            }

            // Store available languages in the list.
            _availableLanguages = languageModelProvider.languageModels
                .Select(languageModel => languageModel.language)
                .ToList();

            // Set default language.
            languageModelProvider.language = GetPreferredLanguage(_availableLanguages);

            // Initialize UI.
            InitializeLanguageDropdown();
            UpdateStatus("");

            // Bind recognizer to event handlers.
            recognizer.Started.AddListener(() =>
            {
                _recognizedText.Clear();
                UpdateStatus("");
            });
            
            recognizer.Finished.AddListener(() => Debug.Log("Finished"));

            recognizer.PartialResultReady.AddListener(OnPartialResult);
            recognizer.ResultReady.AddListener(OnResult);

            recognizer.InitializationFailed.AddListener(OnError);
            recognizer.RuntimeFailed.AddListener(OnError);

            startButton.onClick.AddListener(() =>
            {
                UpdateStatus(LoadingMessage);
                recognizer.StartProcessing();
            });
        }

        private static SystemLanguage GetPreferredLanguage(IList<SystemLanguage> availableLanguages)
        {
            if (availableLanguages.Count == 0)
            {
                throw new InvalidOperationException("No available languages.");
            }

            // Return system language if there's a language model.
            if (availableLanguages.Contains(Application.systemLanguage))
            {
                return Application.systemLanguage;
            }

            // Return English if there's a language model.
            if (availableLanguages.Contains(SystemLanguage.English))
            {
                return SystemLanguage.English;
            }

            // Return first language in the list.
            return availableLanguages[0];
        }

        private void InitializeLanguageDropdown()
        {
            // Fill dropdown with available languages.
            languageDropdown.options = _availableLanguages
                .Select(language => new Dropdown.OptionData {text = language.ToString()})
                .ToList();

            // Set default value to default language of the language model provider.
            languageDropdown.value =
                languageDropdown.options.FindIndex(option => option.text == languageModelProvider.language.ToString());

            // Set language for language model provider and stop recognition when the user has selected a new language.
            languageDropdown.onValueChanged.AddListener(index =>
            {
                var optionText = languageDropdown.options[index].text;
                var selectedLanguage = (SystemLanguage) Enum.Parse(typeof(SystemLanguage), optionText);
                languageModelProvider.language = selectedLanguage;
            });
        }

        private void UpdateStatus(string text)
        {
            status.text = text;
        }

        private void OnPartialResult(PartialResult partial)
        {
            _recognizedText.Append(partial);
            UpdateStatus(_recognizedText.CurrentText);
        }

        private void OnResult(Result result)
        {
            _recognizedText.Append(result);
            UpdateStatus(_recognizedText.CurrentText);
        }

        private void OnError(SpeechProcessorException exception)
        {
            UpdateStatus($"<color=red>{exception.Message}</color>");
        }

        /// <summary>
        ///     Helper class that accumulates recognition results.
        /// </summary>
        private class RecognizedText
        {
            private string _changingText;
            private string _stableText;

            public string CurrentText => $"{_stableText} <color=grey>{_changingText}</color>";

            public void Append(Result result)
            {
                _changingText = "";
                _stableText = $"{_stableText} {result.text}";
            }

            public void Append(PartialResult partialResult)
            {
                _changingText = partialResult.partial;
            }

            public void Clear()
            {
                _changingText = "";
                _stableText = "";
            }
        }
    }
}
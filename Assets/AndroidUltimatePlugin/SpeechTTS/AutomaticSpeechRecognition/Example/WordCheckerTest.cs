using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gigadrillgames.AUP.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.SpeechTTS
{
    public class WordCheckerTest : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private TextMeshProUGUI _commandsDetectText;
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private ScrollRect _scrollrect;
#pragma warning restore 0649
        private StringBuilder _results = new StringBuilder();

        private string[] _commandCollection =
        {
            "clear", "fly", "jump", "punch", "kick", "dock", "eat", "sleep", "scroll up", "open the window"
        };

        // Start is called before the first frame update
        void Start()
        {
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

        private void ActivateClear()
        {
            ClearResult();
            _scrollrect.ScrollToTop();
        }

        private async Task CheckForResult(string json)
        {
            await Task.Delay(1000);
            if (!string.IsNullOrEmpty(json))
            {
                ResultData resultData = JsonUtility.FromJson<ResultData>(json);
                if (resultData != null)
                {
                    if (resultData.result.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(resultData.text))
                        {
                            AppendResult(json);
                            StartCoroutine(_scrollrect.PushToBottom());
                        }

                        string[] possibleCommandCollection =
                            resultData.text.Split(' ');
                        List<CommandData> commandsCollection = null;

                        commandsCollection =
                            WordChecker.CheckForCommands(possibleCommandCollection, _commandCollection);

                        // you can use this to checks all the commands that you detected
                        foreach (var commandFound in commandsCollection)
                        {
                            Debug.Log($" commandFound: {commandFound.Command}");
                        }

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
                    StartCoroutine(_scrollrect.PushToBottom());
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

        public async void ClickTest()
        {
            // sample possible data
            string json = "{ \n" +
                          "\"result\":[{ \n" +
                          "\"conf\": 0.9666,\n" +
                          "\"end\": 0.966,\n" +
                          "\"start\": 0.489,\n" +
                          "\"word\": \"open the window\"\n" +
                          "}],\n" +
                          "\"text\":\"open the window\"\n" +
                          "}\n";

            await CheckForResult(json);

            json = "{ \n" +
                   "\"result\":[{ \n" +
                   "\"conf\": 0.9666,\n" +
                   "\"end\": 0.966,\n" +
                   "\"start\": 0.489,\n" +
                   "\"word\": \"clear\"\n" +
                   "}],\n" +
                   "\"text\":\"clear\"\n" +
                   "}\n";
            await CheckForResult(json);

            json = "{ \n" +
                   "\"result\":[{ \n" +
                   "\"conf\": 0.9666,\n" +
                   "\"end\": 0.966,\n" +
                   "\"start\": 0.489,\n" +
                   "\"word\": \"scroll up\"\n" +
                   "}],\n" +
                   "\"text\":\"scroll up\"\n" +
                   "}\n";
            await CheckForResult(json);

            json = "{ \n" +
                   "\"result\":[{ \n" +
                   "\"conf\": 0.9666,\n" +
                   "\"end\": 0.966,\n" +
                   "\"start\": 0.489,\n" +
                   "\"word\": \"\"\n" +
                   "}],\n" +
                   "\"text\":\"\"\n" +
                   "}\n";
            await CheckForResult(json);

            json = "{ \n" +
                   "\"result\":[{ \n" +
                   "\"conf\": 0.9666,\n" +
                   "\"end\": 0.966,\n" +
                   "\"start\": 0.489,\n" +
                   "\"word\": \"blah\"\n" +
                   "}],\n" +
                   "\"text\":\"blah\"\n" +
                   "}\n";
            await CheckForResult(json);

            json = "{ \n" +
                   "\"result\":[{ \n" +
                   "\"conf\": 0.9666,\n" +
                   "\"end\": 0.966,\n" +
                   "\"start\": 0.489,\n" +
                   "\"word\": \"punch\"\n" +
                   "}],\n" +
                   "\"text\":\"punch\"\n" +
                   "}\n";
            await CheckForResult(json);

            json = "{ \n" +
                   "\"result\":[{ \n" +
                   "\"conf\": 0.9666,\n" +
                   "\"end\": 0.966,\n" +
                   "\"start\": 0.489,\n" +
                   "\"word\": \"kick\"\n" +
                   "}],\n" +
                   "\"text\":\"kick\"\n" +
                   "}\n";
            await CheckForResult(json);
        }
    }
}
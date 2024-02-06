using System;
using System.Collections.Generic;
using Gigadrillgames.AUP.SpeechTTS;
using UnityEngine;

public class WordCheckerJob : ThreadedJob
{
    public string[] SearchMe;
    public string[] FindMe;
    public List<CommandData> FoundCommandCollection;

    protected override void ThreadFunction()
    {
        FoundCommandCollection = new List<CommandData>();
        int searchLength = SearchMe.Length;
        for (int i = 0; i < searchLength; i++)
        {
            for (int j = 0; j < FindMe.Length; j++)
            {
                // check for commands with 3 words
                if ( (i+1) < searchLength && (i+2) < searchLength)
                {
                    if (!string.IsNullOrEmpty(SearchMe[i]) && !string.IsNullOrEmpty(SearchMe[i + 1]) && !string.IsNullOrEmpty(SearchMe[i + 2]))
                    {
                        if ( $"{SearchMe[i]} {SearchMe[i + 1]} {SearchMe[i+2]}".Equals(FindMe[j],StringComparison.Ordinal))
                        {
                            CommandData commandData = new CommandData();
                            commandData.Command = FindMe[j]; 
                            commandData.Index = i;
                            FoundCommandCollection.Add(commandData);
                        }
                    }    
                }

                // check for commands with 2 words
                if ( (i+1) < searchLength)
                {
                    if (!string.IsNullOrEmpty(SearchMe[i]) && !string.IsNullOrEmpty(SearchMe[i + 1]))
                    {
                        if ( $"{SearchMe[i]} {SearchMe[i + 1]}".Equals(FindMe[j],StringComparison.Ordinal))
                        {
                            CommandData commandData = new CommandData();
                            commandData.Command = FindMe[j]; 
                            commandData.Index = i;
                            FoundCommandCollection.Add(commandData);
                        }
                    }    
                }

                // check for commands with 1 word
                if (!string.IsNullOrEmpty(SearchMe[i]))
                {
                    if (SearchMe[i].Equals(FindMe[j],StringComparison.Ordinal))
                    {
                        CommandData commandData = new CommandData();
                        commandData.Command = FindMe[j]; 
                        commandData.Index = i;
                        FoundCommandCollection.Add(commandData);
                    }
                }
            }
        }
    }
    protected override void OnFinished()
    {
        // This is executed by the Unity main thread when the job is finished
        for (int i = 0; i < FoundCommandCollection.Count; i++)
        {
            Debug.Log("Results(" + i + "): " + FoundCommandCollection[i].Command);
        }
    }
}
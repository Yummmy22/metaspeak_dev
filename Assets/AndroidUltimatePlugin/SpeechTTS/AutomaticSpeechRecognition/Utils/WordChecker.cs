using System;
using System.Collections.Generic;

namespace Gigadrillgames.AUP.SpeechTTS
{
    public static class WordChecker 
    {
        public static List<CommandData> CheckForCommands(string[] searchMe, string[] findMe)
        {
             List<CommandData> foundCommandCollection = new List<CommandData>();
             int searchLength = searchMe.Length;
             for (int i = 0; i < searchLength; i++)
             {
                 for (int j = 0; j < findMe.Length; j++)
                 {
                     // check for commands with 3 words
                     if ( (i+1) < searchLength && (i+2) < searchLength)
                     {
                         if (!string.IsNullOrEmpty(searchMe[i]) && !string.IsNullOrEmpty(searchMe[i + 1]) && !string.IsNullOrEmpty(searchMe[i + 2]))
                         {
                             if ( $"{searchMe[i]} {searchMe[i + 1]} {searchMe[i+2]}".Equals(findMe[j],StringComparison.Ordinal))
                             {
                                 CommandData commandData = new CommandData();
                                 commandData.Command = findMe[j]; 
                                 commandData.Index = i;
                                 foundCommandCollection.Add(commandData);
                             }
                         }    
                     }

                     // check for commands with 2 words
                     if ( (i+1) < searchLength)
                     {
                         if (!string.IsNullOrEmpty(searchMe[i]) && !string.IsNullOrEmpty(searchMe[i + 1]))
                         {
                             if ( $"{searchMe[i]} {searchMe[i + 1]}".Equals(findMe[j],StringComparison.Ordinal))
                             {
                                 CommandData commandData = new CommandData();
                                 commandData.Command = findMe[j]; 
                                 commandData.Index = i;
                                 foundCommandCollection.Add(commandData);
                             }
                         }    
                     }

                     // check for commands with 1 word
                     if (!string.IsNullOrEmpty(searchMe[i]))
                     {
                         if (searchMe[i].Equals(findMe[j],StringComparison.Ordinal))
                         {
                             CommandData commandData = new CommandData();
                             commandData.Command = findMe[j]; 
                             commandData.Index = i;
                             foundCommandCollection.Add(commandData);
                         }
                     }
                 }
             }
             return foundCommandCollection;
        }
    }
}

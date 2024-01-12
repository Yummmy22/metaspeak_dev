/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menampung nilai global variabel
 **************************************************************************************************************/
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarManager : MonoBehaviour
    {
        public enum COperationType { SaveData, LoadData, LoadAndSaveData }
        public enum CStorageType { None, ByPlayerPref, ByXML }

        [Space(10)]
        public bool isEnabled;

        [Header("Storage Settings")]
        public CStorageType StorageType;

        [Header("Operation Settings")]
        public COperationType OperationType;
        public VarString XMLFile;
        public string URLPath;

        [System.Serializable]
        public class CTimeContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarTime TimeVariables;
        }

        [System.Serializable]
        public class CHealthContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarHealth HealthVariables;
        }

        [System.Serializable]
        public class CManaContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarMana ManaVariables;
        }

        [System.Serializable]
        public class CExpContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarExp ExpVariables;
        }

        [System.Serializable]
        public class CScoreContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarScore ScoreVariables;
        }

        [System.Serializable]
        public class CIntContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarInteger IntVariables;
        }

        [System.Serializable]
        public class CFloatContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarFloat FloatVariables;
        }

        [System.Serializable]
        public class CStringContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarString StringVariables;
        }

        [System.Serializable]
        public class CBoolContainer
        {
            [Header("Key")]
            public string UniqueID;
            [Header("Value")]
            public VarBoolean BoolVariables;
        }

        [Header("Save Data Container")]
        [Separator]
        public bool SaveOnStart;
        public CTimeContainer[] SaveTimeContainer;
        public CHealthContainer[] SaveHealthContainer;
        public CManaContainer[] SaveManaContainer;
        public CExpContainer[] SaveExpContainer;
        public CScoreContainer[] SaveScoreContainer;
        public CFloatContainer[] SaveFloatContainer;
        public CIntContainer[] SaveIntContainer;
        public CStringContainer[] SaveStringContainer;
        public CBoolContainer[] SaveBoolContainer;

        [Header("Load Data Container")]
        [Separator]
        public bool LoadOnStart;
        public CTimeContainer[] LoadTimeContainer;
        public CHealthContainer[] LoadHealthContainer;
        public CManaContainer[] LoadManaContainer;
        public CExpContainer[] LoadExpContainer;
        public CScoreContainer[] LoadScoreContainer;
        public CFloatContainer[] LoadFloatContainer;
        public CIntContainer[] LoadIntContainer;
        public CStringContainer[] LoadStringContainer;
        public CBoolContainer[] LoadBoolContainer;

        void Start()
        {
            if (LoadOnStart && OperationType == COperationType.LoadData ||
                LoadOnStart && OperationType == COperationType.LoadAndSaveData)
            {
                LoadData();
            }
            if (SaveOnStart && OperationType == COperationType.SaveData ||
                SaveOnStart && OperationType == COperationType.LoadAndSaveData)
            {
                SaveData();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public string DataDirectory()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/Data/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Data/");
            }
            return Application.persistentDataPath + "/Data/";
        }

        public void SaveData() {
            if (StorageType == CStorageType.ByPlayerPref)
            {
                for (int i = 0; i < SaveHealthContainer.Length; i++)
                {
                    PlayerPrefs.SetFloat(SaveHealthContainer[i].UniqueID, SaveHealthContainer[i].HealthVariables.CurrentValue);
                }
                for (int i = 0; i < SaveManaContainer.Length; i++)
                {
                    PlayerPrefs.SetFloat(SaveManaContainer[i].UniqueID, SaveManaContainer[i].ManaVariables.CurrentValue);
                }
                for (int i = 0; i < SaveExpContainer.Length; i++)
                {
                    PlayerPrefs.SetFloat(SaveExpContainer[i].UniqueID, SaveExpContainer[i].ExpVariables.CurrentValue);
                }
                for (int i = 0; i < SaveBoolContainer.Length; i++)
                {
                    PlayerPrefs.SetString(SaveBoolContainer[i].UniqueID, SaveBoolContainer[i].BoolVariables.CurrentValue.ToString());
                }
                for (int i = 0; i < SaveIntContainer.Length; i++)
                {
                    PlayerPrefs.SetInt(SaveIntContainer[i].UniqueID, SaveIntContainer[i].IntVariables.CurrentValue);
                }
                for (int i = 0; i < SaveFloatContainer.Length; i++)
                {
                    PlayerPrefs.SetFloat(SaveFloatContainer[i].UniqueID, SaveFloatContainer[i].FloatVariables.CurrentValue);
                }
                for (int i = 0; i < SaveStringContainer.Length; i++)
                {
                    PlayerPrefs.SetString(SaveStringContainer[i].UniqueID, SaveStringContainer[i].StringVariables.CurrentValue);
                }
                for (int i=0; i<SaveTimeContainer.Length; i++)
                {
                    PlayerPrefs.SetInt(SaveTimeContainer[i].UniqueID, SaveTimeContainer[i].TimeVariables.CurrentValue);
                }
                for (int i = 0; i < SaveScoreContainer.Length; i++)
                {
                    PlayerPrefs.SetFloat(SaveScoreContainer[i].UniqueID, SaveScoreContainer[i].ScoreVariables.CurrentValue);
                }
            }
            if (StorageType == CStorageType.ByXML)
            {
                int index = 0;
                var sr = File.CreateText(DataDirectory() + XMLFile.CurrentValue + ".xml");
                sr.WriteLine("<DataCollection>");

                sr.WriteLine("<DataGroup>");
                    sr.WriteLine("\t<HealthTotal>" + SaveHealthContainer.Length.ToString() + "</HealthTotal>");
                    sr.WriteLine("\t<ManaTotal>" + SaveManaContainer.Length.ToString() + "</ManaTotal>");
                    sr.WriteLine("\t<ExpTotal>" + SaveExpContainer.Length.ToString() + "</ExpTotal>");
                    sr.WriteLine("\t<TimeTotal>" + SaveTimeContainer.Length.ToString() + "</TimeTotal>");
                    sr.WriteLine("\t<ScoreTotal>" + SaveScoreContainer.Length.ToString() + "</ScoreTotal>");
                    sr.WriteLine("\t<IntTotal>" + SaveIntContainer.Length.ToString() + "</IntTotal>");
                    sr.WriteLine("\t<FloatTotal>" + SaveFloatContainer.Length.ToString() + "</FloatTotal>");
                    sr.WriteLine("\t<StringTotal>" + SaveStringContainer.Length.ToString() + "</StringTotal>");
                    sr.WriteLine("\t<BoolTotal>" + SaveBoolContainer.Length.ToString() + "</BoolTotal>");
                sr.WriteLine("</DataGroup>");

                //=========================== HEALTH 
                if (SaveHealthContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<HealthGroup>");
                    foreach (CHealthContainer saveHealth in SaveHealthContainer)
                    {
                        sr.WriteLine("\t<Health" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveHealth.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>" + saveHealth.HealthVariables.MaxValue + "</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>" + saveHealth.HealthVariables.MinValue + "</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveHealth.HealthVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</Health" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</HealthGroup>");
                }

                //=========================== MANA
                if (SaveManaContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<ManaGroup>");
                    foreach (CManaContainer saveMana in SaveManaContainer)
                    {
                        sr.WriteLine("\t<Mana" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveMana.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>" + saveMana.ManaVariables.MaxValue + "</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>" + saveMana.ManaVariables.MinValue + "</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveMana.ManaVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</Mana" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</ManaGroup>");
                }
                //=========================== EXP
                if (SaveExpContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<EXPGroup>");
                    foreach (CExpContainer saveEXP in SaveExpContainer)
                    {
                        sr.WriteLine("\t<EXP" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveEXP.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>" + saveEXP.ExpVariables.MaxValue + "</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>" + saveEXP.ExpVariables.MinValue + "</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveEXP.ExpVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t\t<ExpLevel>" + saveEXP.ExpVariables.ExpLevel + "</ExpLevel>");
                        sr.WriteLine("\t</EXP" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</EXPGroup>");
                }
                //=========================== TIME
                if (SaveTimeContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<TimeGroup>");
                    foreach (CTimeContainer saveTime in SaveTimeContainer)
                    {
                        sr.WriteLine("\t<Time" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveTime.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>" + saveTime.TimeVariables.MaxValue + "</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>" + saveTime.TimeVariables.MinValue + "</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveTime.TimeVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</Time" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</TimeGroup>");
                }
                //=========================== SCORE
                if (SaveScoreContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<ScoreGroup>");
                    foreach (CScoreContainer saveScore in SaveScoreContainer)
                    {
                        sr.WriteLine("\t<Score" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveScore.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>NONE</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>NONE</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveScore.ScoreVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</Score" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</ScoreGroup>");
                }
                //=========================== INTEGER
                if (SaveIntContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<IntegerGroup>");
                    foreach (CIntContainer saveInteger in SaveIntContainer)
                    {
                        sr.WriteLine("\t<Integer" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveInteger.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>" + saveInteger.IntVariables.MaxValue + "</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>" + saveInteger.IntVariables.MinValue + "</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveInteger.IntVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</Integer" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</IntegerGroup>");
                }
                //=========================== FLOAT
                if (SaveFloatContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<FloatGroup>");
                    foreach (CFloatContainer saveFloat in SaveFloatContainer)
                    {
                        sr.WriteLine("\t<Float" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveFloat.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>" + saveFloat.FloatVariables.MaxValue + "</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>" + saveFloat.FloatVariables.MinValue + "</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveFloat.FloatVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</Float" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</FloatGroup>");
                }
                //=========================== STRING
                if (SaveStringContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<StringGroup>");
                    foreach (CStringContainer saveString in SaveStringContainer)
                    {
                        sr.WriteLine("\t<String" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveString.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>NONE</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>NONE</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveString.StringVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</String" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</StringGroup>");
                }
                //=========================== BOOL
                if (SaveBoolContainer.Length > 0)
                {
                    index = 0;
                    sr.WriteLine("<BoolGroup>");
                    foreach (CBoolContainer saveBool in SaveBoolContainer)
                    {
                        sr.WriteLine("\t<Bool" + index.ToString() + ">");
                        sr.WriteLine("\t\t<UniqueID>" + saveBool.UniqueID + "</UniqueID>");
                        sr.WriteLine("\t\t<MaxValue>NONE</MaxValue>");
                        sr.WriteLine("\t\t<MinValue>NONE</MinValue>");
                        sr.WriteLine("\t\t<CurrentValue>" + saveBool.BoolVariables.CurrentValue + "</CurrentValue>");
                        sr.WriteLine("\t</Bool" + index.ToString() + ">");
                        index++;
                    }
                    sr.WriteLine("</BoolGroup>");
                }
                sr.WriteLine("</DataCollection>");
                sr.Close();

                TerminalSendMessage("Save file " + XMLFile.CurrentValue + ".xml success [VarManager.cs:255]");

            }
        }

        public void TerminalSendMessage(string aValue)
        {
            GameObject terminal = GameObject.Find("Terminal.Model");
            if (terminal != null)
            {
                terminal.SendMessage("MessageLog", aValue);
            }
        }

        public void TerminalSendMessageNoDate(string aValue)
        {
            GameObject terminal = GameObject.Find("Terminal.Model");
            if (terminal != null)
            {
                terminal.SendMessage("MessageLogNoDate", aValue);
            }
        }

        public void LoadData()
        {
            if (StorageType == CStorageType.ByPlayerPref)
            {
                for (int i = 0; i < LoadHealthContainer.Length; i++)
                {
                    LoadHealthContainer[i].HealthVariables.CurrentValue = PlayerPrefs.GetFloat(LoadHealthContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadManaContainer.Length; i++)
                {
                    LoadManaContainer[i].ManaVariables.CurrentValue = PlayerPrefs.GetFloat(LoadManaContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadExpContainer.Length; i++)
                {
                    LoadExpContainer[i].ExpVariables.CurrentValue = PlayerPrefs.GetFloat(LoadExpContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadFloatContainer.Length; i++)
                {
                    LoadFloatContainer[i].FloatVariables.CurrentValue = PlayerPrefs.GetFloat(LoadFloatContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadIntContainer.Length; i++)
                {
                    LoadIntContainer[i].IntVariables.CurrentValue = PlayerPrefs.GetInt(LoadIntContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadStringContainer.Length; i++)
                {
                    LoadStringContainer[i].StringVariables.CurrentValue = PlayerPrefs.GetString(LoadStringContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadTimeContainer.Length; i++)
                {
                    LoadTimeContainer[i].TimeVariables.CurrentValue = PlayerPrefs.GetInt(LoadTimeContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadScoreContainer.Length; i++)
                {
                    LoadScoreContainer[i].ScoreVariables.CurrentValue = PlayerPrefs.GetFloat(LoadScoreContainer[i].UniqueID);
                }
                for (int i = 0; i < LoadBoolContainer.Length; i++)
                {
                    LoadBoolContainer[i].BoolVariables.CurrentValue = bool.Parse(PlayerPrefs.GetString(LoadBoolContainer[i].UniqueID));
                }
            }
            if (StorageType == CStorageType.ByXML)
            {
                string xmlfile = DataDirectory() + XMLFile.CurrentValue + ".xml";
                if (File.Exists(xmlfile))
                {
                    string xmlfile_result = System.IO.File.ReadAllText(xmlfile);

                    XmlDocument xmldoc;
                    XmlNodeList xmlnodelist;
                    XmlNode xmlnode;
                    xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xmlfile_result);

                    List<int> group_index = new List<int>();
                    
                    xmlnodelist = xmldoc.GetElementsByTagName("DataGroup");
                    for (int i = 0; i <= xmlnodelist.Count - 1; i++)
                    {
                        //-- health
                        xmlnode = xmlnodelist.Item(i);
                        XmlNode currentNode = xmlnode.FirstChild;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- mana
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- exp
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- time
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- score
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- integer
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- float
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- string
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));

                        //-- bool
                        currentNode = currentNode.NextSibling;
                        group_index.Add(int.Parse(currentNode.InnerText));
                    }

                    //========================================= HEALTH
                    if (LoadHealthContainer.Length > 0)
                    {
                        TerminalSendMessage("Trying Load HEALTH (Length: " + LoadHealthContainer.Length.ToString() + ")");
                        if (LoadHealthContainer.Length == group_index[VarConstant.HEALTH])
                        {
                            TerminalSendMessageNoDate("\tTotal data: " + LoadHealthContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.HEALTH]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Health" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadHealthContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadHealthContainer[i].HealthVariables.MaxValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMaxValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadHealthContainer[i].HealthVariables.MinValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMinValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadHealthContainer[i].HealthVariables.CurrentValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }

                    //========================================= MANA
                    TerminalSendMessage("Trying Load MANA (Length: " + LoadManaContainer.Length.ToString() + ")");
                    if (LoadManaContainer.Length > 0)
                    {
                        if (LoadManaContainer.Length == group_index[VarConstant.MANA])
                        {
                            TerminalSendMessageNoDate("Total data: " + LoadManaContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.MANA]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Mana" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadManaContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadManaContainer[i].ManaVariables.MaxValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMaxValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadManaContainer[i].ManaVariables.MinValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMinValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadManaContainer[i].ManaVariables.CurrentValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }

                    //========================================= EXP
                    TerminalSendMessage("Trying Load EXP (Length: " + LoadExpContainer.Length.ToString() + ")");
                    if (LoadExpContainer.Length > 0)
                    {
                        if (LoadExpContainer.Length == group_index[VarConstant.EXP])
                        {
                            TerminalSendMessageNoDate("Total data: " + LoadExpContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.EXP]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Exp" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadExpContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadExpContainer[i].ExpVariables.MaxValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMaxValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadExpContainer[i].ExpVariables.MinValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMinValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadExpContainer[i].ExpVariables.CurrentValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }

                    //========================================= TIME
                    TerminalSendMessage("Trying Load TIME (Length: " + LoadTimeContainer.Length.ToString() + ")");
                    if (LoadTimeContainer.Length > 0)
                    {
                        if (LoadTimeContainer.Length == group_index[VarConstant.TIME])
                        {
                            TerminalSendMessageNoDate("\tTotal data: " + LoadTimeContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.TIME]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Time" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadTimeContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadTimeContainer[i].TimeVariables.MaxValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMaxValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadTimeContainer[i].TimeVariables.MinValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMinValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadTimeContainer[i].TimeVariables.CurrentValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }

                    //========================================= SCORE
                    TerminalSendMessage("Trying Load SCORE (Length: " + LoadScoreContainer.Length.ToString() + ")");
                    if (LoadScoreContainer.Length > 0)
                    {
                        if (LoadScoreContainer.Length == group_index[VarConstant.SCORE])
                        {
                            TerminalSendMessageNoDate("\tTotal data: " + LoadScoreContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.SCORE]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Score" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadScoreContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling; //none
                                currentNode = currentNode.NextSibling; //none

                                currentNode = currentNode.NextSibling;
                                LoadScoreContainer[i].ScoreVariables.CurrentValue = float.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }

                    //========================================= INT
                    TerminalSendMessage("Trying Load INT (Length: " + LoadIntContainer.Length.ToString() + ")");
                    if (LoadIntContainer.Length > 0)
                    {
                        if (LoadIntContainer.Length == group_index[VarConstant.INT])
                        {
                            TerminalSendMessageNoDate("\tTotal data: " + LoadIntContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.INT]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Integer" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadIntContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadIntContainer[i].IntVariables.MaxValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMaxValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadIntContainer[i].IntVariables.MinValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMinValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadIntContainer[i].IntVariables.CurrentValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }
                    //========================================= FLOAT
                    TerminalSendMessage("Trying Load FLOAT (Length: " + LoadFloatContainer.Length.ToString() + ")");
                    if (LoadFloatContainer.Length > 0)
                    {
                        if (LoadFloatContainer.Length == group_index[VarConstant.FLOAT])
                        {
                            TerminalSendMessageNoDate("\tTotal data: " + LoadFloatContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.FLOAT]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Float" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadFloatContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadFloatContainer[i].FloatVariables.MaxValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMaxValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadFloatContainer[i].FloatVariables.MinValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tMinValue: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                LoadFloatContainer[i].FloatVariables.CurrentValue = int.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }

                    //========================================= STRING
                    TerminalSendMessage("Trying Load STRING (Length: " + LoadStringContainer.Length.ToString() + ")");
                    if (LoadStringContainer.Length > 0)
                    {
                        if (LoadStringContainer.Length == group_index[VarConstant.STRING])
                        {
                            TerminalSendMessageNoDate("\tTotal data: " + LoadStringContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.STRING]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("String" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadStringContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                currentNode = currentNode.NextSibling;

                                currentNode = currentNode.NextSibling;
                                LoadStringContainer[i].StringVariables.CurrentValue = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }

                    //========================================= BOOL
                    TerminalSendMessage("Trying Load BOOL (Length: " + LoadBoolContainer.Length.ToString() + ")");
                    if (LoadBoolContainer.Length > 0)
                    {
                        if (LoadBoolContainer.Length == group_index[VarConstant.BOOL])
                        {
                            TerminalSendMessageNoDate("\tTotal data: " + LoadBoolContainer.Length.ToString());
                            for (int i = 0; i < group_index[VarConstant.BOOL]; i++)
                            {
                                TerminalSendMessageNoDate("\tIndex: " + i.ToString());
                                xmlnodelist = xmldoc.GetElementsByTagName("Bool" + i.ToString());
                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;
                                LoadBoolContainer[i].UniqueID = currentNode.InnerText;
                                TerminalSendMessageNoDate("\tUniqueID: " + currentNode.InnerText);

                                currentNode = currentNode.NextSibling;
                                currentNode = currentNode.NextSibling;

                                currentNode = currentNode.NextSibling;
                                LoadBoolContainer[i].BoolVariables.CurrentValue = bool.Parse(currentNode.InnerText);
                                TerminalSendMessageNoDate("\tCurrentValue: " + currentNode.InnerText);
                            }
                        }
                    }


                }
            }
        }

    }
}

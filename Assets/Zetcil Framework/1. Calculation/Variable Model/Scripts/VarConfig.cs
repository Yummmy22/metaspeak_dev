using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.Events;

namespace Zetcil
{ 

    public class VarConfig : MonoBehaviour
    {
        public enum CLanguageType { None, Indonesian, English, Arabic, Korean, Japanese, Chinese }

        [Space(10)]
        public bool isEnabled;

        [Header("Output Directory")]
        [TextArea(5, 10)]
        public string DataPath; 

        [Header("Directory Settings")]
        public string ConfigDirectory = "Config";
        public string CornerDirectory = "Corner";
        public string LanguageDirectory = "Languages";
        public string NotificationDirectory = "Notification";
        public string VisualNovelDirectory = "VisualNovel";
        //--dynamic data
        public string SessionDirectory = "Session";
        public string DataSessionDirectory = "Data";

        TextAsset configAsset;
        TextAsset cornerAsset;
        TextAsset languageAsset;
        TextAsset notificationAsset;
        TextAsset visualNovelAsset;

        TextAsset sessionAsset;
        TextAsset dataAsset;

        // Start is called before the first frame update
        void Awake()
        {
            if (isEnabled)
            {
                DataPath = Application.persistentDataPath;
                InitializeConfig();
                InitializeLanguage();
                InitializeSession();
                InitializeDataSession();
                InitializeCorner();
                InitializeNotification();
                InitializeVisualNovel();
            }
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }


        public string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        public bool IsConfigExists(string aDirectory, string aFile)
        {
            bool result = false;
            string fileName = Application.persistentDataPath + "/" + aDirectory + "/" + aFile + ".xml";
            if (File.Exists(fileName))
            {
                result = true;
            }
            return result;
        }

        public bool IsSessionExists(string aDirectory, string aFile)
        {
            bool result = false;
            string fileName = Application.persistentDataPath + "/" + aDirectory + "/" + aFile + ".xml";
            if (File.Exists(fileName))
            {
                result = true;
            }
            return result;
        }

        public string GetDataSessionDirectory()
        {
            return GetDirectory(DataSessionDirectory);
        }

        public string GetSessionDirectory()
        {
            return GetDirectory(SessionDirectory);
        }

        public string GetConfigDirectory()
        {
            return GetDirectory(ConfigDirectory);
        }

        public string GetCornerDirectory()
        {
            return GetDirectory(CornerDirectory);
        }

        public string GetLanguageDirectory()
        {
            return GetDirectory(LanguageDirectory);
        }

        public string GetNotificationDirectory()
        {
            return GetDirectory(NotificationDirectory);
        }

        public string GetVisualNovelDirectory()
        {
            return GetDirectory(VisualNovelDirectory);
        }

        public string SetXMLOpenTag(string aTag, int aTab = 1)
        {
            string opentag;
            string tabparent = "\t";
            if (aTab == 2)
            {
                tabparent = "\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
            }

            opentag = tabparent + "<" + aTag + ">\n";
            return opentag;
        }

        public string SetXMLCloseTag(string aTag, int aTab = 1)
        {
            string closetag;
            string tabparent = "\t";
            if (aTab == 2)
            {
                tabparent = "\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
            }

            closetag = tabparent + "</" + aTag + ">\n";
            return closetag;
        }

        public string SetXMLValueSingle(string aTag, string aValue, int aTab = 1)
        {
            string opentag, contenttag, closetag;
            string tabparent = "\t";
            if (aTab == 2)
            {
                tabparent = "\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
            }
            opentag = tabparent + "<" + aTag + ">";
            contenttag = aValue;
            closetag = "</" + aTag + ">\n";

            return opentag + contenttag + closetag;
        }

        public string SetXMLValue(string aTag, string aValue, int aTab = 1, bool aUseNewRow = true)
        {
            string opentag, contenttag, closetag;
            string tabparent = "\t";
            string tabchild = "\t\t";
            string newrow = "\n";
            if (aTab == 2)
            {
                tabparent = "\t\t";
                tabchild = "\t\t\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
                tabchild = "\t\t\t\t\t\t";
            }

            if (!aUseNewRow)
            {
                newrow = "";
            }

            opentag = tabparent + "<" + aTag + ">" + newrow;
            contenttag = tabchild + aValue + newrow;
            closetag = tabparent + "</" + aTag + ">" + newrow;

            return opentag + contenttag + closetag;
        }

        public void InitializeConfig()
        {
            configAsset = (TextAsset)Resources.Load(ConfigDirectory + "/Audio", typeof(TextAsset));
            if (!IsConfigExists(ConfigDirectory, "Audio"))
            {
                SaveConfigFile("Audio", configAsset.ToString());
            }

            configAsset = (TextAsset)Resources.Load(ConfigDirectory + "/Graphic", typeof(TextAsset));
            if (!IsConfigExists(ConfigDirectory, "Graphic"))
            {
                SaveConfigFile("Graphic", configAsset.ToString());
            }

            configAsset = (TextAsset)Resources.Load(ConfigDirectory + "/Language", typeof(TextAsset));
            if (!IsConfigExists(ConfigDirectory, "Language"))
            {
                SaveConfigFile("Language", configAsset.ToString());
            }

            configAsset = (TextAsset)Resources.Load(ConfigDirectory + "/Layout", typeof(TextAsset));
            if (!IsConfigExists(ConfigDirectory, "Layout"))
            {
                SaveConfigFile("Layout", configAsset.ToString());
            }
        }

        public void SaveConfigFile(string aConfigID, string aConfigData)
        {
            string DirName = GetDirectory(ConfigDirectory);
            var sr = File.CreateText(DirName + aConfigID + ".xml");
            sr.WriteLine(aConfigData);
            sr.Close();
        }

        public void InitializeSession()
        {
            sessionAsset = (TextAsset)Resources.Load(SessionDirectory + "/Player", typeof(TextAsset));
            if (!IsSessionExists(SessionDirectory, "Player"))
            {
                SaveSessionFile("Player", sessionAsset.ToString());
            }
        }

        public void SaveSessionFile(string aSessionID, string aSessionData)
        {
            string DirName = GetDirectory(SessionDirectory);
            var sr = File.CreateText(DirName + aSessionID + ".xml");
            sr.WriteLine(aSessionData);
            sr.Close();
        }

        public void InitializeDataSession()
        {
            //-- 1
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.SessionName", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.SessionName"))
            {
                SaveDataSessionFile("Default.SessionName", dataAsset.ToString());
            }

            //-- 2
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentID", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentID"))
            {
                SaveDataSessionFile("Default.CurrentID", dataAsset.ToString());
            }

            //-- 3
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentLevel", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentLevel"))
            {
                SaveDataSessionFile("Default.CurrentLevel", dataAsset.ToString());
            }

            //-- 4
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentScore", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentScore"))
            {
                SaveDataSessionFile("Default.CurrentScore", dataAsset.ToString());
            }

            //-- 5
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentEnergy", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentEnergy"))
            {
                SaveDataSessionFile("Default.CurrentEnergy", dataAsset.ToString());
            }

            //-- 6
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentCoin", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentCoin"))
            {
                SaveDataSessionFile("Default.CurrentCoin", dataAsset.ToString());
            }

            //-- 7
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentDiamond", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentDiamond"))
            {
                SaveDataSessionFile("Default.CurrentDiamond", dataAsset.ToString());
            }

            //-- 8
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentCharacter", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentCharacter"))
            {
                SaveDataSessionFile("Default.CurrentCharacter", dataAsset.ToString());
            }

            //-- 9
            dataAsset = (TextAsset)Resources.Load(DataSessionDirectory + "/Default.CurrentUser", typeof(TextAsset));
            if (!IsSessionExists(DataSessionDirectory, "Default.CurrentUser"))
            {
                SaveDataSessionFile("Default.CurrentUser", dataAsset.ToString());
            }
        }

        public void SaveDataSessionFile(string aSessionID, string aSessionData)
        {
            string DirName = GetDirectory(DataSessionDirectory);
            var sr = File.CreateText(DirName + aSessionID + ".xml");
            sr.WriteLine(aSessionData);
            sr.Close();
        }

        public void InitializeCorner()
        {
            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/English", typeof(TextAsset));
            SaveCornerFile("English", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Indonesian", typeof(TextAsset));
            SaveCornerFile("Indonesian", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Arabic", typeof(TextAsset));
            SaveCornerFile("Arabic", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Korean", typeof(TextAsset));
            SaveCornerFile("Korean", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Japanese", typeof(TextAsset));
            SaveCornerFile("Japanese", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Chinese", typeof(TextAsset));
            SaveCornerFile("Chinese", cornerAsset.ToString());
        }

        public void SaveCornerFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(CornerDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close();
        }

        public void InitializeLanguage()
        {
            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/English", typeof(TextAsset));
            SaveLanguageFile("English", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Indonesian", typeof(TextAsset));
            SaveLanguageFile("Indonesian", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Arabic", typeof(TextAsset));
            SaveLanguageFile("Arabic", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Korean", typeof(TextAsset));
            SaveLanguageFile("Korean", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Japanese", typeof(TextAsset));
            SaveLanguageFile("Japanese", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Chinese", typeof(TextAsset));
            SaveLanguageFile("Chinese", languageAsset.ToString());
        }

        public void SaveLanguageFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(LanguageDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close();
        }

        public void InitializeNotification()
        {
            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/English", typeof(TextAsset));
            SaveNotificationFile("English", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Indonesian", typeof(TextAsset));
            SaveNotificationFile("Indonesian", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Arabic", typeof(TextAsset));
            SaveNotificationFile("Arabic", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Korean", typeof(TextAsset));
            SaveNotificationFile("Korean", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Japanese", typeof(TextAsset));
            SaveNotificationFile("Japanese", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Chinese", typeof(TextAsset));
            SaveNotificationFile("Chinese", notificationAsset.ToString());
        }

        public void SaveNotificationFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(NotificationDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close();
        }

        public void InitializeVisualNovel()
        {
            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/English", typeof(TextAsset));
            SaveVisualNovelFile("English", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Indonesian", typeof(TextAsset));
            SaveVisualNovelFile("Indonesian", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Arabic", typeof(TextAsset));
            SaveVisualNovelFile("Arabic", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Korean", typeof(TextAsset));
            SaveVisualNovelFile("Korean", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Japanese", typeof(TextAsset));
            SaveVisualNovelFile("Japanese", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Chinese", typeof(TextAsset));
            SaveVisualNovelFile("Chinese", visualNovelAsset.ToString());
        }

        public void SaveVisualNovelFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(VisualNovelDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close(); 
        }
    }
} 
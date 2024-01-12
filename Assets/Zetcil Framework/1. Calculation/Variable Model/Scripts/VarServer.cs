using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarServer : MonoBehaviour
    {
        public enum COperationType { None, Initialize, Runtime }

        [Space(10)]
        public bool isEnabled;

        [Header("Operation Settings")]
        public COperationType OperationType;

        [Header("Server Settings")]
        public VarString ConnectURL;
        public VarString LoginURL;
        public VarString RegisterURL;
        public VarString HighScoreURL;
        public VarString ForgotURL;
        public VarString LicenseURL;
        public VarString ActivityURL;
        public VarString ProgressURL;
        public VarString LevelURL;
        public VarString StarURL;
        public VarString ScoreURL;
        public VarString EnergyURL;
        public VarString DiamondURL;

        [Header("Server Event Settings")]
        public UnityEvent ServerEvent;

        string ConfigDirectory = "Config";

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        public void SaveFile()
        {
            string header = "<ServerSettings>\n";
            string footer = "</ServerSettings>";
            string result = "";

            string opentag0 = "\t<" + "ConnectURL" + ">\n";
            string contenttag0 = "\t\t" + ConnectURL.CurrentValue + "\n";
            string closetag0 = "\t</" + "ConnectURL" + ">\n";

            string opentag1 = "\t<" + "LoginURL" + ">\n";
            string contenttag1 = "\t\t" + LoginURL.CurrentValue + "\n";
            string closetag1 = "\t</" + "LoginURL" + ">\n";

            string opentag2 = "\t<" + "RegisterURL" + ">\n";
            string contenttag2 = "\t\t" + RegisterURL.CurrentValue + "\n";
            string closetag2 = "\t</" + "RegisterURL" + ">\n";

            string opentag3 = "\t<" + "HighScoreURL" + ">\n";
            string contenttag3 = "\t\t" + HighScoreURL.CurrentValue + "\n";
            string closetag3 = "\t</" + "HighScoreURL" + ">\n";

            string opentag4 = "\t<" + "ForgotURL" + ">\n";
            string contenttag4 = "\t\t" + ForgotURL.CurrentValue + "\n";
            string closetag4 = "\t</" + "ForgotURL" + ">\n";

            string opentag5 = "\t<" + "LicenseURL" + ">\n";
            string contenttag5 = "\t\t" + LicenseURL.CurrentValue + "\n";
            string closetag5 = "\t</" + "LicenseURL" + ">\n";

            string opentag6 = "\t<" + "ActivityURL" + ">\n";
            string contenttag6 = "\t\t" + ActivityURL.CurrentValue + "\n";
            string closetag6 = "\t</" + "ActivityURL" + ">\n";

            string opentag7 = "\t<" + "ProgressURL" + ">\n";
            string contenttag7 = "\t\t" + ProgressURL.CurrentValue + "\n";
            string closetag7 = "\t</" + "ProgressURL" + ">\n";

            string opentag8 = "\t<" + "LevelURL" + ">\n";
            string contenttag8 = "\t\t" + LevelURL.CurrentValue + "\n";
            string closetag8 = "\t</" + "LevelURL" + ">\n";

            string opentag9 = "\t<" + "StarURL" + ">\n";
            string contenttag9 = "\t\t" + StarURL.CurrentValue + "\n";
            string closetag9 = "\t</" + "StarURL" + ">\n";

            string opentag10 = "\t<" + "ScoreURL" + ">\n";
            string contenttag10 = "\t\t" + ScoreURL.CurrentValue + "\n";
            string closetag10 = "\t</" + "ScoreURL" + ">\n";

            string opentag11 = "\t<" + "EnergyURL" + ">\n";
            string contenttag11 = "\t\t" + EnergyURL.CurrentValue + "\n";
            string closetag11 = "\t</" + "EnergyURL" + ">\n";

            string opentag12 = "\t<" + "DiamondURL" + ">\n";
            string contenttag12 = "\t\t" + DiamondURL.CurrentValue + "\n";
            string closetag12 = "\t</" + "DiamondURL" + ">\n";

            result = opentag0 + contenttag0 + closetag0 +
                     opentag1 + contenttag1 + closetag1 +
                     opentag2 + contenttag2 + closetag2 +
                     opentag3 + contenttag3 + closetag3 +
                     opentag4 + contenttag4 + closetag4 +
                     opentag5 + contenttag5 + closetag5 +
                     opentag6 + contenttag6 + closetag6 +
                     opentag7 + contenttag7 + closetag7 +
                     opentag8 + contenttag8 + closetag8 +
                     opentag9 + contenttag9 + closetag9 +
                     opentag10 + contenttag10 + closetag10 +
                     opentag11 + contenttag11 + closetag11 +
                     opentag12 + contenttag12 + closetag12;
            result = header + result + footer;

            string DirName = GetDirectory(ConfigDirectory);
            var sr = File.CreateText(DirName + "Server.xml");
            sr.WriteLine(result);
            sr.Close();
        }

        public void LoadFile()
        {
            string FullPathFile = GetDirectory(ConfigDirectory) + "Server.xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName("ConnectURL");
                ConnectURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("LoginURL");
                LoginURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("RegisterURL");
                RegisterURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("HighScoreURL");
                HighScoreURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("ForgotURL");
                ForgotURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("LicenseURL");
                LicenseURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("ActivityURL");
                ActivityURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("ProgressURL");
                ProgressURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("LevelURL");
                LevelURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("StarURL");
                StarURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("ScoreURL");
                ScoreURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("EnergyURL");
                EnergyURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                xmlnodelist = xmldoc.GetElementsByTagName("DiamondURL");
                DiamondURL.CurrentValue = xmlnodelist.Item(0).InnerText.Trim();

                ServerEvent.Invoke();
            }
        }

        void Awake()
        {
            if (isEnabled)
            {
                if (OperationType == COperationType.Initialize)
                {
                    SaveFile();
                    LoadFile();
                }
                if (OperationType == COperationType.Runtime)
                {
                    LoadFile();
                }
            }
        }
    }
}

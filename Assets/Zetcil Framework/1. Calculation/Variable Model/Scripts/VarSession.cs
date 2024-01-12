using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

using TechnomediaLabs;

namespace Zetcil
{
    public class VarSession : MonoBehaviour
    {
        public enum COperationType { None, Initialize, Runtime }

        [Space(10)]
        public bool isEnabled;

        [Header("Operation Settings")]
        public COperationType OperationType;

        [Header("Session Settings")]
        public VarString Username;

        string ConfigDirectory = "Config";

        string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (OperationType == COperationType.Initialize)
                {
                    SaveFile();
                }
                if (OperationType == COperationType.Runtime)
                {
                    LoadFile();
                }
            }

        }

        string SaveCurrentSession()
        {
            string result = Username.CurrentValue;
            return result;
        }

        void LoadCurrentSession(string aSession)
        {
            Username.CurrentValue = aSession;
        }

        public void SaveFile()
        {
            string header = "<SessionSettings>\n";
            string footer = "</SessionSettings>";
            string result = "";

            string opentag = "\t<" + "Username" + ">\n";
            string contenttag = "\t\t" + SaveCurrentSession() + "\n";
            string closetag = "\t</" + "Username" + ">\n";

            result = opentag + contenttag + closetag;
            result = header + result + footer;

            string DirName = GetDirectory(ConfigDirectory);
            var sr = File.CreateText(DirName + "Session.xml");
            sr.WriteLine(result);
            sr.Close();
        }

        public void LoadFile()
        {
            string FullPathFile = GetDirectory(ConfigDirectory) + "Session.xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName("Username");
                LoadCurrentSession(xmlnodelist.Item(0).InnerText.Trim());
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;

namespace Zetcil
{

    public class VarXML : MonoBehaviour
    {
        public enum COperationType { None, Save, Load }

        [System.Serializable]
        public class CXMLContent
        {
            public string XMLTag;
            public VarString XMLValue;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("File Settings")]
        public GlobalVariable.CInvokeType InvokeType;
        public COperationType OperationType;
        public string DirectoryName;
        public string FileName;

        [Header("XML Settings")]
        public List<CXMLContent> ContentValue;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay = 0;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval = 0;

        void Start()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
                {
                    ExecuteFileOperation();
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
                {
                    Invoke("ExecuteFileOperation", Delay);
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
                {
                    InvokeRepeating("ExecuteFileOperation", Interval, 1);
                }
            }
        }

        void ExecuteFileOperation()
        {
            if (OperationType == COperationType.Save)
            {
                SaveFile();
            }
            if (OperationType == COperationType.Load)
            {
                LoadFile();
            }
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
            string header = "<DATAObjectCollection>\n";
            string footer = "</DATAObjectCollection>";
            string result = "";

            for (int i = 0; i < ContentValue.Count; i++)
            {
                string opentag = "\t<"+ContentValue[i].XMLTag+">\n";
                string contenttag = "\t\t" + ContentValue[i].XMLValue.CurrentValue+"\n";
                string closetag = "\t</" + ContentValue[i].XMLTag + ">\n";
                result += opentag + contenttag + closetag;
            }

            result = header + result + footer;

            string DirName = GetDirectory(DirectoryName);
            var sr = File.CreateText(DirName + FileName);
            sr.WriteLine(result);
            sr.Close();
        }

        public void LoadFile()
        {
            string FullPathFile = GetDirectory(DirectoryName) + FileName;
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                for (int i = 0; i < ContentValue.Count; i++)
                {
                    xmlnodelist = xmldoc.GetElementsByTagName(ContentValue[i].XMLTag);
                    ContentValue[i].XMLValue.CurrentValue = xmlnodelist.Item(i).InnerText.Trim();
                }
            }
        }
    }
}

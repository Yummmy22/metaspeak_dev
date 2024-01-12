using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Zetcil
{

    public class VarFile : MonoBehaviour
    {
        public enum COperationType { None, Save, Load }

        [Space(10)]
        public bool isEnabled;

        [Header("File Settings")]
        public GlobalVariable.CInvokeType InvokeType;
        public COperationType OperationType;
        public string DirectoryName;
        public string FileName;
        public VarString ContentValue;

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
            string DirName = GetDirectory(DirectoryName);
            var sr = File.CreateText(DirName + FileName);
            sr.WriteLine(ContentValue.CurrentValue);
            sr.Close();
        }

        public void LoadFile()
        {
            string result = "NULL";
            string FullPathFile = GetDirectory(DirectoryName) + FileName;
            if (File.Exists(FullPathFile))
            {
                string temp = System.IO.File.ReadAllText(FullPathFile);
                result = temp;
            }
            ContentValue.CurrentValue = result;
        }
    }
}

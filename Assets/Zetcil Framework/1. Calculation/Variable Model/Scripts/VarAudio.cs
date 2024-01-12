using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;


namespace Zetcil
{
    public class VarAudio : MonoBehaviour
    {
        public enum COperationType { None, Initialize, Runtime }

        [Space(10)]
        public bool isEnabled;

        [Header("Operation Settings")]
        public COperationType OperationType;

        [Header("Audio Settings")]
        [Range(0,1)] public float SoundVolume = 1;

        [Header("Slider Settings")]
        public Slider TargetSlider;

        string ConfigDirectory = "Config";
        string SoundDirectory = "Audio";

        // Start is called before the first frame update
        void Start()
        {
            if (OperationType == COperationType.Runtime)
            {
                LoadFile();
            }
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

        public void SaveSoundFile(string aSoundID, string aSoundData)
        {
            string DirName = GetDirectory(SoundDirectory);
            var sr = File.CreateText(DirName + aSoundID + ".xml");
            sr.WriteLine(aSoundData);
            sr.Close();
        }

        public void SaveFile()
        {
            string header = "<AudioSettings>\n";
            string footer = "</AudioSettings>";
            string result = "";

            string opentag = "\t<" + "Volume" + ">\n";
            string contenttag = "\t\t" + SoundVolume.ToString() + "\n";
            string closetag = "\t</" + "Volume" + ">\n";

            result = opentag + contenttag + closetag;
            result = header + result + footer;

            string DirName = GetDirectory(ConfigDirectory);
            var sr = File.CreateText(DirName + "Audio.xml");
            sr.WriteLine(result);
            sr.Close();
        }

        public void LoadFile()
        {
            string FullPathFile = GetDirectory(ConfigDirectory) + "Audio.xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName("Volume");
                SoundVolume = float.Parse(xmlnodelist.Item(0).InnerText.Trim());
                if (TargetSlider != null)
                {
                    TargetSlider.value = SoundVolume;
                }
                SetAllSound();
            }
        }

        void SetAllSound()
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Audio");
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].GetComponent<AudioSource>().volume = SoundVolume;
            }

        }

        public void SetSoundSlider()
        {
            SoundVolume = TargetSlider.value;
        }

        void Awake()
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
    }
}

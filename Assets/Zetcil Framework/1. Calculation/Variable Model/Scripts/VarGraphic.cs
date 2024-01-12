using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;


namespace Zetcil
{
    public class VarGraphic : MonoBehaviour
    {
        public enum COperationType { None, Initialize, Runtime }

        [Space(10)]
        public bool isEnabled;

        [Header("Operation Settings")]
        public COperationType OperationType;

        [Header("Graphic Settings")]
        public bool AntiAlias;
        public bool Reflection;
        public bool LightCorrection;

        string ConfigDirectory = "Config";
        string GraphicDirectory = "Graphic";

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

        public void SaveGraphicFile(string aGraphicID, string aGraphicData)
        {
            string DirName = GetDirectory(GraphicDirectory);
            var sr = File.CreateText(DirName + aGraphicID + ".xml");
            sr.WriteLine(aGraphicData);
            sr.Close();
        }

        public void SaveFile()
        {
            string header = "<GraphicSettings>\n";
            string footer = "</GraphicSettings>";
            string result = "";

            string antialias = "\t<" + "AntiAlias" + ">\n" + "\t\t" + AntiAlias.ToString() + "\n" + "\t</" + "AntiAlias" + ">\n";
            string reflection = "\t<" + "Reflection" + ">\n" + "\t\t" + Reflection.ToString() + "\n" + "\t</" + "Reflection" + ">\n";
            string lightCorrection = "\t<" + "LightCorrection" + ">\n" + "\t\t" + LightCorrection.ToString() + "\n" + "\t</" + "LightCorrection" + ">\n";

            result = antialias + reflection + lightCorrection;
            result = header + result + footer;

            string DirName = GetDirectory(ConfigDirectory);
            var sr = File.CreateText(DirName + "Graphic.xml");
            sr.WriteLine(result);
            sr.Close();
        }

        public void LoadFile()
        {
            string FullPathFile = GetDirectory(ConfigDirectory) + "Graphic.xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName("AntiAlias");
                AntiAlias = bool.Parse(xmlnodelist.Item(0).InnerText.Trim());

                xmlnodelist = xmldoc.GetElementsByTagName("Reflection");
                Reflection = bool.Parse(xmlnodelist.Item(0).InnerText.Trim());

                xmlnodelist = xmldoc.GetElementsByTagName("LightCorrection");
                LightCorrection = bool.Parse(xmlnodelist.Item(0).InnerText.Trim());

                SetAllGraphic();
            }
        }

        public void SetAntiAlias(Toggle aBooleanUI)
        {
            AntiAlias = aBooleanUI.isOn;
        }

        public void SetReflection(Toggle aBooleanUI)
        {
            Reflection = aBooleanUI.isOn;
        }

        public void SetLightCorrection(Toggle aBooleanUI)
        {
            LightCorrection = aBooleanUI.isOn;
        }

        void SetAllGraphic()
        {
            //GameObject[] temp = GameObject.FindGameObjectsWithTag("AntiAlias");
            //for (int i = 0; i < temp.Length; i++)
            //{
                //-- nanti lagi
            //}

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

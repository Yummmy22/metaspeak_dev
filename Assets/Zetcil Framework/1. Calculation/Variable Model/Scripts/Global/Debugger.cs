using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Zetcil
{

    public class Debugger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void SaveStatic(string aValue)
        {
            string filename = "data.log";
            string currtime = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            string path = Application.persistentDataPath + "/" + filename;
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(currtime + " : " + aValue);
            writer.Close();
        }

        public static void Save(string aValue, string aFilename = "", string aLine = "")
        {
            string filename = System.DateTime.Now.ToString("yyyy_MM_dd") + "_data.log";
            string currtime = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            string path = Application.persistentDataPath + "/" + filename;
            StreamWriter writer = new StreamWriter(path, true);
            if (aFilename == "")
            {
                writer.WriteLine(currtime + " : " + aValue);
                
            } else
            {
                writer.WriteLine(currtime + " : " + aValue + "[" + aFilename + ":" +aLine + "]");
            }
            writer.Close();
        }
    }
}

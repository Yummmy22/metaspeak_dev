using System;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace AUP.Editor
{
    public class SpeechTTSManifestModifier : MonoBehaviour
    {
        private static string[] _speechTTsPermissions = new string[6]
        {
            "android.permission.INTERNET",
            "android.permission.READ_EXTERNAL_STORAGE",
            "android.permission.READ_INTERNAL_STORAGE",
            "android.permission.ACCESS_NETWORK_STATE",
            "android.permission.WRITE_EXTERNAL_STORAGE",
            "android.permission.RECORD_AUDIO"
        };
        
        #region Methods
        [MenuItem("Window/AUP/SpeechTTS/Add SpeechTTS Android Manifest Permissions")]
        private static void AddSpeechTTsPermissions()
        {
            // this assumes that android manifest exists on Assets/Plugins/Android
            string filename = $"{Application.dataPath}/Plugins/Android/AndroidManifest.xml";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);

            foreach (var permission in _speechTTsPermissions)
            {
                if (!GetNode(xmlDocument, "uses-permission", new string[1]
                {
                    permission
                }, true))
                {
                    xmlDocument.DocumentElement?.AppendChild(CreateNodeWithAttribute(xmlDocument, "uses-permission",
                        "android", "name",
                        "http://schemas.android.com/apk/res/android", permission));

                    xmlDocument.Save(filename);
                    Debug.Log($"<color=green>Permission {permission} added!</color>");
                }
                else
                {
                    Debug.Log($"<color=red>Permission: {permission} already exists</color>");
                }
            }
        }

        private static XmlNode GetActivityMainNode(XmlDocument xmlDocument, string tagName, string value)
        {
            XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName(tagName);
            XmlNode activityNode = null;
            for (int i = 0; i < xmlNodes.Count; i++)
            {
                if (xmlNodes[i].Attributes.Count > 0)
                {
                    if (xmlNodes[i].Attributes[0].Value.Equals(value, StringComparison.Ordinal))
                    {
                        Debug.Log($"Activity: {xmlNodes[i].Attributes[0].Value}");
                        activityNode = xmlNodes[i];
                        break;
                    }
                }
            }

            return activityNode;
        }

        private static bool GetIntentFilter(XmlDocument xmlDocument, string tagName, string[] values, bool debug)
        {
            XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName(tagName);
            bool found = false;
            int matchCount = 0;
            foreach (XmlNode node in xmlNodes)
            {
                matchCount = 0;
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Attributes.Count > 0)
                    {
                        for (int k = 0; k < values.Length; k++)
                        {
                            if (debug)
                            {
                                Debug.Log($"Activity: {childNode.Attributes[0].Value}");
                            }

                            if (childNode.Attributes[0].Value.Equals(values[k], StringComparison.Ordinal))
                            {
                                matchCount++;
                                if (matchCount >= values.Length)
                                {
                                    found = true;
                                    Debug.Log($"Match found!");
                                    if (debug)
                                    {
                                        Debug.Log($"Activity: {childNode.Attributes[0].Value}");
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return found;
        }

        private static bool GetNode(XmlDocument xmlDocument, string tagName, string[] values, bool debug)
        {
            XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName(tagName);
            bool found = false;
            int matchCount = 0;
            foreach (XmlNode node in xmlNodes)
            {
                matchCount = 0;
                
                    if (node.Attributes.Count > 0)
                    {
                        for (int k = 0; k < values.Length; k++)
                        {
                            if (debug)
                            {
                                Debug.Log($"Activity: {node.Attributes[0].Value}");
                            }

                            if (node.Attributes[0].Value.Equals(values[k], StringComparison.Ordinal))
                            {
                                matchCount++;
                                if (matchCount >= values.Length)
                                {
                                    found = true;
                                    Debug.Log($"Match found!");
                                    if (debug)
                                    {
                                        Debug.Log($"Activity: {node.Attributes[0].Value}");
                                    }

                                    break;
                                }
                            }
                        }
                    }
                
            }

            return found;
        }

        private static XmlNode CreateNode(XmlDocument doc, string nodeName)
        {
            var node = doc.CreateNode(XmlNodeType.Element, nodeName, null);
            return node;
        }
        
        private static XmlNode CreateNodeWithAttribute(XmlDocument doc, string nodeName, string prefix, string localName,
            string namespaceUri, string value)
        {
            var node = doc.CreateElement( nodeName);
            XmlAttribute attribute =
                doc.CreateAttribute(prefix, localName, namespaceUri);
            attribute.InnerText = value;
            node.Attributes?.Append(attribute);
            
            
            
            return node;
        }

        private static XmlElement CreateSubNode(XmlDocument doc, XmlNode node, string nodeName)
        {
            var newNode = doc.CreateNode(XmlNodeType.Element, nodeName, null);
            var element = (XmlElement) node.AppendChild(newNode);
            return element;
        }

        private static void AddNodePrefixAttribute(XmlDocument doc, XmlNode element, string prefix, string localName,
            string namespaceUri, string value)
        {
            XmlAttribute attribute =
                doc.CreateAttribute(prefix, localName, namespaceUri);
            attribute.InnerText = value;
            element.Attributes?.Append(attribute);
        }

        #endregion Methods
    }
}

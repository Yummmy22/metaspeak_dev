using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Zetcil
{
    public class VarBuilder : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Main Settings")]
        public string ScriptFileName;
        [TextArea] public string ScriptVariables;
        public bool UseDialogError;

        [Header("Result Settings")]
        [TextArea(15, 20)]
        public string ResultScript;

        // Start is called before the first frame update

        string GetClearString(string aData)
        {
            string result = Regex.Replace(aData, @"\t|\n|\r", "");
            return result;
        }

        string GetCommas(string[] aData)
        {
            string result = "";

            for (int i = 0; i < aData.Length-1; i++)
            {
                result = result + "\t\t\t" + GetClearString(aData[i]) + ", \n";
            }
            result = result + "\t\t\t" + GetClearString(aData[aData.Length-1]) + " \n";

            return result; 
        }

        string GetEnables(string[] aData)
        {
            string result = "";

            for (int i = 0; i < aData.Length - 1; i++)
            {
                result = result + "\t\t\t\t" + GetClearString(aData[i]) + " = serializedObject.FindProperty(\""+ GetClearString(aData[i])+"\"); \n";
            }
            result = result + "\t\t\t\t" + GetClearString(aData[aData.Length - 1]) + " = serializedObject.FindProperty(\"" + GetClearString(aData[aData.Length-1]) + "\"); \n";

            return result;
        }

        string GetLayout(string[] aData)
        {
            string result = "";

            for (int i = 0; i < aData.Length - 1; i++)
            {
                result = result + "\t\t\t\t" + " EditorGUILayout.PropertyField("+ GetClearString(aData[i]) + ", true); \n";
                result = result + GetDialog(GetClearString(aData[i])); 
            }
            result = result + "\t\t\t\t" + " EditorGUILayout.PropertyField(" + GetClearString(aData[aData.Length-1]) + ", true); \n";
            result = result + GetDialog(GetClearString(aData[aData.Length-1]));

            return result;
        }

        string GetDialog(string aData)
        {
            string result = "";

            if (UseDialogError)
            {
                result = result + "\t\t\t\t" + " if ("+ GetClearString(aData)+".objectReferenceValue == null) \n";
                result = result + "\t\t\t\t" + " { \n";
                result = result + "\t\t\t\t" + " EditorGUILayout.HelpBox(\"Required Field(s) Null / None\", MessageType.Error); \n";
                result = result + "\t\t\t\t" + " } \n";
            }

            return result;
        }




        void Start()
        {
            string[] targetVars = ScriptVariables.Split('\n');



            string header =
                "using UnityEditor; \n" +
                "using UnityEngine; \n" +
                "\n" +
                "namespace Zetcil \n" +
                "{ \n" +
                "\t[CustomEditor(typeof(" + ScriptFileName + ")), CanEditMultipleObjects] \n" +
                "\tpublic class " + ScriptFileName + "Editor : Editor \n" +
                "\t{ \n" +
                "\t\t public SerializedProperty \n" +
                GetCommas(targetVars) +
                "\t\t ; \n\n" +
                "\t\tvoid OnEnable() \n\n" +
                "\t\t{ \n" + GetEnables(targetVars) +
                "\t\t} \n" +
                "\t\t public override void OnInspectorGUI() \n" +
                "\t\t{ \n" +
                "\t\t\tserializedObject.Update(); \n" +
                "\t\t\tEditorGUILayout.PropertyField(isEnabled); \n" +
                "\t\t\tif (isEnabled.boolValue) \n" +
                "\t\t\t{\n" +
                GetLayout(targetVars) +
                "\t\t\t} else \n" +
                "\t\t\t{" +
                "\t\t\t\t" + " EditorGUILayout.HelpBox(\"Prefab Status: Disabled\", MessageType.Error); \n"+
                "\t\t\t}" +
                "\t\t\tserializedObject.ApplyModifiedProperties(); \n" +
                "\t\t} \n"
                ;

            string content = "";

            string footer =
                "\t" +
                "\n" +
                "\t}" +
                "\n" +
                "}";

            ResultScript = header + content + footer;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

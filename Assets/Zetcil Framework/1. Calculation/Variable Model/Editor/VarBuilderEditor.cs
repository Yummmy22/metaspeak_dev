using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarBuilder)), CanEditMultipleObjects]
    public class VarBuilderEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           ScriptFileName,
           ScriptVariables,
           UseDialogError,
           ResultScript
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            ScriptFileName = serializedObject.FindProperty("ScriptFileName");
            ScriptVariables = serializedObject.FindProperty("ScriptVariables");
            UseDialogError = serializedObject.FindProperty("UseDialogError");
            ResultScript = serializedObject.FindProperty("ResultScript");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Color saved = GUI.color;

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(ScriptFileName);
                EditorGUILayout.PropertyField(ScriptVariables);
                EditorGUILayout.PropertyField(UseDialogError);

                GUI.color = Color.green;
                EditorGUILayout.PropertyField(ResultScript);
                GUI.color = saved;

            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
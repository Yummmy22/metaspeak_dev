using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarConfig)), CanEditMultipleObjects]
    public class VarConfigEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           DataPath
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            DataPath = serializedObject.FindProperty("DataPath");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.HelpBox("Prefab Status: Activated", MessageType.Info);
                EditorGUILayout.PropertyField(DataPath, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarExp)), CanEditMultipleObjects]
    public class VarExpEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            CurrentValue,
            Configuration,
            ExpLevel,
            ExpSetting
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Configuration = serializedObject.FindProperty("Configuration");
            ExpLevel = serializedObject.FindProperty("ExpLevel");
            ExpSetting = serializedObject.FindProperty("ExpSetting");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(CurrentValue, true);
                EditorGUILayout.PropertyField(Configuration, true);
                if (Configuration.boolValue)
                {
                    EditorGUILayout.PropertyField(ExpLevel, true);
                    EditorGUILayout.PropertyField(ExpSetting, true);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarList)), CanEditMultipleObjects]
    public class VarListEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            CurrentIndex,
            CurrentValue
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentIndex = serializedObject.FindProperty("CurrentIndex");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(CurrentIndex, true);
                EditorGUILayout.PropertyField(CurrentValue, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
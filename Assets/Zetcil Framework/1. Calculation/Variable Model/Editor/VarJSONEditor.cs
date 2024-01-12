using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarJSON)), CanEditMultipleObjects]
    public class VarJSONEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            JSONValue,
            JSONRoot,
            CurrentRootIndex,
            CurrentItemIndex,
            CurrentKeyword
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            JSONValue = serializedObject.FindProperty("JSONValue");
            JSONRoot = serializedObject.FindProperty("JSONRoot");
            CurrentRootIndex = serializedObject.FindProperty("CurrentRootIndex");
            CurrentItemIndex = serializedObject.FindProperty("CurrentItemIndex");
            CurrentKeyword = serializedObject.FindProperty("CurrentKeyword");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(JSONValue, true);
                EditorGUILayout.PropertyField(JSONRoot, true);
                EditorGUILayout.PropertyField(CurrentRootIndex, true);
                EditorGUILayout.PropertyField(CurrentItemIndex, true);
                EditorGUILayout.PropertyField(CurrentKeyword, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
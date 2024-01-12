using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarVector2)), CanEditMultipleObjects]
    public class VarVector2Editor : Editor
    {
        public SerializedProperty
           isEnabled,
           usingVector2,
           CurrentValue,
           Vector2X,
           Vector2Y
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            usingVector2 = serializedObject.FindProperty("usingVector2");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Vector2X = serializedObject.FindProperty("Vector2X");
            Vector2Y = serializedObject.FindProperty("Vector2Y");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(usingVector2);
                EditorGUILayout.PropertyField(CurrentValue);
                EditorGUILayout.PropertyField(Vector2X);
                EditorGUILayout.PropertyField(Vector2Y);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
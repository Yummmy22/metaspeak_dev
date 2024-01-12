using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarVector3)), CanEditMultipleObjects]
    public class VarVector3Editor : Editor
    {
        public SerializedProperty
           isEnabled,
           CurrentValue,
           Vector3X,
           Vector3Y,
           Vector3Z,
           Rotation,
           Position
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Vector3X = serializedObject.FindProperty("Vector3X");
            Vector3Y = serializedObject.FindProperty("Vector3Y");
            Vector3Z = serializedObject.FindProperty("Vector3Z");
            Rotation = serializedObject.FindProperty("Rotation");
            Position = serializedObject.FindProperty("Position");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(CurrentValue);
                EditorGUILayout.PropertyField(Vector3X);
                EditorGUILayout.PropertyField(Vector3Y);
                EditorGUILayout.PropertyField(Vector3Z);
                EditorGUILayout.PropertyField(Position);
                EditorGUILayout.PropertyField(Rotation);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
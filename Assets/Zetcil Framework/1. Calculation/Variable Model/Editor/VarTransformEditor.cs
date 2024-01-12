using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarTransform)), CanEditMultipleObjects]
    public class VarTransformEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetTransform,
           usingPosition,
           Position,
           usingRotation,
           Rotation,
           usingScale,
           Scale
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetTransform = serializedObject.FindProperty("TargetTransform");
            usingPosition = serializedObject.FindProperty("usingPosition");
            Position = serializedObject.FindProperty("Position");
            usingRotation = serializedObject.FindProperty("usingRotation");
            Rotation = serializedObject.FindProperty("Rotation");
            usingScale = serializedObject.FindProperty("usingScale");
            Scale = serializedObject.FindProperty("Scale");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetTransform);
                EditorGUILayout.PropertyField(usingPosition);
                EditorGUILayout.PropertyField(Position);
                EditorGUILayout.PropertyField(usingRotation);
                EditorGUILayout.PropertyField(Rotation);
                EditorGUILayout.PropertyField(usingScale);
                EditorGUILayout.PropertyField(Scale);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
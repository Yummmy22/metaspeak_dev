using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarPlay)), CanEditMultipleObjects]
    public class VarPlayEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetController,
           MovementType,
           MoveSpeed,
           jumpSpeed,
           RotateSpeed,
           gravity,
           KeyboardKey,
           AltKeyboardKey
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetController = serializedObject.FindProperty("TargetController");
            MovementType = serializedObject.FindProperty("MovementType");
            MoveSpeed = serializedObject.FindProperty("MoveSpeed");
            jumpSpeed = serializedObject.FindProperty("jumpSpeed");
            RotateSpeed = serializedObject.FindProperty("RotateSpeed");
            gravity = serializedObject.FindProperty("gravity");
            KeyboardKey = serializedObject.FindProperty("KeyboardKey");
            AltKeyboardKey = serializedObject.FindProperty("AltKeyboardKey");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetController, true);
                EditorGUILayout.PropertyField(MovementType, true);
                EditorGUILayout.PropertyField(MoveSpeed, true);
                EditorGUILayout.PropertyField(jumpSpeed, true);
                EditorGUILayout.PropertyField(RotateSpeed, true);
                EditorGUILayout.PropertyField(gravity, true);
                EditorGUILayout.PropertyField(KeyboardKey, true);
                EditorGUILayout.PropertyField(AltKeyboardKey, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
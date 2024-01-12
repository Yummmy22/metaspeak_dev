using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarCursor)), CanEditMultipleObjects]
    public class VarCursorEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           CursorKey,
           CursorVisible
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            CursorKey = serializedObject.FindProperty("CursorKey");
            CursorVisible = serializedObject.FindProperty("CursorVisible");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(CursorKey);
                EditorGUILayout.PropertyField(CursorVisible);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
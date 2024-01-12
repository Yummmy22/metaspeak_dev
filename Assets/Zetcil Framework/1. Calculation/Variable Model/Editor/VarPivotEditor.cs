using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarPivot)), CanEditMultipleObjects]
    public class VarPivotEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           AnchorType,
           TargetObject
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            AnchorType = serializedObject.FindProperty("AnchorType");
            TargetObject = serializedObject.FindProperty("TargetObject");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(AnchorType);
                EditorGUILayout.PropertyField(TargetObject);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
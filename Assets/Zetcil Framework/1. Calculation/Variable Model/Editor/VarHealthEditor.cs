using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarHealth)), CanEditMultipleObjects]
    public class VarHealthEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            CurrentValue,
            Constraint,
            MinValue,
            MaxValue
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Constraint = serializedObject.FindProperty("Constraint");
            MinValue = serializedObject.FindProperty("MinValue");
            MaxValue = serializedObject.FindProperty("MaxValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(CurrentValue, true);
                EditorGUILayout.PropertyField(Constraint, true);
                if (Constraint.boolValue)
                {
                    EditorGUILayout.PropertyField(MinValue, true);
                    EditorGUILayout.PropertyField(MaxValue, true);
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
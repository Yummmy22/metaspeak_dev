using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarTime)), CanEditMultipleObjects]
    public class VarTimeEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            CurrentValue,
            Constraint,
            MinValue,
            MaxValue,
            TimeCalculation
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Constraint = serializedObject.FindProperty("Constraint");
            TimeCalculation = serializedObject.FindProperty("TimeCalculation");
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

                EditorGUILayout.PropertyField(TimeCalculation, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
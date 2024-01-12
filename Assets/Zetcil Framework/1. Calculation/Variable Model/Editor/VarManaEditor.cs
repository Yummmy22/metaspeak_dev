using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarMana)), CanEditMultipleObjects]
    public class VarManaEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            CurrentValue,
            Constraint,
            MinValue,
            MaxValue,
            usingRegeneration,
            RepeatRate,
            RegenerationValue
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Constraint = serializedObject.FindProperty("Constraint");
            MinValue = serializedObject.FindProperty("MinValue");
            MaxValue = serializedObject.FindProperty("MaxValue");
            usingRegeneration = serializedObject.FindProperty("usingRegeneration");
            RepeatRate = serializedObject.FindProperty("RepeatRate");
            RegenerationValue = serializedObject.FindProperty("RegenerationValue");
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
                    EditorGUILayout.PropertyField(usingRegeneration, true);
                    EditorGUILayout.PropertyField(RepeatRate, true);
                    EditorGUILayout.PropertyField(RegenerationValue, true);
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
using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarSyncronize)), CanEditMultipleObjects]
    public class VarSyncronizeEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            VariableName,
            VariableType,
            TimeVariables,
            HealthVariables,
            ManaVariables,
            ExpVariables,
            ScoreVariables,
            IntegerVariables,
            FloatVariables,
            StringVariables,
            BooleanVariables,
            ObjectVariables
        ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            VariableName = serializedObject.FindProperty("VariableName");
            VariableType = serializedObject.FindProperty("VariableType");
            TimeVariables = serializedObject.FindProperty("TimeVariables");
            HealthVariables = serializedObject.FindProperty("HealthVariables");
            ManaVariables = serializedObject.FindProperty("ManaVariables");
            ExpVariables = serializedObject.FindProperty("ExpVariables");
            ScoreVariables = serializedObject.FindProperty("ScoreVariables");
            IntegerVariables = serializedObject.FindProperty("IntegerVariables");
            FloatVariables = serializedObject.FindProperty("FloatVariables");
            StringVariables = serializedObject.FindProperty("StringVariables");
            BooleanVariables = serializedObject.FindProperty("BooleanVariables");
            ObjectVariables = serializedObject.FindProperty("ObjectVariables");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);
            bool check = isEnabled.boolValue;

            if (check)
            {

                EditorGUILayout.PropertyField(VariableName, true);
                EditorGUILayout.PropertyField(VariableType, true);

                GlobalVariable.CVariableType vartype = (GlobalVariable.CVariableType) VariableType.enumValueIndex;

                switch (vartype)
                {
                    case GlobalVariable.CVariableType.timeVar:
                        EditorGUILayout.PropertyField(TimeVariables, true);
                        if (TimeVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.healthVar:
                        EditorGUILayout.PropertyField(HealthVariables, true);
                        if (HealthVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.manaVar:
                        EditorGUILayout.PropertyField(ManaVariables, true);
                        if (ManaVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.expVar:
                        EditorGUILayout.PropertyField(ExpVariables, true);
                        if (ExpVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.scoreVar:
                        EditorGUILayout.PropertyField(ScoreVariables, true);
                        if (ScoreVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.intVar:
                        EditorGUILayout.PropertyField(IntegerVariables, true);
                        if (IntegerVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.floatVar:
                        EditorGUILayout.PropertyField(FloatVariables, true);
                        if (FloatVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.stringVar:
                        EditorGUILayout.PropertyField(StringVariables, true);
                        if (StringVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.boolVar:
                        EditorGUILayout.PropertyField(BooleanVariables, true);
                        if (BooleanVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
                    case GlobalVariable.CVariableType.objectVar:
                        EditorGUILayout.PropertyField(ObjectVariables, true);
                        if (ObjectVariables.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                        }
                        break;
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

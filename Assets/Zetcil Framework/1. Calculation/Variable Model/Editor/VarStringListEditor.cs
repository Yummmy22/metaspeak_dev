using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarStringList)), CanEditMultipleObjects]
    public class VarStringListEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetString,
           CurrentIndex,
           StringListValue
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetString = serializedObject.FindProperty("TargetString");
            CurrentIndex = serializedObject.FindProperty("CurrentIndex");
            StringListValue = serializedObject.FindProperty("StringListValue");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetString, true);
                if (TargetString.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(CurrentIndex, true);
                EditorGUILayout.PropertyField(StringListValue, true);
                if (StringListValue.arraySize == 0)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
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
using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarFile)), CanEditMultipleObjects]
    public class VarFileEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           OperationType,
           DirectoryName,
           FileName,
           ContentValue,
           usingDelay,
           Delay,
           usingInterval,
           Interval
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            OperationType = serializedObject.FindProperty("OperationType");
            DirectoryName = serializedObject.FindProperty("DirectoryName");
            FileName = serializedObject.FindProperty("FileName");
            ContentValue = serializedObject.FindProperty("ContentValue");
            usingDelay = serializedObject.FindProperty("usingDelay");
            Delay = serializedObject.FindProperty("Delay");
            usingInterval = serializedObject.FindProperty("usingInterval");
            Interval = serializedObject.FindProperty("Interval");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(OperationType, true);
                EditorGUILayout.PropertyField(DirectoryName, true);
                if (DirectoryName.stringValue.Length == 0)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(FileName, true);
                if (FileName.stringValue.Length == 0)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ContentValue, true);
                if (!ContentValue.objectReferenceValue)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }

                if (GlobalVariable.CInvokeType.OnDelay == (GlobalVariable.CInvokeType)InvokeType.enumValueIndex)
                {
                    EditorGUILayout.PropertyField(usingDelay, true);
                    if (usingDelay.boolValue)
                    {
                        EditorGUILayout.PropertyField(Delay, true);
                    }
                }
                if (GlobalVariable.CInvokeType.OnInterval == (GlobalVariable.CInvokeType)InvokeType.enumValueIndex)
                {
                    EditorGUILayout.PropertyField(usingInterval, true);
                    if (usingInterval.boolValue)
                    {
                        EditorGUILayout.PropertyField(Interval, true);
                    }
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
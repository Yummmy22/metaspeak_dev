/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script editor untuk var manager
 **************************************************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarManager)), CanEditMultipleObjects]
    public class UITexVarManagerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            OperationType,
            StorageType,
            XMLFile,
            URLPath,
            SaveOnStart,
            SaveTimeContainer,
            SaveHealthContainer,
            SaveManaContainer,
            SaveExpContainer,
            SaveScoreContainer,
            SaveFloatContainer,
            SaveIntContainer,
            SaveStringContainer,
            SaveBoolContainer,
            LoadOnStart,
            LoadTimeContainer,
            LoadHealthContainer,
            LoadManaContainer,
            LoadExpContainer,
            LoadScoreContainer,
            LoadFloatContainer,
            LoadIntContainer,
            LoadStringContainer,
            LoadBoolContainer,
            LoadDataContainer
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");

            OperationType = serializedObject.FindProperty("OperationType");
            StorageType = serializedObject.FindProperty("StorageType");
            XMLFile = serializedObject.FindProperty("XMLFile");
            URLPath = serializedObject.FindProperty("URLPath");

            SaveTimeContainer = serializedObject.FindProperty("SaveTimeContainer");
            SaveHealthContainer = serializedObject.FindProperty("SaveHealthContainer");
            SaveManaContainer = serializedObject.FindProperty("SaveManaContainer");
            SaveExpContainer = serializedObject.FindProperty("SaveExpContainer");
            SaveScoreContainer = serializedObject.FindProperty("SaveScoreContainer");
            SaveFloatContainer = serializedObject.FindProperty("SaveFloatContainer");
            SaveIntContainer = serializedObject.FindProperty("SaveIntContainer");
            SaveStringContainer = serializedObject.FindProperty("SaveStringContainer");
            SaveBoolContainer = serializedObject.FindProperty("SaveBoolContainer");

            SaveOnStart = serializedObject.FindProperty("SaveOnStart");

            LoadTimeContainer = serializedObject.FindProperty("LoadTimeContainer");
            LoadHealthContainer = serializedObject.FindProperty("LoadHealthContainer");
            LoadManaContainer = serializedObject.FindProperty("LoadManaContainer");
            LoadExpContainer = serializedObject.FindProperty("LoadExpContainer");
            LoadScoreContainer = serializedObject.FindProperty("LoadScoreContainer");
            LoadFloatContainer = serializedObject.FindProperty("LoadFloatContainer");
            LoadIntContainer = serializedObject.FindProperty("LoadIntContainer");
            LoadStringContainer = serializedObject.FindProperty("LoadStringContainer");
            LoadBoolContainer = serializedObject.FindProperty("LoadBoolContainer");

            LoadOnStart = serializedObject.FindProperty("LoadOnStart");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled, true);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(StorageType, true);

                VarManager.CStorageType opt = (VarManager.CStorageType) StorageType.enumValueIndex;

                if (opt != VarManager.CStorageType.None)
                {
                    switch (opt)
                    {
                        case VarManager.CStorageType.ByPlayerPref:
                            break;
                        case VarManager.CStorageType.ByXML:
                            EditorGUILayout.PropertyField(XMLFile, true);
                            break;
                    }


                    EditorGUILayout.PropertyField(OperationType, true);

                    VarManager.COperationType st = (VarManager.COperationType)OperationType.enumValueIndex;

                    switch (st)
                    {
                        case VarManager.COperationType.SaveData:
                            EditorGUILayout.PropertyField(SaveOnStart, true);

                            EditorGUILayout.PropertyField(SaveHealthContainer, true);
                            EditorGUILayout.PropertyField(SaveManaContainer, true);
                            EditorGUILayout.PropertyField(SaveExpContainer, true);
                            EditorGUILayout.PropertyField(SaveTimeContainer, true);
                            EditorGUILayout.PropertyField(SaveScoreContainer, true);
                            EditorGUILayout.PropertyField(SaveIntContainer, true);
                            EditorGUILayout.PropertyField(SaveFloatContainer, true);
                            EditorGUILayout.PropertyField(SaveStringContainer, true);
                            EditorGUILayout.PropertyField(SaveBoolContainer, true);
                            break;
                        case VarManager.COperationType.LoadData:
                            EditorGUILayout.PropertyField(LoadOnStart, true);

                            EditorGUILayout.PropertyField(LoadHealthContainer, true);
                            EditorGUILayout.PropertyField(LoadManaContainer, true);
                            EditorGUILayout.PropertyField(LoadExpContainer, true);
                            EditorGUILayout.PropertyField(LoadTimeContainer, true);
                            EditorGUILayout.PropertyField(LoadScoreContainer, true);
                            EditorGUILayout.PropertyField(LoadIntContainer, true);
                            EditorGUILayout.PropertyField(LoadFloatContainer, true);
                            EditorGUILayout.PropertyField(LoadStringContainer, true);
                            EditorGUILayout.PropertyField(LoadBoolContainer, true);

                            break;
                        case VarManager.COperationType.LoadAndSaveData:

                            EditorGUILayout.PropertyField(LoadOnStart, true);
                            EditorGUILayout.PropertyField(LoadHealthContainer, true);
                            EditorGUILayout.PropertyField(LoadManaContainer, true);
                            EditorGUILayout.PropertyField(LoadExpContainer, true);
                            EditorGUILayout.PropertyField(LoadTimeContainer, true);
                            EditorGUILayout.PropertyField(LoadScoreContainer, true);
                            EditorGUILayout.PropertyField(LoadIntContainer, true);
                            EditorGUILayout.PropertyField(LoadFloatContainer, true);
                            EditorGUILayout.PropertyField(LoadStringContainer, true);
                            EditorGUILayout.PropertyField(LoadBoolContainer, true);

                            EditorGUILayout.PropertyField(SaveOnStart, true);
                            EditorGUILayout.PropertyField(SaveHealthContainer, true);
                            EditorGUILayout.PropertyField(SaveManaContainer, true);
                            EditorGUILayout.PropertyField(SaveExpContainer, true);
                            EditorGUILayout.PropertyField(SaveTimeContainer, true);
                            EditorGUILayout.PropertyField(SaveScoreContainer, true);
                            EditorGUILayout.PropertyField(SaveIntContainer, true);
                            EditorGUILayout.PropertyField(SaveFloatContainer, true);
                            EditorGUILayout.PropertyField(SaveStringContainer, true);
                            EditorGUILayout.PropertyField(SaveBoolContainer, true);

                            break;
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

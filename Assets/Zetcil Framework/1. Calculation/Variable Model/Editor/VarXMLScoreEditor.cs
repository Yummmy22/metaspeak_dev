using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarXMLScore)), CanEditMultipleObjects]
    public class VarXMLScoreEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           XMLScore,
           XMLRank,
           XMLScores,
           XMLUIScores
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            XMLScore = serializedObject.FindProperty("XMLScore");
            XMLRank = serializedObject.FindProperty("XMLRank");
            XMLScores = serializedObject.FindProperty("XMLScores");
            XMLUIScores = serializedObject.FindProperty("XMLUIScores");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(XMLScore, true);
                EditorGUILayout.PropertyField(XMLRank, true);
                EditorGUILayout.PropertyField(XMLScores, true);
                EditorGUILayout.PropertyField(XMLUIScores, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
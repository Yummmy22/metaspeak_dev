using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarRandom)), CanEditMultipleObjects]
    public class VarRandomEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           RandomMin,
           RandomMax,
           RandomResult
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            RandomMin = serializedObject.FindProperty("RandomMin");
            RandomMax = serializedObject.FindProperty("RandomMax");
            RandomResult = serializedObject.FindProperty("RandomResult");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(RandomMin);
                EditorGUILayout.PropertyField(RandomMax);
                EditorGUILayout.PropertyField(RandomResult);
                if (RandomResult.objectReferenceValue == null)
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
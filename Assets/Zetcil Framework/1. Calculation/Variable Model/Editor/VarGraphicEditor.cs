using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarGraphic)), CanEditMultipleObjects]
    public class VarGraphicEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           OperationType,
           AntiAlias,
           Reflection,
           LightCorrection
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            OperationType = serializedObject.FindProperty("OperationType");
            AntiAlias = serializedObject.FindProperty("AntiAlias");
            Reflection = serializedObject.FindProperty("Reflection");
            LightCorrection = serializedObject.FindProperty("LightCorrection");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(OperationType, true);
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Initialize)
                {
                    EditorGUILayout.HelpBox("Save Mode: ON", MessageType.Warning);
                }
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Runtime)
                {
                    EditorGUILayout.HelpBox("Publish Mode: ON", MessageType.Info);
                }

                EditorGUILayout.PropertyField(AntiAlias, true);
                EditorGUILayout.PropertyField(Reflection, true);
                EditorGUILayout.PropertyField(LightCorrection, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
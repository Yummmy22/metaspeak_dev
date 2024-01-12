using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarJoystick)), CanEditMultipleObjects]
    public class VarJoystickEditor : Editor
    {
        public SerializedProperty
           isEnabled
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarPointer)), CanEditMultipleObjects]
    public class VarPointerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            PointerType,
            TargetCamera,
            usingVector2,
            VarVector2D,
            usingEventVector2,
            EventVector2,
            usingVector3,
            VarVector3D,
            usingEventVector3,
            EventVector3,
            isDebugReadOnly,
            Pointer2D,
            Pointer3D,
            usingPointerRotation,
            PointerController,
            usingRaycastController,
            RaycastController
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            PointerType = serializedObject.FindProperty("PointerType");
            usingVector2 = serializedObject.FindProperty("usingVector2");
            VarVector2D = serializedObject.FindProperty("VarVector2D");
            usingEventVector2 = serializedObject.FindProperty("usingEventVector2");
            EventVector2 = serializedObject.FindProperty("EventVector2");

            usingVector3 = serializedObject.FindProperty("usingVector3");
            VarVector3D = serializedObject.FindProperty("VarVector3D");
            usingEventVector3 = serializedObject.FindProperty("usingEventVector3");
            EventVector3 = serializedObject.FindProperty("EventVector3");

            isDebugReadOnly = serializedObject.FindProperty("isDebugReadOnly");
            Pointer2D = serializedObject.FindProperty("Pointer2D");
            Pointer3D = serializedObject.FindProperty("Pointer3D");

            usingPointerRotation = serializedObject.FindProperty("usingPointerRotation");
            PointerController = serializedObject.FindProperty("PointerController");
            usingRaycastController = serializedObject.FindProperty("usingRaycastController");
            RaycastController = serializedObject.FindProperty("RaycastController");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(PointerType, true);
                EditorGUILayout.PropertyField(TargetCamera, true);

                EditorGUILayout.PropertyField(usingVector2, true);
                if (usingVector2.boolValue)
                {
                    EditorGUILayout.PropertyField(VarVector2D, true);
                    EditorGUILayout.PropertyField(usingEventVector2, true);
                    EditorGUILayout.PropertyField(EventVector2, true);
                }

                EditorGUILayout.PropertyField(usingVector3, true);
                if (usingVector3.boolValue)
                {
                    EditorGUILayout.PropertyField(VarVector3D, true);
                    EditorGUILayout.PropertyField(usingEventVector3, true);
                    EditorGUILayout.PropertyField(EventVector3, true);
                }

                EditorGUILayout.PropertyField(usingPointerRotation, true);
                if (usingPointerRotation.boolValue)
                {
                    EditorGUILayout.PropertyField(PointerController, true);
                }

                EditorGUILayout.PropertyField(usingRaycastController, true);
                if (usingRaycastController.boolValue)
                {
                    EditorGUILayout.PropertyField(RaycastController, true);
                }

                EditorGUILayout.PropertyField(isDebugReadOnly, true);
                EditorGUILayout.PropertyField(Pointer2D, true);
                EditorGUILayout.PropertyField(Pointer3D, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }


    }
}
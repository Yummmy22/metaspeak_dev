using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarLayout)), CanEditMultipleObjects]
    public class VarLayoutEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           OperationType,
           PresetType,
           HeaderColor,
           HeaderTransparency,
           FooterColor,
           FooterTransparency,
           BackgroundColor,
           BackgroundTransparency,
           PanelColor,
           PanelTransparency,
           IconColor,
           IconTransparency,
           PrimaryNormalColor,
           PrimaryHighlightColor,
           PrimaryPressedColor,
           PrimarySelectedColor,
           PrimaryDisabledColor,
           PrimaryButtonTransparency,
           SecondaryNormalColor,
           SecondaryHighlightColor,
           SecondaryPressedColor,
           SecondarySelectedColor,
           SecondaryDisabledColor,
           SecondaryButtonTransparency
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            PresetType = serializedObject.FindProperty("PresetType");
            OperationType = serializedObject.FindProperty("OperationType");
            HeaderColor = serializedObject.FindProperty("HeaderColor");
            HeaderTransparency = serializedObject.FindProperty("HeaderTransparency");
            FooterColor = serializedObject.FindProperty("FooterColor");
            FooterTransparency = serializedObject.FindProperty("FooterTransparency");
            BackgroundColor = serializedObject.FindProperty("BackgroundColor");
            BackgroundTransparency = serializedObject.FindProperty("BackgroundTransparency");
            PanelColor = serializedObject.FindProperty("PanelColor");
            PanelTransparency = serializedObject.FindProperty("PanelTransparency");
            IconColor = serializedObject.FindProperty("IconColor");
            IconTransparency = serializedObject.FindProperty("IconTransparency");
            PrimaryNormalColor = serializedObject.FindProperty("PrimaryNormalColor");
            PrimaryHighlightColor = serializedObject.FindProperty("PrimaryHighlightColor");
            PrimaryPressedColor = serializedObject.FindProperty("PrimaryPressedColor");
            PrimarySelectedColor = serializedObject.FindProperty("PrimarySelectedColor");
            PrimaryDisabledColor = serializedObject.FindProperty("PrimaryDisabledColor");
            PrimaryButtonTransparency = serializedObject.FindProperty("PrimaryButtonTransparency");
            SecondaryNormalColor = serializedObject.FindProperty("SecondaryNormalColor");
            SecondaryHighlightColor = serializedObject.FindProperty("SecondaryHighlightColor");
            SecondaryPressedColor = serializedObject.FindProperty("SecondaryPressedColor");
            SecondarySelectedColor = serializedObject.FindProperty("SecondarySelectedColor");
            SecondaryDisabledColor = serializedObject.FindProperty("SecondaryDisabledColor");
            SecondaryButtonTransparency = serializedObject.FindProperty("SecondaryButtonTransparency");
        }

        public Color ConvertColor(string aColor, float aTransparency)
        {
            Color result = Color.black;
            ColorUtility.TryParseHtmlString(aColor, out result);
            result.a = aTransparency;
            return result;
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

                EditorGUILayout.PropertyField(PresetType, true);
                EditorGUILayout.PropertyField(HeaderColor, true);
                EditorGUILayout.PropertyField(HeaderTransparency, true);
                EditorGUILayout.PropertyField(FooterColor, true);
                EditorGUILayout.PropertyField(FooterTransparency, true);
                EditorGUILayout.PropertyField(BackgroundColor, true);
                EditorGUILayout.PropertyField(BackgroundTransparency, true);
                EditorGUILayout.PropertyField(PanelColor, true);
                EditorGUILayout.PropertyField(PanelTransparency, true);
                EditorGUILayout.PropertyField(IconColor, true);
                EditorGUILayout.PropertyField(IconTransparency, true);
                EditorGUILayout.PropertyField(PrimaryNormalColor, true);
                EditorGUILayout.PropertyField(PrimaryHighlightColor, true);
                EditorGUILayout.PropertyField(PrimaryPressedColor, true);
                EditorGUILayout.PropertyField(PrimarySelectedColor, true);
                EditorGUILayout.PropertyField(PrimaryDisabledColor, true);
                EditorGUILayout.PropertyField(PrimaryButtonTransparency, true);
                EditorGUILayout.PropertyField(SecondaryNormalColor, true);
                EditorGUILayout.PropertyField(SecondaryHighlightColor, true);
                EditorGUILayout.PropertyField(SecondaryPressedColor, true);
                EditorGUILayout.PropertyField(SecondarySelectedColor, true);
                EditorGUILayout.PropertyField(SecondaryDisabledColor, true);
                EditorGUILayout.PropertyField(SecondaryButtonTransparency, true);

                if ((VarLayout.CPresetType)PresetType.enumValueIndex == VarLayout.CPresetType.Default)
                {
                    HeaderTransparency.floatValue = 1f;
                    FooterTransparency.floatValue = 1f;
                    BackgroundTransparency.floatValue = 1f;
                    PanelTransparency.floatValue = 1f;
                    IconTransparency.floatValue = 1f;
                    PrimaryButtonTransparency.floatValue = 1f;
                    SecondaryButtonTransparency.floatValue = 1f;

                    HeaderColor.colorValue = ConvertColor("#FFFFFF", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#FFFFFF", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#FFFFFF", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#FFFFFF", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#FFFFFF", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#FFFFFF", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#C8C8C8", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#FFFFFF", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#FFFFFF", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#FFFFFF", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#FFFFFF", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#C8C8C8", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#FFFFFF", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#FFFFFF", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#FFFFFF", SecondaryButtonTransparency.floatValue);
                }
                else if ((VarLayout.CPresetType)PresetType.enumValueIndex == VarLayout.CPresetType.Vermilion)
                {
                    HeaderTransparency.floatValue = 0.8f;
                    FooterTransparency.floatValue = 0.8f;
                    BackgroundTransparency.floatValue = 0.8f;
                    PanelTransparency.floatValue = 0.8f;
                    IconTransparency.floatValue = 0.8f;
                    PrimaryButtonTransparency.floatValue = 0.8f;
                    SecondaryButtonTransparency.floatValue = 0.8f;

                    HeaderColor.colorValue = ConvertColor("#9F243D", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#9F243D", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#9F243D", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#9F243D", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#9F243D", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#9F243D", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#FF9358", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#F8B997", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#F6AA83", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#9F243D", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#9F243D", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#FF9358", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#F8B997", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#F6AA83", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#9F243D", SecondaryButtonTransparency.floatValue);
                }
                else if ((VarLayout.CPresetType)PresetType.enumValueIndex == VarLayout.CPresetType.Emerald)
                {
                    HeaderTransparency.floatValue = 0.8f;
                    FooterTransparency.floatValue = 0.8f;
                    BackgroundTransparency.floatValue = 0.8f;
                    PanelTransparency.floatValue = 0.8f;
                    IconTransparency.floatValue = 0.8f;
                    PrimaryButtonTransparency.floatValue = 0.8f;
                    SecondaryButtonTransparency.floatValue = 0.8f;

                    HeaderColor.colorValue = ConvertColor("#1F662E", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#1F662E", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#1F662E", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#1F662E", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#1F662E", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#1F662E", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#BEC83B", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#DBE37F", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#D0D969", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#1F662E", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#1F662E", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#BEC83B", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#DBE37F", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#D0D969", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#1F662E", SecondaryButtonTransparency.floatValue);
                }
                else if ((VarLayout.CPresetType)PresetType.enumValueIndex == VarLayout.CPresetType.Azure)
                {
                    HeaderTransparency.floatValue = 0.8f;
                    FooterTransparency.floatValue = 0.8f;
                    BackgroundTransparency.floatValue = 0.8f;
                    PanelTransparency.floatValue = 0.8f;
                    IconTransparency.floatValue = 0.8f;
                    PrimaryButtonTransparency.floatValue = 0.8f;
                    SecondaryButtonTransparency.floatValue = 0.8f;

                    HeaderColor.colorValue = ConvertColor("#1B29AB", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#1B29AB", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#1B29AB", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#1B29AB", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#1B29AB", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#1B29AB", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#ABAB2B", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#FFFBE6", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#ADBCCF", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#1B29AB", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#1B29AB", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#ABAB2B", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#FFFBE6", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#ADBCCF", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#1B29AB", SecondaryButtonTransparency.floatValue);
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
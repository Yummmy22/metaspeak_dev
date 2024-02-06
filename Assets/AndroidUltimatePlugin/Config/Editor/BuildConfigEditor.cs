namespace Gigadrillgames.AUP.ScriptableObjects
{
    using System;
    using UnityEditor;
    using UnityEngine;
    
    [CustomEditor(typeof(BuildConfig))]
    [InitializeOnLoad]
    public class BuildConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            BuildConfig config = (BuildConfig) target;

            if (GUILayout.Button("Update"))
            {
                UpdateConfig(config);
            }
            
            if (GUILayout.Button("Clear"))
            {
                ClearValues();
            }
        }

        public void UpdateConfig(BuildConfig config)
        {
            if (!CheckValues(config.Version,"Version"))
            {
                return;
            }
            
            if (!CheckValues( config.BundleVersionCode.ToString(),"Bundle Version Code"))
            {
                return;
            }

            PlayerSettings.bundleVersion = config.Version;
            PlayerSettings.Android.bundleVersionCode = config.BundleVersionCode;
            PlayerSettings.iOS.buildNumber  = config.BundleVersionCode.ToString();
            Debug.Log($"<color=blue> bundleVersion: {PlayerSettings.bundleVersion}.</color>");
            Debug.Log($"<color=blue> bundleVersionCode: {PlayerSettings.Android.bundleVersionCode}.</color>");
            Debug.Log($"<color=blue> iOS buildNumber: {PlayerSettings.iOS.buildNumber}.</color>");
            Debug.Log("<color=blue> Initialized Build Version Player Settings.</color>");
        }

        private void ClearValues()
        {
            PlayerSettings.bundleVersion = "";
            PlayerSettings.Android.bundleVersionCode = 0;
            PlayerSettings.iOS.buildNumber = "";
        }

        private bool CheckValues(string fieldValue, string fieldName)
        {
            if (String.IsNullOrEmpty(fieldValue))
            {
                EditorUtility.DisplayDialog("Missing Field", $"You must set {fieldName}!", "OK");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

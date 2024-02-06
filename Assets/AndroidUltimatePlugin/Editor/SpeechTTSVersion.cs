using Gigadrillgames.AUP.ScriptableObjects;
using UnityEditor;
using UnityEngine;

public class SpeechTTSVersion : EditorWindow
{
    #region Fields
    private static string version;
    #endregion Fields
    

    #region Methods
    [MenuItem("Window/AUP/SpeechTTS/About")]
    static void ShowWindow()
    {
        SpeechTTSVersion window = CreateInstance<SpeechTTSVersion>();
        window.titleContent = new GUIContent("AUP SpeechTTS");
        Vector2 size =  new Vector2(200, 135);
        window.maxSize = size;
        window.minSize = size;
        window.ShowUtility();
    }

    void OnGUI()
    {
        GUILayout.Space(5);
        EditorGUILayout.LabelField($"Version: {MasterConfig.BuildConfig.Version}");
        GUILayout.Space(15);

        if (GUILayout.Button("About"))
        {
            Application.OpenURL("https://gigadrillgames.com/2020/03/13/android-ultimate-plugin/");
        }
        GUILayout.Space(2);
        if (GUILayout.Button("How to setup"))
        {
            Application.OpenURL("https://docs.google.com/document/d/1UOF1h5Q_wh-Wp9g6g5z7wgea03v5jkIz3Pr0LJJDXaY/edit?usp=sharing");
        }
        GUILayout.Space(2);
        if (GUILayout.Button("Video tutorial"))
        {
            Application.OpenURL("https://youtu.be/Xg7-uia7yes");
        }
        GUILayout.Space(2);
        if (GUILayout.Button("Features"))
        {
            //Close();
            Application.OpenURL("https://youtu.be/_vzlLWpUOyU");
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
    #endregion Methods
    
}
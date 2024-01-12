using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Zetcil
{
    public class ZetcilLicense : EditorWindow
    {
        GUISkin skin;
        GameObject charObj;
        GameObject mixamoHips;
        GameObject raiderHips;
        Animator charAnimator;
        public RuntimeAnimatorController controller;
        public GameObject hud;
        Vector2 rect = new Vector2(390, 300);
        Texture2D m_Logo;

        /// <summary>
        /// 3rdPersonController Menu 
        /// </summary>    
        [MenuItem("Zetcil/About Framework %F1", false, 9010)]
        public static void CreateNewCharacter()
        {
            GetWindow<ZetcilLicense>();
        }

        bool isHuman, isValidAvatar, charExist;
        void OnEnable()
        {
            m_Logo = Resources.Load("Icons/icon_zetcil") as Texture2D;
        }

        void OnGUI()
        {
            if (!skin) skin = Resources.Load("Skin/zetSkin") as GUISkin;
            GUI.skin = skin;

            this.maxSize = rect;
            this.minSize = rect;
            this.titleContent = new GUIContent("About", null, "Zetcil Framework");
            GUILayout.BeginVertical("Zetcil Framework", "window");
            GUILayout.FlexibleSpace();

            GUILayout.Label(m_Logo, GUILayout.MaxHeight(25));

            GUILayout.Space(10);

            GUILayout.Label("Zetcil Framework is a C# library to speed up Unity-based\n" +
                            "application/game development. This framework is developed \n" +
                            "by Rickman Roedavan and released under CC/NC (Creative \n"+
                            "Common/Non-Commercial). You are permitted to use it for \n"+
                            "non-commercial type products or educational purpose. \n"+
                            "For commercial project, please contact: \n"+
                            "rroedavan@gmail.com");

            GUILayout.Space(5);

            GUILayout.Label("Special Thanks: Invector, Brackeys, Kaynn, Yeo Wen Qin,\n" +
                            "Gramm, Matthew Miner, Richard Fine, Ryan Hipple,\n" +
                            "Alan Mattano, and all Unity Game Developer Community. \n" +
                            "You guys ROCKS!");

            GUILayout.Space(5);

            GUILayout.Label("Zetcil Framework ver 5.b0403 (c) 2018-2021 \n");
        }

    }
}
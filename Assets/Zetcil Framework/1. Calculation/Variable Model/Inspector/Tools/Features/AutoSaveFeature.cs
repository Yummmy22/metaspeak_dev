#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace TechnomediaLabs.Internal
{
	[InitializeOnLoad]
	public class AutoSaveFeature
	{
		private const string MenuItemName = "Zetcil/Tools/AutoSave on Play";

        static AutoSaveFeature()
        {
            EditorApplication.playModeStateChanged += AutoSaveWhenPlaymodeStarts;
        }

        private static bool IsEnabled
		{
			get { return TechnomediaLabsSettings.AutoSaveEnabled; }
			set { TechnomediaLabsSettings.AutoSaveEnabled = value; }
		}

        private static void MenuItem()
		{
			IsEnabled = !IsEnabled;
		}

        [MenuItem(MenuItemName, false, 5000)]
        private static bool MenuItemValidation()
		{
			Menu.SetChecked(MenuItemName, IsEnabled);
			return IsEnabled;
		}


		private static void AutoSaveWhenPlaymodeStarts(PlayModeStateChange obj)
		{
			if (!IsEnabled) return;

            /*
			if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
			{
				for (var i = 0; i < SceneManager.sceneCount; i++)
				{
					var scene = SceneManager.GetSceneAt(i);
					if (scene.isDirty) EditorSceneManager.SaveScene(scene);
				}

				AssetDatabase.SaveAssets();
			}
            */

		}
	}
}
#endif
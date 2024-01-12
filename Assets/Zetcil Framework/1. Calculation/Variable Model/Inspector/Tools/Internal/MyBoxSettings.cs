#if UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TechnomediaLabs.Internal
{
	[Serializable]
	public class TechnomediaLabsSettings : ScriptableObject
	{
		[SerializeField] private bool _autoSaveEnabled = true;
		[SerializeField] private bool _cleanEmptyDirectoriesFeature = true;

		public static bool AutoSaveEnabled
		{
			get { return Instance._autoSaveEnabled; }
			set
			{
				if (Instance._autoSaveEnabled == value) return;
				Instance._autoSaveEnabled = value;
				Save();
			}
		}

		public static bool CleanEmptyDirectoriesFeature
		{
			get { return Instance._cleanEmptyDirectoriesFeature; }
			set
			{
				if (Instance._cleanEmptyDirectoriesFeature == value) return;
				Instance._cleanEmptyDirectoriesFeature = value;
				Save();
			}
		}


		#region Instance

		private static TechnomediaLabsSettings Instance
		{
			get
			{
				if (_instance != null) return _instance;
				_instance = LoadOrCreate();
				return _instance;
			}
		}

		private const string Directory = "ProjectSettings";
		private const string Path = Directory + "/TechnomediaLabsSettings.asset";
		private static TechnomediaLabsSettings _instance;

		private static void Save()
		{
			var instance = _instance;
			if (!System.IO.Directory.Exists(Directory)) System.IO.Directory.CreateDirectory(Directory);
			try
			{
				UnityEditorInternal.InternalEditorUtility.SaveToSerializedFileAndForget(new Object[] {instance}, Path, true);
			}
			catch (Exception ex)
			{
				Debug.LogError("Unable to save TechnomediaLabsSettings!\n" + ex);
			}
		}

		private static TechnomediaLabsSettings LoadOrCreate()
		{
			var settings = !File.Exists(Path) ? CreateNewSettings() : LoadSettings();
			if (settings == null)
			{
				DeleteFile(Path);
				settings = CreateNewSettings();
			}

			settings.hideFlags = HideFlags.HideAndDontSave;

			return settings;
		}


		private static TechnomediaLabsSettings LoadSettings()
		{
			TechnomediaLabsSettings settingsInstance;
			try
			{
				settingsInstance = (TechnomediaLabsSettings) UnityEditorInternal.InternalEditorUtility.LoadSerializedFileAndForget(Path)[0];
			}
			catch (Exception ex)
			{
                //Debug.LogError("Unable to read TechnomediaLabsSettings, set to defaults" + ex);
                if (ex == null) {
                }
				settingsInstance = null;
			}

			return settingsInstance;
		}

		private static TechnomediaLabsSettings CreateNewSettings()
		{
			_instance = CreateInstance<TechnomediaLabsSettings>();
			Save();

			return _instance;
		}

		private static void DeleteFile(string path)
		{
			if (!File.Exists(path)) return;

			var attributes = File.GetAttributes(path);
			if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);

			File.Delete(path);
		}

		#endregion
	}
}
#endif
using System;
using UnityEngine;

namespace Gigadrillgames.AUP.Common
{
    public class UtilsPlugin : MonoBehaviour
    {
        private static UtilsPlugin instance;
        private static GameObject container;
        private static AUPHolder aupHolder;
        private const string TAG = "[UtilsPlugin]: ";

#if UNITY_ANDROID
        private static AndroidJavaObject jo;
#endif

        public bool isDebug = true;
        private bool isInit = false;

        public static UtilsPlugin GetInstance()
        {
            if (instance == null)
            {
                container = new GameObject();
                container.name = "UtilsPlugin";
                instance = container.AddComponent(typeof(UtilsPlugin)) as UtilsPlugin;
                DontDestroyOnLoad(instance.gameObject);
                aupHolder = AUPHolder.GetInstance();
                instance.gameObject.transform.SetParent(aupHolder.gameObject.transform);
            }

            return instance;
        }

        private void Awake()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo = new AndroidJavaObject("com.gigadrillgames.androidplugin.utils.UtilsPlugin");
            }
#endif
        }

        /// <summary>
        /// initialize the Utils plugin
        /// </summary>
        public void Init()
        {
            if (isInit)
            {
                return;
            }

#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
                AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
                
                jo.CallStatic("init",currentActivity);
                isInit = true;
                Utils.Message(TAG, "init");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Sets the debug.
        /// 0 - false, 1 - true
        /// </summary>
        /// <param name="debug">Debug.</param>
        public void SetDebug(int debug)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("SetDebug", debug);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        //----------------------------------------------[Immersive]-------------------------------------------------------------
        //immersive
        //only support kitkat and above version
        /// <summary>
        /// set immersive mode on
        /// , note:only support kitkat and above android version 4.4 api 19
        /// </summary>
        /// <param name="delay">Delay.</param>
        public void ImmersiveOn(int delay)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("immersiveOn", delay);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Immersives the off.
        /// </summary>
        public void ImmersiveOff()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("immersiveOff");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        //----------------------------------------------[Immersive]-------------------------------------------------------------

        //----------------------------------------------[Android Native UI]-------------------------------------------------------------
        /// <summary>
        /// Shows the toast message.
        /// </summary>
        /// <param name="message">Message.</param>
        public void ShowToastMessage(String message)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("showToastMessage", message);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        //native popup
        //rate us
        /// <summary>
        /// Shows the  native rate popup.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="url">URL.</param>
        public void ShowRatePopup(String title, String message, String url)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("showRatePopup", title, message, url);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Shows the native alert popup.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        public void ShowAlertPopup(String title, String message)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("showNativePopup", title, message);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }
        //native popup

        //native loading
        /// <summary>
        /// Shows the native loading.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="isCancelable">If set to <c>true</c> is cancelable.</param>
        public void ShowNativeLoading(String message, bool isCancelable)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("showNativeLoading", message, isCancelable);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Hides the native loading.
        /// </summary>
        public void HideNativeLoading()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("hideNativeLoading");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }
        //----------------------------------------------[Android Native UI]-------------------------------------------------------------

        //----------------------------------------------[Android Information]-------------------------------------------------------------
        /// <summary>
        /// Gets the package identifier.
        /// </summary>
        /// <returns>The package identifier.</returns>
        public String GetPackageId()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("getPackageId");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        /// <summary>
        /// Gets the android version.
        /// </summary>
        /// <returns>The android version.</returns>
        public String GetAndroidVersion()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("getAndroidVersion");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }
        //----------------------------------------------[Android Information]-------------------------------------------------------------

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <returns>The folder directory path if successful else return "".</returns>
        /// <param name="folderName">Folder name.</param>
        /// <param name="dir">if 1 picture directory, if 2 DCIM directory, default picture directory</param>
        public String CreateFolder(string folderName, int dir)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("createFolder", folderName, dir);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String WriteNoMediaFile(string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("writeNoMediaFile", path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String SaveImageBase64(string base64, string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("saveImageBase64", base64, path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String SaveFileByte(byte[] byteArray, string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("saveFileByte", byteArray, path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String GetImageBase64(string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("getImageBase64", path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String getExternalStorageDirectory()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("getExternalStorageDirectory");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String CreateRootFolder(string folderName)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("createRootFolder", folderName);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String CreateAnyFolder(string path, string folderName)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("createAnyFolder", path, folderName);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        // returns 1 if successfully deleted, 0 failed to delete, -1 file does  not exists
        public int DeleteFileFromDevice(string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<int>("deleteFile", path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return 0;
        }

        // returns total deleted
        public int FindAndDeleteFile(string directoryPath, string key)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<int>("findAndDeleteFile", directoryPath, key);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return 0;
        }

        public void ShowToast(string val)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("showToast", val);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        public String GetDCIMPath()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("getDCIMPath");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        public String GetPicturePath()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("getPicturePath");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }

        /// <summary>
        /// Gets the always finish activity status if 0 = disable, if 1 = enable, if enable please don't show ads if you don't want your
        /// application to restart
        /// </summary>
        /// <returns>The always finish activity.</returns>
        public int GetAlwaysFinishActivity()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<int>("getAlwaysFinishActivity");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return 0;
        }

        /// <summary>
        /// Sets the always finish activity.
        /// note: will only work if this application is a system application and if this application is given a authority to write in settings
        /// </summary>
        /// <param name="val">Value.</param>
        public void SetAlwaysFinishActivity(int val)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("setAlwaysFinishActivity", val);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Refreshs the gallery use this after taking screen shots or when you add new images
        /// </summary>
        /// <param name="Path">Path.</param>
        public void RefreshGallery(string Path, ImageType imageType)
        {
            Debug.Log("Refresh Gallery " + Path);

#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
                using (AndroidJavaClass jcMediaScannerConnection =
                    new AndroidJavaClass("android.media.MediaScannerConnection"))
                using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
                using (AndroidJavaObject joExDir =
                    jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
                {
                    jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] {Path}, null, null);
                }
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        // Audio Controller
        /// <summary>
        /// Mutes the beep.
        /// disable the beep 
        /// </summary>
        public void MuteBeep()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("muteBeep");
                Utils.Message(TAG, "muteBeep");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Unmute the beep.
        /// enable the beep
        /// </summary>
        public void UnMuteBeep()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("unMuteBeep");
                Utils.Message(TAG, "unMuteBeep");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Increases the volume.
        /// </summary>
        public void IncreaseVolume()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("increaseVolume");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Decreases the volume.
        /// </summary>
        public void DecreaseVolume()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("decreaseVolume");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Increases the music volume by value.
        /// </summary>
        /// <param name="val">Value.</param>
        public void IncreaseMusicVolumeByValue(int val)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("increaseMusicVolumeByValue", val);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        /// <summary>
        /// Decreases the music volume by value.
        /// </summary>
        /// <param name="val">Value.</param>
        public void DecreaseMusicVolumeByValue(int val)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("decreaseMusicVolumeByValue", val);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        // Audio Controller

        public void UnLockScreen()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("unLockScreen");
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif
        }

        public string[] GetFileNames(string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<string[]>("getFileNames", path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return null;
        }
        
        
        public string CreateInternalDirectory(string directoryName)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<string>("createInternalDirectory", directoryName);
            }

            Debug.Log(TAG + " warning: must run in actual android device");
#endif

            return "";
        }

        public string CreateExternalDirectory(string directoryName)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<string>("createExternalDirectory", directoryName);
            }

            Debug.Log(TAG + " warning: must run in actual android device");
#endif

            return "";
        }

        public bool IsAppInstalled(string packageName)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<bool>("isAppInstalled",packageName);
            }

            Debug.Log(TAG + " warning: must run in actual android device");
#endif
            return false;
        }
        
        public void OpenGooglePlayMarket(string packageName)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                jo.CallStatic("openGooglePlayMarket", packageName);
            }
            else
            {
                Debug.Log(TAG + " warning: must run in actual android device");
            }
#endif
        }
        
        public String GetFileBase64String(string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<String>("getFileBase64String", path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return "";
        }
        
        public AndroidJavaObject  GetFileByteArray(string path)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                return jo.CallStatic<AndroidJavaObject >("getFileByteArray", path);
            }
            else
            {
                Utils.Message(TAG, "warning: must run in actual android device");
            }
#endif

            return null;
        }
    }
}
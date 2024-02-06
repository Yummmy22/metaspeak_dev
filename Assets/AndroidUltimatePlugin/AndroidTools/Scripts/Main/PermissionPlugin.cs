using System;
using UnityEngine;

namespace Gigadrillgames.AUP.Tools
{
    public class PermissionPlugin : MonoBehaviour
    {
        #region Fields

        private static PermissionPlugin _instance;
        private static GameObject _container;
        private const string _tag = "[PermissionPlugin]: ";
        private bool _hasInit = false;
        
        private Action<bool> HandlePermission;
        
        public event Action<bool> OnHandlePermission
        {
            add { HandlePermission += value; }
            remove { HandlePermission += value; }
        }
        
#if UNITY_ANDROID
        private static AndroidJavaObject _jo;
#endif

        #endregion Fields

        #region Constructors

        public static PermissionPlugin GetInstance()
        {
            if (_instance == null)
            {
                _container = new GameObject();
                _container.name = "PermissionPlugin";
                _instance = _container.AddComponent(typeof(PermissionPlugin)) as PermissionPlugin;
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }

        #endregion Constructors

        #region Methods

        private void Awake()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo = new AndroidJavaObject("com.gigadrillgames.androidplugin.permission.PermissionPlugin");
            }
#endif
        }

        public void SetDebug(int debug)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                _jo.CallStatic("setDebug", debug);
            }
            else
            {
                Debug.Log(_tag + "warning: must run in actual android device");
            }
#endif
        }

        public void Init()
        {
            if (!_hasInit)
            {
                _hasInit = true;
#if UNITY_ANDROID
                if (Application.platform == RuntimePlatform.Android)
                {
                    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
                    AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
                    
                    PermissionCallback permissionCallback = new PermissionCallback();
                    permissionCallback.OnPermission = onHandlePermission;
                    
                    _jo.CallStatic("init",currentActivity,permissionCallback);
                }
                else
                {
                    Debug.Log(_tag + "warning: must run in actual android device");
                }
#endif
            }
            else
            {
                Debug.Log(_tag + "DeepLinkingPlugin is already initialized");
            }
        }

        /*public void SetEventListener(Action<bool> OnPermission)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                PermissionCallback permissionCallback = new PermissionCallback();
                permissionCallback.OnPermission = onHandlePermission;
                _jo.CallStatic("setEventListener", permissionCallback);
            }
            else
            {
                Debug.Log(_tag + " warning: must run in actual android device");
            }
#endif
        }*/

        public bool CheckPermision(string[] permissions)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                object[] objs = new object[1];
                objs[0] = permissions;
                return _jo.CallStatic<bool>("checkPermission", objs);
            }
            else
            {
                Debug.Log(_tag + " warning: must run in actual android device");
            }
#endif

            return false;
        }

        public void AskPermission(string[] permissions)
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                object[] objs = new object[1];
                objs[0] = permissions;
                _jo.CallStatic("askPermission", objs);
            }
            else
            {
                Debug.Log(_tag + " warning: must run in actual android device");
            }
#endif
        }

        #endregion Methods

        #region Events
        private void onHandlePermission(bool status)
        {
            if (null != HandlePermission)
            {
                HandlePermission(status);
            }
        }
        #endregion Events
    }
}
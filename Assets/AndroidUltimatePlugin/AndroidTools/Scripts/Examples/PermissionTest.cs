using System.Text;
using Gigadrillgames.AUP.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.Tools
{
    public class PermissionTest : MonoBehaviour
    {
        #region Fields

        private PermissionPlugin _permissionPlugin;
        public TextMeshProUGUI permissionToCheckText;
        public TextMeshProUGUI statusText;
        public Button askPermissionButton;
        private Dispatcher _dispatcher;

        // permissions string must be the same with the permissions constant value that can be found here
        // https://developer.android.com/reference/android/Manifest.permission
        private readonly string[] _permissions =
        {
            "android.permission.INTERNET",
            "android.permission.READ_EXTERNAL_STORAGE",
            "android.permission.ACCESS_NETWORK_STATE",
            "android.permission.WRITE_EXTERNAL_STORAGE"
        };

        #endregion Fields

        #region Methods

        private void Awake()
        {
            // disable ask permission button
            EnableDisableAskPermissionButton(false);
            
            // displays permissions to check
            StringBuilder sb = new StringBuilder();
            foreach (var permission in _permissions)
            {
                sb.Append("\n" + permission);
            }
            permissionToCheckText?.SetText(sb.ToString());
            
            _dispatcher = Dispatcher.GetInstance();

            // initialize the permission plugin
            _permissionPlugin = PermissionPlugin.GetInstance();
            _permissionPlugin.Init();
            _permissionPlugin.SetDebug(0);
            _permissionPlugin.OnHandlePermission += HandlePermissions;
        }

        private void OnDestroy()
        {
            _permissionPlugin.OnHandlePermission -= HandlePermissions;
        }

        private void EnableDisableAskPermissionButton(bool enable)
        {
            if (askPermissionButton!=null)
            {
                askPermissionButton.enabled = enable;    
            }
        }

        public void CheckPermission()
        {
#if UNITY_EDITOR
            statusText?.SetText("CheckPermission, You must call this on actual android mobile device!");
#else
            statusText?.SetText("Checking Permission...");
            if (_permissionPlugin.CheckPermision(_permissions))
            {
                statusText?.SetText("Permission is Enabled!");
            }
            else
            {
                statusText?.SetText("Permission is disabled!");
                EnableDisableAskPermissionButton(true);
            }
#endif
        }

        public void AskPermission()
        {
#if UNITY_EDITOR
            statusText?.SetText("AskPermission, You must call this on actual android mobile device!");
#else
            EnableDisableAskPermissionButton(false);
            statusText?.SetText("Asking Permission...");
            _permissionPlugin.AskPermission(_permissions);
#endif
        }

        #endregion Methods

        #region Events

        /// <summary>
        ///  Will received an response selected  by user val will be true if user
        ///  accepts the permissions else false if they didn't accept it
        /// </summary>
        /// <param name="val"></param>
        private void HandlePermissions(bool val)
        {
            Debug.Log($"HandlePermissions: {val}");
            _dispatcher.InvokeAction(
                () =>
                {
                    if (val)
                    {
                        // do something here based on user response
                        statusText?.SetText("Permission allowed by user!");
                        EnableDisableAskPermissionButton(false);
                    }
                    else
                    {
                        statusText?.SetText("Permission deny by user!");
                        EnableDisableAskPermissionButton(true);
                    }
                }
            );
        }

        #endregion Events
    }
}
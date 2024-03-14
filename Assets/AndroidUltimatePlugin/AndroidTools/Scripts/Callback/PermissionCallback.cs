using System;
using UnityEngine;

namespace Gigadrillgames.AUP.Tools
{
    public class PermissionCallback : AndroidJavaProxy
    {
        #region Fields

        public Action<bool> OnPermission;

        #endregion Fields

        #region Constructors

        public PermissionCallback() : base("com.gigadrillgames.androidplugin.permission.IPermission")
        {
        }

        #endregion Constructors

        #region Events

        void HandlePermission(bool val)
        {
            OnPermission(val);
        }

        #endregion Events
    }
}
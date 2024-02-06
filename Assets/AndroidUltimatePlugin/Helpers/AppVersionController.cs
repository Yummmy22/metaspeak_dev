using Gigadrillgames.AUP.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Gigadrillgames.AUP.Helpers
{
    public class AppVersionController : MonoBehaviour
    {
        public Text versionText;

        private void OnEnable()
        {
            UpdateVersionText();
        }

        private void UpdateVersionText()
        {
            if (versionText != null)
            {
                versionText.text = "version: " + MasterConfig.BuildConfig.Version;
            }
        }
    }
}
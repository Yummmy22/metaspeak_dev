using UnityEngine;

namespace Gigadrillgames.AUP.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BuildConfig", menuName = "ScriptableObjects/Config/AndroidKeyStoreConfig", order = 1)]
    public class BuildConfig : ScriptableObject
    {
        // use for signing up a released apk build
        public string Version;
        public int BundleVersionCode;
    }
}
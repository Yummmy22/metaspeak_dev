using Gley.Common;

namespace Gley.MobileAds.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        public const string menuItem = "Tools/Gley/Mobile Ads";
        public const string admobPatch = "Gley/MobileAds/Patches/AdmobPatch.unitypackage";
        public const string testScene = "Example/Scenes/MobileAdsTest.unity";
        public const string exampleScene = "Example/Scenes/MobileAdsExample.unity";
        public const string documentation = "https://gley.gitbook.io/mobile-ads/";

        public const string GLEY_ADCOLONY = "GLEY_ADCOLONY";
        public const string GLEY_ADMOB = "GLEY_ADMOB";
        public const string GLEY_CHARTBOOST = "GLEY_CHARTBOOST";
        public const string GLEY_UNITYADS = "GLEY_UNITYADS";
        public const string GLEY_VUNGLE = "GLEY_VUNGLE";
        public const string GLEY_APPLOVIN = "GLEY_APPLOVIN";
        public const string GLEY_LEVELPLAY = "GLEY_LEVELPLAY";
        public const string GLEY_ATT = "GLEY_ATT";
        public const string GLEY_PATCH_ADMOB = "GLEY_PATCH_ADMOB";

        public string versionFilePath => "/Scripts/Version.txt";
        public string windowName => "Mobile Ads - v.";
        public int minWidth => 520;
        public int minHeight => 520;
        public string folderName => "MobileAds";
        public string parentFolder => "Gley";
    }
}

using Gley.Common;

namespace Gley.CrossPromo.Editor
{
    public class SettingsWindowProperties : ISettingsWindowProperties
    {
        public const string menuItem = "Tools/Gley/Cross Promo";
        internal const string documentation = "https://gley.gitbook.io/mobile-cross-promo/";
        internal const string autoLoadScene = "Example/Scenes/AutoloadTest.unity";
        internal const string loadAndShowScene= "Example/Scenes/LoadAndShowTest.unity";

        public string versionFilePath => "/Scripts/Version.txt";

        public string windowName => "Cross Promo - v.";

        public int minWidth => 520;

        public int minHeight => 520;

        public string folderName => "CrossPromo";

        public string parentFolder => "Gley";
    }
}
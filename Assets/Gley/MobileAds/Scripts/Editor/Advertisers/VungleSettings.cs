using Gley.MobileAds.Internal;

namespace Gley.MobileAds.Editor
{
    public class VungleSettings : IAdvertiserSettings
    {
        public SupportedAdvertisers advertiser => SupportedAdvertisers.Vungle;

        public string preprocessorDirective => SettingsWindowProperties.GLEY_VUNGLE;

        public string sdkLink => "https://publisher.vungle.com/sdk_/plugins";

        public SupportedPlatforms[] supportedPlatforms => new SupportedPlatforms[] { SupportedPlatforms.Android, SupportedPlatforms.iOS };

        public string appIdDisplayName => "APP ID";

        public string bannerDisplayName => "Banner Placement ID";

        public string interstitialDisplayName => "Interstitial Placement ID";

        public string rewardedVideoDisplayName => "Rewarded Placement ID";

        public string rewardedInterstitialDisplayName => "";

        public string nativeAdsDisplayName => "";

        public string appOpenAdsDisplayName => "";

        public bool directedForChildrenRequired => false;

        public bool testModeRequired => false;

        public bool consentPopupRequired => false;

        public string mRecDisplayName => "MRec Placement ID";

        public bool testDeviceRequired => false;
    }
}

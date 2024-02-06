using Gley.MobileAds.Internal;

namespace Gley.MobileAds.Editor
{
    public class AdColonySettings : IAdvertiserSettings
    {
        public SupportedAdvertisers advertiser => SupportedAdvertisers.AdColony;

        public string preprocessorDirective => SettingsWindowProperties.GLEY_ADCOLONY;

        public string sdkLink => "https://github.com/AdColony/AdColony-Unity-Plugin";

        public SupportedPlatforms[] supportedPlatforms => new SupportedPlatforms[] { SupportedPlatforms.Android, SupportedPlatforms.iOS };


        public string appIdDisplayName => "App ID";

        public string bannerDisplayName => "Banner Zone ID";

        public string interstitialDisplayName => "Interstitial Zone ID";

        public string rewardedVideoDisplayName => "Rewarded Zone ID";

        public string rewardedInterstitialDisplayName => "";

        public string nativeAdsDisplayName => "";

        public string appOpenAdsDisplayName => "";

        public bool directedForChildrenRequired => true;

        public bool testModeRequired => false;

        public bool consentPopupRequired => true;

        public string mRecDisplayName => "MRec Zone ID";

        public bool testDeviceRequired => false;
    }
}

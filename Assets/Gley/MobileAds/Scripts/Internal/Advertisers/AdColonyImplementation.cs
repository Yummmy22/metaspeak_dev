#if GLEY_ADCOLONY
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using AdColony;

namespace Gley.MobileAds.Internal
{
    internal class AdColonyImplementation : MonoBehaviour, IAdProvider
    {
        private const float reloadTime = 30;
        private readonly int maxRetryCount = 10;

        private UnityAction<bool> onRewardedVideoClosed;
        private UnityAction onInterstitialClosed;
        private UnityAction onInitialized;
        private AdColonyAdView bannerAd;
        private InterstitialAd interstitialAd;
        private InterstitialAd rewardedAd;
        private Events events;
        private string appId;
        private string bannerZoneId;
        private string mrecZoneId;
        private string interstitialZoneId;
        private string rewardedZoneId;
        private int currentRetryRewardedVideo;
        private int currentRetryInterstitial;
        private bool canShowBanner;
        private bool initialized;
        private bool directedForChildren;


        #region Initialize
        #region Interface Implementation
        public void SetDirectedForChildren(bool active)
        {
            directedForChildren = active;
        }

        /// <summary>
        /// Initializing AdColony
        /// </summary>
        /// <param name="consent">user consent -> if true show personalized ads</param>
        /// <param name="platformSettings">contains all required settings for this publisher</param>
        public void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized)
        {
            if (initialized == false)
            {
                events = new Events();
                UserConsent consent = GetConsent(Constants.GDPR_KEY);
                UserConsent ccpaConsent = GetConsent(Constants.CCPA_KEY);
                //get settings
                PlatformSettings settings = platformSettings;
                this.onInitialized = onInitialized;

                //apply settings
                appId = settings.appId.id;
                bannerZoneId = settings.idBanner.id;
                interstitialZoneId = settings.idInterstitial.id;
                rewardedZoneId = settings.idRewarded.id;
                mrecZoneId = settings.idMRec.id;

                //add listeners
                Ads.OnConfigurationCompleted += OnConfigurationCompleted;
                Ads.OnRequestInterstitial += OnRequestInterstitial;
                Ads.OnRequestInterstitialFailedWithZone += OnRequestInterstitialFailed;
                Ads.OnClosed += OnClosed;
                Ads.OnRewardGranted += OnRewardGranted;
                Ads.OnAdViewLoaded += BannerLoaded;
                Ads.OnAdViewFailedToLoad += BannerLoadFailed;

                //preparing AdColony SDK for initialization
                AppOptions appOptions = new AppOptions();
                appOptions.SetPrivacyFrameworkRequired(AppOptions.GDPR, true);
                if (consent == UserConsent.Unset || consent == UserConsent.Accept)
                {
                    appOptions.SetPrivacyConsentString(AppOptions.GDPR, "1");
                }
                else
                {
                    appOptions.SetPrivacyConsentString(AppOptions.GDPR, "0");
                }

                appOptions.SetPrivacyFrameworkRequired(AppOptions.CCPA, true);
                if (ccpaConsent == UserConsent.Unset || consent == UserConsent.Accept)
                {
                    appOptions.SetPrivacyConsentString(AppOptions.CCPA, "1");
                }
                else
                {
                    appOptions.SetPrivacyConsentString(AppOptions.CCPA, "0");
                }

                if (directedForChildren == true)
                {
                    appOptions.SetPrivacyFrameworkRequired(AppOptions.COPPA, true);
                }

                List<string> zoneIDs = new List<string>();
                if (!string.IsNullOrEmpty(bannerZoneId))
                {
                    zoneIDs.Add(bannerZoneId);
                }

                if (!string.IsNullOrEmpty(mrecZoneId))
                {
                    zoneIDs.Add(mrecZoneId);
                }

                if (!string.IsNullOrEmpty(interstitialZoneId))
                {
                    zoneIDs.Add(interstitialZoneId);
                }
                if (!string.IsNullOrEmpty(rewardedZoneId))
                {
                    zoneIDs.Add(rewardedZoneId);
                }

                if (zoneIDs.Count == 0)
                {
                    Debug.LogError("Please add your IDs in SettingsWindow");
                    return;
                }

                //Apply configuration
                Ads.Configure(appId, appOptions, zoneIDs.ToArray());

                //verify settings
                GleyLogger.AddLog($"{settings.appId.displayName} : {appId}");
                GleyLogger.AddLog($"{settings.idBanner.displayName} : {bannerZoneId}");
                GleyLogger.AddLog($"{settings.idMRec.displayName} : {mrecZoneId}");
                GleyLogger.AddLog($"{settings.idInterstitial.displayName} : {interstitialZoneId}");
                GleyLogger.AddLog($"{settings.idRewarded.displayName} : {rewardedZoneId}");
                GleyLogger.AddLog($"Directed for children: {directedForChildren}");

            }
        }
        #endregion


        /// <summary>
        /// AdColony specific event triggered after initialization is done
        /// </summary>
        /// <param name="zones_"></param>
        private void OnConfigurationCompleted(List<Zone> zones_)
        {
            GleyLogger.AddLog("OnConfigurationCompleted called");

            if (zones_ == null || zones_.Count <= 0)
            {
                GleyLogger.AddLog("Configuration Failed");
            }
            else
            {
                GleyLogger.AddLog("Configuration Succeeded.");
            }
            RequestInterstitial();
            RequestRewarded();
            initialized = true;
            onInitialized?.Invoke();
        }
        #endregion


        #region Banner
        #region InterfaceImplementation
        public void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            ShowBanner(bannerZoneId, position, bannerType, customSize, customPosition);
        }


        public void HideBanner()
        {
            canShowBanner = false;
            if (bannerAd != null)
            {
                bannerAd.DestroyAdView();
            }
        }
        #endregion
        public void ShowBanner(string bannerID, BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            HideBanner();
            canShowBanner = true;
            RequestBanner(bannerID, bannerType, position);
        }

        /// <summary>
        /// Request a banner
        /// </summary>
        private void RequestBanner(string bannerID, BannerType bannerType, BannerPosition position)
        {
            if (string.IsNullOrEmpty(bannerID))
            {
                return;
            }
            GleyLogger.AddLog("Request banner");

            AdOptions adOptions = new AdOptions();
            adOptions.ShowPrePopup = false;
            adOptions.ShowPostPopup = false;

            AdSize adSize = AdSize.Banner;
            AdPosition adPosition = AdPosition.Top;

            switch (bannerType)
            {
                case BannerType.Banner:
                    adSize = AdSize.Banner;
                    break;
                case BannerType.Leaderboard:
                    adSize = AdSize.Leaderboard;
                    break;
                case BannerType.MediumRectangle:
                    adSize = AdSize.MediumRectangle;
                    break;
                case BannerType.Skyscraper:
                    adSize = AdSize.SKYSCRAPER;
                    break;
                default:
                    GleyLogger.AddLog($"Banner Type: {bannerType} not supported by AdColony, BannerType.Banner will be used");
                    break;
            }

            switch (position)
            {
                case BannerPosition.Bottom:
                    adPosition = AdPosition.Bottom;
                    break;
                case BannerPosition.BottomLeft:
                    adPosition = AdPosition.BottomLeft;
                    break;
                case BannerPosition.BottomRight:
                    adPosition = AdPosition.BottomRight;
                    break;
                case BannerPosition.Center:
                    adPosition = AdPosition.Center;
                    break;
                case BannerPosition.Top:
                    adPosition = AdPosition.Top;
                    break;
                case BannerPosition.TopLeft:
                    adPosition = AdPosition.TopLeft;
                    break;
                case BannerPosition.TopRight:
                    adPosition = AdPosition.TopRight;
                    break;
                default:
                    GleyLogger.AddLog($"Banner Position: {position} not supported by AdColony, BannerPosition.Top will be used");
                    break;
            }

            Ads.RequestAdView(bannerID, adSize, adPosition, null);
        }


        private void BannerLoadFailed(AdColonyAdView obj)
        {
            GleyLogger.AddLog("Banner Load Failed ");
            events.TriggerBannerLoadFailed($"{obj.Id} Failed");
        }

        private void BannerLoaded(AdColonyAdView ad)
        {
            bannerAd = ad;
            if (canShowBanner)
            {
                GleyLogger.AddLog("Banner Loaded");
                events.TriggerBannerLoadSucces();
            }
            else
            {
                GleyLogger.AddLog("Banner closed before loading");
                bannerAd.DestroyAdView();
            }
        }
        #endregion


        #region Interstitial
        #region Interface Implementation
        /// <summary>
        /// Check if AdColony interstitial is available
        /// </summary>
        /// <returns>true if an interstitial is available</returns>
        public bool IsInterstitialAvailable()
        {
            if (interstitialAd != null)
            {
                if (interstitialAd.ZoneId == interstitialZoneId)
                {
                    if (interstitialAd.Expired == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Show AdColony interstitial
        /// </summary>
        /// <param name="InterstitialClosed">callback called when user closes interstitial</param>
        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
            if (interstitialAd != null)
            {
                onInterstitialClosed = InterstitialClosed;
                Ads.ShowAd(interstitialAd);
            }
        }
        #endregion


        /// <summary>
        /// Request an interstitial
        /// </summary>
        private void RequestInterstitial()
        {
            if (string.IsNullOrEmpty(interstitialZoneId))
            {
                return;
            }
            GleyLogger.AddLog("Request Interstitial");

            AdOptions adOptions = new AdOptions();
            adOptions.ShowPrePopup = false;
            adOptions.ShowPostPopup = false;
            Ads.RequestInterstitialAd(interstitialZoneId, adOptions);
        }
        #endregion


        #region Rewarded
        #region Interface Implementation
        /// <summary>
        /// Check if AdColony rewarded video is available
        /// </summary>
        /// <returns>true if a rewarded video is available</returns>
        public bool IsRewardedVideoAvailable()
        {
            if (rewardedAd != null)
            {
                if (rewardedAd.ZoneId == rewardedZoneId)
                {
                    if (rewardedAd.Expired == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Show AdColony rewarded video
        /// </summary>
        /// <param name="CompleteMethod">callback called when user closes the rewarded video -> if true video was not skipped</param>
        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
            if (rewardedAd != null)
            {
                onRewardedVideoClosed = CompleteMethod;
                Ads.ShowAd(rewardedAd);
            }
        }
        #endregion


        /// <summary>
        /// Request a rewarded video 
        /// </summary>
        private void RequestRewarded()
        {
            if (string.IsNullOrEmpty(rewardedZoneId))
            {
                return;
            }
            GleyLogger.AddLog("Request Rewarded");

            AdOptions adOptions = new AdOptions();
            adOptions.ShowPrePopup = false;
            adOptions.ShowPostPopup = false;
            Ads.RequestInterstitialAd(rewardedZoneId, adOptions);
        }

        /// <summary>
        /// AdColony specific event triggered after a rewarded video is closed
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="success"></param>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        private void OnRewardGranted(string zoneId, bool success, string name, int amount)
        {
            if (zoneId == rewardedZoneId)
            {
                GleyLogger.AddLog(string.Format(" OnRewardGranted called\n\tzoneId: {0}\n\tsuccess: {1}\n\tname: {2}\n\tamount: {3}", zoneId, success, name, amount));


                if (success)
                {
                    if (onRewardedVideoClosed != null)
                    {
                        onRewardedVideoClosed(true);
                        onRewardedVideoClosed = null;
                    }
                }
                else
                {
                    if (onRewardedVideoClosed != null)
                    {
                        onRewardedVideoClosed(false);
                        onRewardedVideoClosed = null;
                    }
                }
            }
        }


        /// <summary>
        /// AdColony specific event triggered after an ad is closed
        /// </summary>
        /// <param name="ad_"></param>
        private void OnClosed(InterstitialAd ad_)
        {
            GleyLogger.AddLog($"OnClosed called, expired: {ad_.Expired}");


            if (ad_.ZoneId == interstitialZoneId)
            {
                if (onInterstitialClosed != null)
                {
                    onInterstitialClosed();
                    onInterstitialClosed = null;
                }

                interstitialAd = ad_;
                if (interstitialAd.Expired)
                {
                    interstitialAd = null;
                    RequestInterstitial();
                }
            }

            if (ad_.ZoneId == rewardedZoneId)
            {
                rewardedAd = ad_;
                if (rewardedAd.Expired)
                {
                    rewardedAd = null;
                    RequestRewarded();
                }
            }
        }


        /// <summary>
        /// AdColony specific event triggered when an AdColony video failed to load
        /// </summary>
        /// <param name="zoneID"></param>
        private void OnRequestInterstitialFailed(string zoneID)
        {
            GleyLogger.AddLog("Load Ad Failed");

            if (zoneID == interstitialZoneId)
            {
                if (currentRetryInterstitial < maxRetryCount)
                {
                    currentRetryInterstitial++;
                    GleyLogger.AddLog($"Interstitial Failed->Retry {currentRetryInterstitial}");

                    Invoke("RequestInterstitial", reloadTime);
                }
                events.TriggerInterstitialLoadFailed($"{zoneID} Failed");
            }
            if (zoneID == rewardedZoneId)
            {
                if (currentRetryRewardedVideo < maxRetryCount)
                {
                    currentRetryRewardedVideo++;
                    GleyLogger.AddLog($"Rewarded Video Failed->Retry {currentRetryRewardedVideo}");

                    Invoke("RequestRewarded", reloadTime);
                }
                events.TriggerRewardedVideoLoadFailed($"{zoneID} Failed");
            }
        }


        /// <summary>
        /// AdColony specific event triggered when an ad was loaded
        /// </summary>
        /// <param name="ad_"></param>
        private void OnRequestInterstitial(InterstitialAd ad_)
        {
            GleyLogger.AddLog($"OnRequestInterstitial called id: {ad_.ZoneId}");

            if (ad_.ZoneId == interstitialZoneId)
            {
                currentRetryInterstitial = 0;
                interstitialAd = ad_;
                events.TriggerInterstitialLoadSucces();
            }

            if (ad_.ZoneId == rewardedZoneId)
            {
                currentRetryRewardedVideo = 0;
                rewardedAd = ad_;
                events.TriggerRewardedVideoLoadSucces();
            }
        }
        #endregion


        #region MRec
        public void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            ShowBanner(mrecZoneId, position, BannerType.MediumRectangle, new Vector2Int(), customPosition);
        }

        public void HideMRec()
        {
            HideBanner();
        }
        #endregion


        #region Resume
        public void LoadAdsOnResume()
        {
            if (IsInterstitialAvailable() == false)
            {
                if (currentRetryInterstitial == maxRetryCount)
                {
                    RequestInterstitial();
                }
            }

            if (IsRewardedVideoAvailable() == false)
            {
                if (currentRetryRewardedVideo == maxRetryCount)
                {
                    RequestRewarded();
                }
            }
        }
        #endregion


        #region Auto Consent
        public bool HasBuiltInConsentWindow()
        {
            return false;
        }

        public void ShowBuiltInConsentWindow(UnityAction consentPopupClosed)
        {
            GleyLogger.AddLog($"ShowBuiltInConsentWindow Not supported on {SupportedAdvertisers.AdColony}");
        }
        #endregion


        #region ManualConsent
        #region InterfaceImplementation
        public bool GDPRConsentWasSet()
        {
            return ConsentWasSet(Constants.GDPR_KEY);
        }


        /// <summary>
        /// Used to set user consent that will be later forwarded to each advertiser SDK
        /// Should be set before initializing the SDK
        /// </summary>
        /// <param name="accept">if true -> show personalized ads, if false show unpersonalized ads</param>
        public void SetGDPRConsent(bool accept)
        {
            if (accept == true)
            {
                PlayerPrefs.SetInt(Constants.GDPR_KEY, (int)UserConsent.Accept);
            }
            else
            {
                PlayerPrefs.SetInt(Constants.GDPR_KEY, (int)UserConsent.Deny);
            }

            if (initialized)
            {
                AppOptions appOptions = Ads.GetAppOptions();
                appOptions.SetPrivacyFrameworkRequired(AppOptions.GDPR, true);
                if (accept)
                {
                    appOptions.SetPrivacyConsentString(AppOptions.GDPR, "1");
                }
                else
                {
                    appOptions.SetPrivacyConsentString(AppOptions.GDPR, "0");
                }
                Ads.SetAppOptions(appOptions);
            }
        }


        public UserConsent GetGDPRConsent()
        {
            return GetConsent(Constants.GDPR_KEY);
        }


        public bool CCPAConsentWasSet()
        {
            return ConsentWasSet(Constants.CCPA_KEY);
        }


        /// <summary>
        /// Used to set user consent that will be later forwarded to each advertiser SDK
        /// Should be set before initializing the SDK
        /// </summary>
        /// <param name="accept">if true -> show personalized ads, if false show unpersonalized ads</param>
        public void SetCCPAConsent(bool accept)
        {
            if (accept == true)
            {
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Accept);
            }
            else
            {
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Deny);
            }

            if (initialized)
            {
                AppOptions appOptions = Ads.GetAppOptions();
                appOptions.SetPrivacyFrameworkRequired(AppOptions.CCPA, true);

                if (accept)
                {
                    appOptions.SetPrivacyConsentString(AppOptions.CCPA, "1");
                }
                else
                {
                    appOptions.SetPrivacyConsentString(AppOptions.CCPA, "0");
                }
                Ads.SetAppOptions(appOptions);
            }
        }


        public UserConsent GetCCPAConsent()
        {
            return GetConsent(Constants.CCPA_KEY);
        }
        #endregion


        private UserConsent GetConsent(string fileName)
        {
            if (!ConsentWasSet(fileName))
                return UserConsent.Unset;
            return (UserConsent)PlayerPrefs.GetInt(fileName);
        }


        private bool ConsentWasSet(string fileName)
        {
            return PlayerPrefs.HasKey(fileName);
        }
        #endregion


        #region Debug
        public void OpenDebugWindow()
        {
            GleyLogger.AddLog($"OpenDebugWindow Not supported on {SupportedAdvertisers.AdColony}");
        }
        #endregion


        #region NotSupported
        public bool IsAppOpenAvailable()
        {
            return false;
        }

        public void ShowAppOpen(UnityAction appOpenClosed)
        {
            GleyLogger.AddLog($"ShowAppOpen Not supported on {SupportedAdvertisers.AdColony}");
        }

        public bool IsRewardedInterstitialAvailable()
        {
            return false;
        }

        public void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            GleyLogger.AddLog($"ShowRewardedInterstitial Not supported on {SupportedAdvertisers.AdColony}");
        }
        #endregion
    }
}
#endif
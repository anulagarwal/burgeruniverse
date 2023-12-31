using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class AppOpenAdM : MonoBehaviour
{
    private DateTime loadTime;

    //#if UNITY_ANDROID
    //    //private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/3419835294"; //TEST
    private const string AD_UNIT_ID = "ca-app-pub-1435251642715983/7288394392"; //REAL
    //#elif UNITY_IOS
    //private const string AD_UNIT_ID = "ca-app-pub-1435251642715983/3012232646";
    //#else
    //        private const string AD_UNIT_ID = "unexpected_platform";
    //#endif

    private static AppOpenAdM instance;

    private AppOpenAd ad;

    private bool isShowingAd = false;

    public static AppOpenAdM Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppOpenAdM();
            }

            return instance;
        }
    }

    private bool IsAdAvailable
    {
        get
        {
            return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }
    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            ShowAdIfAvailable();
        }
    }
    public void LoadAd()
    {
       
    }

    public void ShowAdIfAvailable()
    {
        if (!IsAdAvailable || isShowingAd || (Time.timeSinceLevelLoad > 2f))
        {
            return;
        }

        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();
    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                args.AdValue.CurrencyCode, args.AdValue.Value);
    }
}

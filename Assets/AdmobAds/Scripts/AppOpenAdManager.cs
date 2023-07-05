using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AppOpenAdManager : MonoBehaviour
{

    public string testAndroidAdUnitID = "ca-app-pub-3940256099942544/3419835294";
    public string realAndroidAdUnitID;
    [Space]
    public string testIOSAdUnitID;
    public string realIOSAdUnitID;

    private string tempAdUnitID;

    private void SetTempAdUnit()
    {
        if (AdManager.Instance.isBuildingForIOS)
        {
            if (AdManager.Instance.isShowingRealAds)
                tempAdUnitID = realIOSAdUnitID; //REAL IOS
            else
                tempAdUnitID = testIOSAdUnitID; //TEST IOS
        }
        else
        {
            if (AdManager.Instance.isShowingRealAds)
                tempAdUnitID = realAndroidAdUnitID; //REAL ANDROID
            else
                tempAdUnitID = testAndroidAdUnitID; //TEST ANDROID
        }
    }

    private AppOpenAd ad;
    private static AppOpenAdManager instance;

    private bool isShowingAd = false;

    public static AppOpenAdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppOpenAdManager();
            }

            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }
    public void Start()
    {
        // Load an app open ad when the scene starts
        LoadAdOnStart();
    }
    private DateTime loadTime;

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
        //if (!paused)
        //{
        //    ShowAdIfAvailable();
        //}
    }

    public void LoadAdOnStart()
    {
        //IS FIRST OPEN? THEN DO NOT SHOW - IOS ONLY
        if (AdManager.Instance.isBuildingForIOS)
        {
            if (PlayerPrefs.GetInt("ShowAppOpenAd", 0) == 0)
            {
                PlayerPrefs.SetInt("ShowAppOpenAd", 1);
                return;
            }
        }
        //---------

        if (IsAdAvailable)
        {
            return;
        }
        AdRequest request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        SetTempAdUnit();
        AppOpenAd.LoadAd(tempAdUnitID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            // App open ad is loaded.
            ad = appOpenAd;
            loadTime = DateTime.UtcNow;
            ShowAdIfAvailable();
        }));
    }
    public void LoadAd()
    {
        if (IsAdAvailable)
        {
            return;
        }
        AdRequest request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        SetTempAdUnit();
        AppOpenAd.LoadAd(tempAdUnitID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            // App open ad is loaded.
            ad = appOpenAd;
            loadTime = DateTime.UtcNow;
        }));
    }
    public void ShowAdIfAvailable()
    {
        if (!IsAdAvailable || isShowingAd)
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
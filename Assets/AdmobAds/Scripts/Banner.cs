using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class Banner : MonoBehaviour
{
    public static Banner Instance;
    private void Awake()
    {
        Instance = this;
    }

    private BannerView bannerView;

    public string testAndroidAdUnitID = "ca-app-pub-3940256099942544/6300978111";
    public string realAndroidAdUnitID;
    [Space]
    public string testIOSAdUnitID= "ca-app-pub-3940256099942544/2934735716";
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

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        SetTempAdUnit();

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(tempAdUnitID, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}
using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
    public static Interstitial Instance;
    private void Awake()
    {
        Instance = this;
    }

    public string testAndroidAdUnitID = "ca-app-pub-3940256099942544/8691691433";
    public string realAndroidAdUnitID;
    [Space]
    public string testIOSAdUnitID= "ca-app-pub-3940256099942544/4411468910";
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

    void Start()
    {
       
    }

    public void ShowInterstitialAd()
    {

    }

    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
       
    }
}

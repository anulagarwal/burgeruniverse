using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdManager : MonoBehaviour
{
    public bool isShowingRealAds = false;
    [HideInInspector] public bool isBuildingForIOS = false;
    [Space]

    public float timeBetweenAdsInMinutes = 3f;
    private bool rewardedComing = false;

    public int showRewardedAfterThisManyInterstitials = 2;
    private int tempInterstitialIndex = 0;

    public bool isShowingAdsBasedOnTimePassing = true;


    public static AdManager Instance;
    private void Awake()
    {
        isBuildingForIOS = (Application.platform == RuntimePlatform.IPhonePlayer);

        Instance = this;
    }

    IEnumerator Start()
    {
        //Load an app open ad when the scene starts
        //AppOpenAdM.Instance.LoadAd();

        // Listen to application foreground and background events.
        //AppStateEventNotifier.AppStateChanged += OnAppStateChanged;


        while (isShowingAdsBasedOnTimePassing)
        {
            yield return new WaitForSeconds(timeBetweenAdsInMinutes * 60f);

            if (rewardedComing)
            {
                Rewarded.Instance.ShowRewardedAd(-1);
                rewardedComing = false;
            }
            else
            {
               // Interstitial.Instance.ShowInterstitialAd();
                tempInterstitialIndex++;

                if (tempInterstitialIndex > showRewardedAfterThisManyInterstitials)
                {
                    tempInterstitialIndex = 0;
                    rewardedComing = true;
                }
            }
        }
    }

    //private void OnApplicationPause(bool paused)
    //{
    //    // Display the app open ad when the app is foregrounded.
    //    if (!paused)
    //    {
    //        AppOpenAdM.Instance.ShowAdIfAvailable();
    //    }
    //}
}

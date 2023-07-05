using GoogleMobileAds.Api;
using System;
using UnityEngine;


public class Rewarded : MonoBehaviour
{
    public static Rewarded Instance;
    private void Awake()
    {
        Instance = this;
    }

    public string testAndroidAdUnitID = "ca-app-pub-3940256099942544/5224354917";
    public string realAndroidAdUnitID;
    [Space]
    public string testIOSAdUnitID = "ca-app-pub-3940256099942544/1712485313";
    public string realIOSAdUnitID;

    private string tempAdUnitID;
    public bool loadedAd = true;
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

    private RewardedAd rewardedAd;

    private void Start()
    {       
        RequestRewardedVideo();
    }

    private int rewardType;
    private CapsuleCollider capsuleCollider;
    private GameObject senderObj;

    public void ShowRewardedAd(int tempRewardType, CapsuleCollider capsuleColl = null, GameObject sender = null)
    {
        print(tempRewardType);
        rewardType = tempRewardType;
        if (capsuleColl != null)
            capsuleCollider = capsuleColl;

        if (sender != null)
            senderObj = sender;

        if (loadedAd)
        GameDistribution.Instance.ShowRewardedAd();
        else
        {
            RequestRewardedVideo();
        }

    }

    public void RequestRewardedVideo()
    {
        //SetTempAdUnit();

        //  GameDistribution.OnResumeGame += OnResumeGame;
        //  GameDistribution.OnPauseGame += OnPauseGame;
        GameDistribution.OnPreloadRewardedVideo += HandleRewardedAdLoaded;

       // GameDistribution.OnRewardedVideoSuccess += OnRewardedVideoSuccess;
        GameDistribution.OnRewardedVideoFailure += HandleRewardedAdFailedToShow;
        GameDistribution.OnRewardGame += HandleUserEarnedReward;

        /*
                this.rewardedAd = new RewardedAd(tempAdUnitID);

                // Called when an ad request has successfully loaded.
                this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
                // Called when an ad request failed to load.
                this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
                // Called when an ad is shown.
                this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
                // Called when an ad request failed to show.
               // this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
                // Called when the user should be rewarded for interacting with the ad.
               // this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
                // Called when the ad is closed.
                this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded ad with the request.
                this.rewardedAd.LoadAd(request);
        */
        GameDistribution.Instance.PreloadRewardedAd();
    }

    public void HandleRewardedAdLoaded(int loaded)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        if(loaded == 1)
        {
            loadedAd = true;
        }
        else
        {
            loadedAd = false;
        }
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestRewardedVideo();
        //MonoBehaviour.print(
        //    "HandleRewardedAdFailedToLoad event received with message: "
        //                     + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow()
    {
       
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestRewardedVideo();
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward()
    {
        //string type = args.Type;
        //double amount = args.Amount;
        //MonoBehaviour.print(
        //    "HandleRewardedAdRewarded event received for "
        //                + amount.ToString() + " " + type);

        switch (rewardType)
        {
            case -1:
                break;

            //MONEY ON GROUND
            case 0:
                capsuleCollider.enabled = true;
                capsuleCollider.GetComponentInParent<RandomReward>().GiveReward_DisableChildren();
                VibrationManager.Instance.Vibr_SoftImpact();
                break;
            //UPDATE FREE PURCHASE
            case 1:
                senderObj.GetComponent<PurchaseRewarded>().SuccessfulRewarded();
                VibrationManager.Instance.Vibr_SoftImpact();
                break;
            //SPIN WHEEL REWARD
            case 2:
                senderObj.GetComponent<BonusTimerCountDown>().AddRewardSpin();
                VibrationManager.Instance.Vibr_SoftImpact();
                break;
            //VESPA_AND_TABLE REWARD
            case 3:
                senderObj.GetComponent<LockedCapsule>().CapsuleBuy(MoneySpawner.Instance.tempMoney);
                VibrationManager.Instance.Vibr_SoftImpact();
                break;
            //DRINK WAITER
            case 4:
                //capsuleCollider.enabled = true;
                //capsuleCollider.GetComponentInParent<RandomReward>().GiveReward_DisableChildren();
                PlayerPrefs.SetInt("DrinkWaiter", 1);
                senderObj.GetComponent<Waiter_Drinks>().enabled = true;
                VibrationManager.Instance.Vibr_SoftImpact();
                break;
        }

        Debug.Log("REWARD ADDED");
    }
}

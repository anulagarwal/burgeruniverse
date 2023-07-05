using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseRewarded : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        Rewarded.Instance.ShowRewardedAd(1, null, gameObject);
    }

    public UpgradesManager upgrManager;
    public int upgradeIndex;
    public ButtonDollarManager dollarManager;


    public void SuccessfulRewarded()
    {
        MoneySpawner.Instance.UpdateMoney(upgrManager.GetTempPriceOfUpdate(upgradeIndex));
        upgrManager.Upgrade(upgradeIndex);
        dollarManager.SetTempAnim();
    }
}

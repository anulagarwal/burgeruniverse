using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedCollider : MonoBehaviour
{
    public int rewardIndex = 0;

    void Start()
    {
        fillIm.fillAmount = 0f;
    }

    public Image fillIm;

    void Update()
    {
        if (canIncrease)
        {
            fillIm.fillAmount += 0.8f * Time.deltaTime;

            if (fillIm.fillAmount >= 1f)
            {
                fillIm.fillAmount = 0f;
                canIncrease = false;

                //ADD REWARD
                if (rewardIndex == 0)
                    Rewarded.Instance.ShowRewardedAd(rewardIndex, GetComponentInParent<RandomReward>().GetComponentInChildren<MoneyStack>().GetComponent<CapsuleCollider>());
                else if (rewardIndex == 2)
                    Rewarded.Instance.ShowRewardedAd(rewardIndex, null, GetComponentInParent<OutsideFaszomCanvasCapsuleLocker>().GetComponentInChildren<BonusTimerCountDown>().gameObject);
                else if (rewardIndex == 3)
                    Rewarded.Instance.ShowRewardedAd(rewardIndex, null, GetComponentInParent<UnlockableAreaReward>().GetComponentInChildren<LockedCapsule>().gameObject);
                else if (rewardIndex == 4)
                    Rewarded.Instance.ShowRewardedAd(rewardIndex, null, GetComponentInParent<DrinkWaiterReward>().GetComponentInChildren<Waiter_Drinks>().gameObject);
            }
        }
        else
        {
            fillIm.fillAmount -= 0.8f * Time.deltaTime;
        }
    }

    private bool canIncrease = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            if (GetComponentInParent<RandomReward>())
                GetComponentInParent<RandomReward>().DontDestroy();
            canIncrease = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            canIncrease = false;
            if (GetComponentInParent<RandomReward>())
                GetComponentInParent<RandomReward>().InvokeDestroy();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class ButtonHolder : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public string maxLevel;
    public GameObject locked, unlocked;
    public string rewardedIndex = "";
    public bool canMaxAtFirstTry = true;

    private void OnEnable()
    {
        TryToUnlock();
    }


    public void DecideIfCanBuy(bool canBuy)
    {
        int.TryParse(maxLevel, out int fasz);

        if (levelText.text == (fasz + 1).ToString())
        {
            canMaxAtFirstTry = true;
            Maxed();
        }


        if (levelText.text == maxLevel)
            Maxed();
        else
        {
            if (levelText.text != (fasz + 1).ToString())
            {
                if (!canBuy)
                    CanBuy();
                else
                    CanNotBuy();
            }
        }

        if (rewardedIndex == levelText.text)
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            transform.localPosition = new Vector3(-32.5f, transform.localPosition.y, 0f);
        }
        else
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            transform.localPosition = new Vector3(14f, transform.localPosition.y, 0f);
        }

        TryToUnlock();
    }

    public void TryToUnlock()
    {
        if (locked.gameObject.name.Contains("Null") || !transform.parent.gameObject.activeInHierarchy)
            return;

        if (levelText.text != "0")
        {
            locked.SetActive(false);
            unlocked.SetActive(true);
        }
        else
        {
            locked.SetActive(true);
            unlocked.SetActive(false);
        }
    }

    public void CanBuy()
    {

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    public void CanNotBuy()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    public GameObject maxedReward;

    public void Maxed()
    {
        if (!canMaxAtFirstTry) //REWARDED ONE MORE
        {
            maxedReward.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }
        else //MAXED
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);

            if (maxedReward != null)
                maxedReward.SetActive(false);
        }
    }
}

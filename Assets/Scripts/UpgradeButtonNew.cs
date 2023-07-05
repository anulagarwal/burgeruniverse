using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButtonNew : MonoBehaviour
{
    public GameObject upgradesForBuilder;
    private GameObject tempUpgradesHolder;
    private TextMeshProUGUI runText, capText;

    public AnimationCurve price;
    public int maxLevel = 1;
    [Space]
    public TextMeshProUGUI tempIdexText, tempPriceText;
    public Color notEnoughMoneyColor, notEnoughMoneyColor2;
    public Image[] moneyColorImage, moneyColorImage2;

    private Color enoughMoneyColor, enoughMoneyColor2;
    [SerializeField] private int tempLevelIndex = 0;

    [Space]
    public string saveName;
    public AnimationCurve upgradeCurve;
    [Space]
    public bool enableGameObject = false;
    public GameObject objectToEnable;
    [Space]
    public bool increaseBrickCapacity = false;
    public PutDownBag bagsHolderToIncreaseCapacity;
    //public SpawnEmptyChildrenAuto boxToIncreaseCapacity;
    [Space]
    public bool increaseBrickSpeed = false;
    public DrinksFlasksVaultMiddle boxToIncreaseSpeed;

    private AnimationCurve builderCapacityCurve, builderSpeedCurve;

    //private void OnEnable()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(CheckingDatas());
    //}
    //IEnumerator CheckingDatas()
    //{
    //    while (true)
    //    {
    //        UpdateTempPriceTextAndTempLevelIndexText();
    //        CheckIfHasEnoughMoney();
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

    private void Update()
    {
        UpdateTempPriceTextAndTempLevelIndexText();
        CheckIfHasEnoughMoney();
    }

    private void Awake()
    {
        if (upgradesForBuilder)
        {
            //objectToEnable.GetComponent<UpgradesBuilder>().upgradeManager = this;

            //tempUpgradesHolder = Instantiate(upgradesForBuilder, transform);

            //tempUpgradesHolder.SetActive(tempLevelIndex > 0);

            //tempUpgradesHolder.transform.localPosition = Vector3.zero;
            //tempUpgradesHolder.transform.localRotation = Quaternion.identity;
            //tempUpgradesHolder.transform.localScale = Vector3.one;

            //runText = tempUpgradesHolder.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            //capText = tempUpgradesHolder.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        }

        if (saveName.Contains("Builder"))
            maxLevel = 8;

        //builderCapacityCurve = GetComponentInParent<RefreshUpgrades>().buildersCapacity;
        //builderSpeedCurve = GetComponentInParent<RefreshUpgrades>().buildersSpeed;

        tempLevelIndex = PlayerPrefs.GetInt(saveName, 0);

        enoughMoneyColor = moneyColorImage[1].color;
        enoughMoneyColor2 = moneyColorImage2[1].color;

        UpdateTempPriceTextAndTempLevelIndexText();
        CheckIfHasEnoughMoney();
    }

    private void CheckIfHasEnoughMoney()
    {
        if (MoneySpawner.Instance.tempMoney >= GetTempPrice())
        {
            for (int i = 0; i < moneyColorImage.Length; i++)
                moneyColorImage[i].color = enoughMoneyColor;

            for (int i = 0; i < moneyColorImage2.Length; i++)
                moneyColorImage2[i].color = enoughMoneyColor2;
        }
        else
        {
            for (int i = 0; i < moneyColorImage.Length; i++)
                moneyColorImage[i].color = notEnoughMoneyColor;

            for (int i = 0; i < moneyColorImage2.Length; i++)
                moneyColorImage2[i].color = notEnoughMoneyColor2;
        }
    }
    private void UpdateTempPriceTextAndTempLevelIndexText()
    {
        tempPriceText.text = GetTempPrice().ToString();
        tempIdexText.text = tempLevelIndex.ToString();

        if (tempLevelIndex >= maxLevel)
        {
            transform.parent.GetChild(transform.parent.childCount - 1).gameObject.SetActive(true);
            transform.parent.GetChild(transform.parent.childCount - 2).gameObject.SetActive(false);
            transform.parent.GetChild(transform.parent.childCount - 3).gameObject.SetActive(false);

            if (upgradesForBuilder)
            {
                tempUpgradesHolder.transform.parent = transform.parent;
                tempUpgradesHolder.transform.SetSiblingIndex(3);
            }

            gameObject.SetActive(false);
        }

        if (upgradesForBuilder)
        {
            tempUpgradesHolder.SetActive(tempLevelIndex > 0);

            runText.text = ((float)tempLevelIndex / maxLevel * 100).ToString("0") + "%";
            capText.text = GetBuilderCapacity().ToString();
        }
    }
    private void SaveTempUgrades()
    {
        PlayerPrefs.SetInt(saveName, PlayerPrefs.GetInt(saveName, 0) + 1);
        tempLevelIndex = PlayerPrefs.GetInt(saveName, 0);
    }
    private void DoTheUpgrade()
    {
        if (enableGameObject)
        {
            objectToEnable.SetActive(true);
        }
        if (increaseBrickCapacity)
        {
            //boxToIncreaseCapacity.SpawnNewEmptyChildren(Mathf.RoundToInt(upgradeCurve.Evaluate((float)tempLevelIndex / maxLevel)));

            bagsHolderToIncreaseCapacity.maxBoxesCount = Mathf.RoundToInt(upgradeCurve.Evaluate((float)tempLevelIndex / maxLevel));
        }
        if (increaseBrickSpeed)
        {
            boxToIncreaseSpeed.timeBetweenConverts = upgradeCurve.Evaluate((float)tempLevelIndex / maxLevel);
        }
        if (increaseDeliveryGuyCapacity)
        {
            for (int i = 0; i < deliveryGuys.Length; i++)
            {
                deliveryGuys[i].capacity = Mathf.RoundToInt(upgradeCurve.Evaluate((float)tempLevelIndex / maxLevel));
            }
        }

        //if (saveName.Contains("Builder"))
        //{
        //    objectToEnable.GetComponent<UpgradesBuilder>().SetBuilderSpeedAndCapacity();
        //}
    }

    public bool increaseDeliveryGuyCapacity = false;
    public FoodHolderPlayers[] deliveryGuys;

    public void TryToPurchase()
    {
        if (MoneySpawner.Instance.tempMoney >= GetTempPrice())
        {
            MoneySpawner.Instance.UpdateMoney(-GetTempPrice());
            GetComponentInParent<Animation>().Play();
            VibrationManager.Instance.Vibr_SoftImpact();

            SaveTempUgrades();
            UpdateTempPriceTextAndTempLevelIndexText();
            CheckIfHasEnoughMoney();
            DoTheUpgrade();
        }
        else
        {
            Debug.Log("NOT ENOUGH MONEY :(");
        }
    }

    int GetTempPrice()
    {
        return Mathf.RoundToInt(price.Evaluate((float)tempLevelIndex / maxLevel));
    }

    public int GetUpgradeValue()
    {
        tempLevelIndex = PlayerPrefs.GetInt(saveName, 0);
        return Mathf.RoundToInt(upgradeCurve.Evaluate((float)tempLevelIndex / maxLevel));
    }
    public float GetUpgradeValue(bool isFloat)
    {
        tempLevelIndex = PlayerPrefs.GetInt(saveName, 0);
        return upgradeCurve.Evaluate((float)tempLevelIndex / maxLevel);
    }




    public int GetBuilderCapacity()
    {
        return Mathf.RoundToInt(builderCapacityCurve.Evaluate((float)tempLevelIndex / maxLevel));
    }

    public float GetBuilderSpeed()
    {
        return builderSpeedCurve.Evaluate((float)tempLevelIndex / maxLevel);
    }
}

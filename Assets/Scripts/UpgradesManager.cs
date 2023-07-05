using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class UpgradesManager : MonoBehaviour
{
    public AnimationCurve price1, price2, price3;
    public string help1, help2, help3;
    [Space]
    public AnimationCurve priceOfNextUpdate, count, capacity, speed;
    public TextMeshProUGUI[] levelTexts, priceTexts;
    public ButtonHolder[] buttonHoldersNewIm;
    public GameObject[] disabledImages;

    private Animation[] anims;
    private string[] upgradableNames = { "count", "capacity", "speed" };
    private int[] upgradablePrices = { 30, 30, 30 };
    private int canUpgradeThisTimes = 10;

    [HideInInspector]
    public float tempcount, tempcapacity, tempspeed;
    [HideInInspector]

    public static UpgradesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Invoke("UpdateTexts", 0.01f);
    }

    void Start()
    {
        if (gameObject.name.Contains("Security"))
        {
            upgradableNames[0] += "Security";
            upgradableNames[1] += "Security";
            upgradableNames[2] += "Security";
        }
        else if (gameObject.name.Contains("ChipMan"))
        {
            upgradableNames[0] += "ChipMan";
            upgradableNames[1] += "ChipMan";
            upgradableNames[2] += "ChipMan";
        }
        else if (gameObject.name.Contains("PokerTable"))
        {
            upgradableNames[0] += "PokerTable";
            upgradableNames[1] += "PokerTable";
            upgradableNames[2] += "PokerTable";
        }

        anims = GetComponentsInChildren<Animation>();

        tempcount = count.Evaluate(PlayerPrefs.GetInt(upgradableNames[0]) / (float)canUpgradeThisTimes);
        tempcapacity = capacity.Evaluate(PlayerPrefs.GetInt(upgradableNames[1]) / (float)canUpgradeThisTimes);
        tempspeed = speed.Evaluate(PlayerPrefs.GetInt(upgradableNames[2]) / (float)canUpgradeThisTimes);

        Invoke("UpdateTexts", 0.3f);
    }

    private void UpdateTexts(/*bool upgradeAll = true*/)
    {
        for (int i = 0; i < levelTexts.Length; i++)
        {
            if (i == 0)
                priceOfNextUpdate = price1;
            if (i == 1)
                priceOfNextUpdate = price2;
            if (i == 2)
                priceOfNextUpdate = price3;

            upgradablePrices[i] = Mathf.FloorToInt((float)priceOfNextUpdate.Evaluate(PlayerPrefs.GetInt(upgradableNames[i], 0) / (float)canUpgradeThisTimes));
            upgradablePrices[i] = upgradablePrices[i] / 10 * 10;
            if (levelTexts[i].gameObject.activeInHierarchy)
                levelTexts[i].text = (PlayerPrefs.GetInt(upgradableNames[i], 0)).ToString();
            if (priceTexts[i].gameObject.activeInHierarchy)
                priceTexts[i].text = upgradablePrices[i].ToString();


            disabledImages[i].gameObject.SetActive(MoneySpawner.Instance.tempMoney < upgradablePrices[i]);

            if ((buttonHoldersNewIm[i] != null) && (buttonHoldersNewIm[i].gameObject.activeInHierarchy))
                buttonHoldersNewIm[i].DecideIfCanBuy(MoneySpawner.Instance.tempMoney < upgradablePrices[i]);

          

        }
    }

    public int GetTempPriceOfUpdate(int index)
    {
        return upgradablePrices[index];
    }

    public void Upgrade(int index)
    {
        if (gameObject.name.Contains("MC"))
        {
            if (upgradablePrices[index] <= MoneySpawner.Instance.tempMoney)
            {
                MoneySpawner.Instance.UpdateMoney(-upgradablePrices[index]);
                PlayerPrefs.SetInt(upgradableNames[index], PlayerPrefs.GetInt(upgradableNames[index], 0) + 1);
                anims[index + 1].Play("UpgradedAnim");

                switch (index)
                {
                    case 0:
                        tempcount = count.Evaluate(PlayerPrefs.GetInt(upgradableNames[0]) / (float)canUpgradeThisTimes);


                        if (gameObject.name.Contains("Security"))
                        {
                            tempcount = Mathf.RoundToInt(tempcount);
                            PlayerPrefs.SetInt("SecurityCount", Mathf.RoundToInt(tempcount));
                            FindObjectOfType<PlayerMcDonalds>().GetComponentInChildren<FoodHolderPlayers>().capacity = Mathf.RoundToInt(tempcount);
                            Debug.Log("Tempcount:" + tempcount + "_Capacity:" + FindObjectOfType<PlayerMcDonalds>().GetComponentInChildren<FoodHolderPlayers>().capacity);
                        }
                        else if (gameObject.name.Contains("ChipMan"))
                        {
                            PlayerPrefs.SetInt("ChipManCount", Mathf.RoundToInt(tempcount));
                            enableCharacter = GameObject.FindGameObjectWithTag("ChipMans").transform.GetChild(Mathf.RoundToInt(tempcount - 1)).gameObject;
                            Invoke("EnableGameObjectLater", 0.3f);
                        }
                        else if (gameObject.name.Contains("PokerTable"))
                        {
                            PlayerPrefs.SetInt("ChefCount", PlayerPrefs.GetInt("ChefCount") + 1);
                            enableCharacter = GameObject.FindGameObjectWithTag("Chefs").transform.GetChild(Mathf.RoundToInt(tempcount - 1)).gameObject;
                            Invoke("EnableGameObjectLater", 0.3f);
                        }
                        break;

                    case 1:
                        tempcapacity = capacity.Evaluate(PlayerPrefs.GetInt(upgradableNames[1]) / (float)canUpgradeThisTimes);

                        if (gameObject.name.Contains("Player"))
                        {
                            PlayerPrefs.SetInt("PlayerCapacity", PlayerPrefs.GetInt("PlayerCapacity", 3) + 1);
                            PlayerPrefs.SetInt("PlayerBurgers", PlayerPrefs.GetInt("PlayerBurgers", 3) + 1);
                        }
                        else if (gameObject.name.Contains("Security"))
                        {
                            PlayerPrefs.SetInt("SecurityCapacity", Mathf.RoundToInt(tempcapacity));
                            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Security"))
                                obj.transform.GetChild(Mathf.RoundToInt(tempcount - 1)).gameObject.SetActive(true);
                        }
                        else if (gameObject.name.Contains("Chef"))
                        {
                            //CHEF CAPACITY----------------------------------------
                            tempcapacity = Mathf.RoundToInt(tempcapacity);
                            PlayerPrefs.SetInt("ChefCapacity", PlayerPrefs.GetInt("ChefCapacity", 1) + 1);


                            Debug.Log(PlayerPrefs.GetInt("ChefCapacity"));


                            foreach (ChipMan chipm in FindObjectsOfType<ChipMan>())
                            {
                                if (chipm.gameObject.name.Contains("CHEF"))
                                    chipm.GetComponentInChildren<FoodHolderPlayers>().capacity = PlayerPrefs.GetInt("ChefCapacity", 1);

                            }
                        }
                        else if (gameObject.name.Contains("ChipMan"))
                        {
                            //WAITER NEW FOOD CAPACITY-------------------
                            PlayerPrefs.SetInt("ChipManCapacity", PlayerPrefs.GetInt("ChipManCapacity", 1) + 1);

                            foreach (Waiter_Mc chipm in FindObjectsOfType<Waiter_Mc>())
                            {
                                chipm.GetComponentInChildren<FoodHolderPlayers>().capacity = PlayerPrefs.GetInt("ChipManCapacity", 1);
                            }
                        }
                        else if (gameObject.name.Contains("Player"))
                        {
                            //PlayerPrefs.SetInt("PlayerCapacity", Mathf.RoundToInt(tempcapacity));
                            PlayerPrefs.SetInt("PlayerCapacity", PlayerPrefs.GetInt("PlayerCapacity", 3) + 1);
                            PlayerPrefs.SetInt("PlayerBurgers", PlayerPrefs.GetInt("PlayerBurgers", 3) + 1);
                        }
                        else if (gameObject.name.Contains("PokerTable"))
                        {
                            foreach (PokerTable table in FindObjectsOfType<PokerTable>())
                            {
                                table.transform.GetChild(Mathf.RoundToInt(tempcount)).gameObject.SetActive(true);
                            }
                        }
                        break;

                    case 2:
                        tempspeed = speed.Evaluate(PlayerPrefs.GetInt(upgradableNames[2]) / (float)canUpgradeThisTimes);

                        if (gameObject.name.Contains("Security"))
                        {
                            PlayerPrefs.SetInt("SecuritySpeed", PlayerPrefs.GetInt("SecuritySpeed", 0) - 1);
                            FindObjectOfType<EnableWaitresses>().transform.GetChild(Mathf.RoundToInt(tempspeed) - 1).gameObject.SetActive(true);
                        }

                        else if (gameObject.name.Contains("Chef"))
                        {
                            PlayerPrefs.SetFloat("ChefSpeed", tempspeed);
                            foreach (ChipMan chipm in FindObjectsOfType<ChipMan>())
                            {
                                if (chipm.gameObject.name.Contains("CHEF"))
                                    chipm.GetComponentInChildren<NavMeshAgent>().speed = tempspeed;
                            }
                        }
                        else if (gameObject.name.Contains("ChipMan"))
                        {
                            //WAITER_NEW SPEED----------------------------------------------
                            PlayerPrefs.SetFloat("ChipManSpeed", tempspeed);
                            foreach (Waiter_Mc chipm in FindObjectsOfType<Waiter_Mc>())
                            {
                                chipm.GetComponentInChildren<NavMeshAgent>().speed = tempspeed;
                            }

                        }
                        break;
                }

                string eventNameToDisplay = "";
                int eventCountToPass = 0;
                switch (upgradableNames[index])
                {
                    case "speedSecurity":
                        eventNameToDisplay = "upgradedCashierCount";
                        PlayerPrefs.SetInt("upgradedCashierCount", PlayerPrefs.GetInt("upgradedCashierCount", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedCashierCount", 0);
                        break;

                    case "countPokerTable":
                        eventNameToDisplay = "upgradedChefCount";
                        PlayerPrefs.SetInt("upgradedChefCount", PlayerPrefs.GetInt("upgradedChefCount", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedChefCount", 0);
                        break;

                    case "countChipMan":
                        eventNameToDisplay = "upgradedWaiterCount";
                        PlayerPrefs.SetInt("upgradedWaiterCount", PlayerPrefs.GetInt("upgradedWaiterCount", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedWaiterCount", 0);
                        break;

                    //-----

                    case "countSecurity":
                        eventNameToDisplay = "upgradedPlayerCapacity";
                        PlayerPrefs.SetInt("upgradedPlayerCapacity", PlayerPrefs.GetInt("upgradedPlayerCapacity", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedPlayerCapacity", 0);
                        break;

                    case "capacityPokerTable":
                        eventNameToDisplay = "upgradedChefCapacity";
                        PlayerPrefs.SetInt("upgradedChefCapacity", PlayerPrefs.GetInt("upgradedChefCapacity", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedChefCapacity", 0);
                        break;

                    case "capacityChipMan":
                        eventNameToDisplay = "upgradedWaiterCapacity";
                        PlayerPrefs.SetInt("upgradedWaiterCapacity", PlayerPrefs.GetInt("upgradedWaiterCapacity", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedWaiterCapacity", 0);
                        break;

                    case "speedChipMan":
                        eventNameToDisplay = "upgradedWaiterSpeed";
                        PlayerPrefs.SetInt("upgradedWaiterSpeed", PlayerPrefs.GetInt("upgradedWaiterSpeed", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedWaiterSpeed", 0);
                        break;
                }
                Debug.Log("SUCCESSFULLY UPGRADED " + eventNameToDisplay + "_" + eventCountToPass.ToString());

                levelTexts[index].transform.parent.parent.GetComponent<Animation>().Play();

                levelTexts[index].GetComponentInParent<Animation>().playAutomatically = true;

                GetComponentInParent<RefreshUpgrades>().Refresh();

                Invoke("UpdateTexts", 0.01f);
            }
            else
            {
                anims[index + 1].Play("FailedAnim");
                Debug.Log("NOT ENOUGH MONEY");
            }
        }
        else
        {
            if (upgradablePrices[index] <= MoneySpawner.Instance.tempMoney)
            {
                MoneySpawner.Instance.UpdateMoney(-upgradablePrices[index]);
                PlayerPrefs.SetInt(upgradableNames[index], PlayerPrefs.GetInt(upgradableNames[index], 0) + 1);
                anims[index + 1].Play("UpgradedAnim");

                switch (index)
                {
                    case 0:
                        tempcount = count.Evaluate(PlayerPrefs.GetInt(upgradableNames[0]) / (float)canUpgradeThisTimes);


                        if (gameObject.name.Contains("Security"))
                        {
                            PlayerPrefs.SetInt("SecurityCount", Mathf.RoundToInt(tempcount));
                            enableCharacter = GameObject.FindGameObjectWithTag("Securities").transform.GetChild(Mathf.RoundToInt(tempcount - 1)).gameObject;
                            Invoke("EnableGameObjectLater", 0.3f);
                        }
                        else if (gameObject.name.Contains("ChipMan"))
                        {
                            PlayerPrefs.SetInt("ChipManCount", Mathf.RoundToInt(tempcount));
                            enableCharacter = GameObject.FindGameObjectWithTag("ChipMans").transform.GetChild(Mathf.RoundToInt(tempcount - 1)).gameObject;
                            Invoke("EnableGameObjectLater", 0.3f);
                        }
                        else if (gameObject.name.Contains("PokerTable"))
                        {
                            PlayerPrefs.SetInt("PokerSeat", Mathf.RoundToInt(tempcount));
                            foreach (PokerTable table in FindObjectsOfType<PokerTable>())
                            {
                                table.transform.GetChild(Mathf.RoundToInt(tempcount)).gameObject.SetActive(true);
                            }
                        }
                        break;

                    case 1:
                        tempcapacity = capacity.Evaluate(PlayerPrefs.GetInt(upgradableNames[1]) / (float)canUpgradeThisTimes);

                        if (gameObject.name.Contains("Security"))
                        {
                            PlayerPrefs.SetInt("SecurityCapacity", Mathf.RoundToInt(tempcapacity));
                            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Security"))
                                obj.transform.GetChild(Mathf.RoundToInt(tempcount - 1)).gameObject.SetActive(true);
                        }
                        else if (gameObject.name.Contains("ChipMan"))
                        {
                            PlayerPrefs.SetInt("ChipManCapacity", Mathf.RoundToInt(tempcapacity));
                            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ChipMan"))
                                obj.GetComponentInChildren<FoodHolderPlayers>().capacity = Mathf.RoundToInt(tempcapacity);
                        }
                        else if (gameObject.name.Contains("Player"))
                        {
                            PlayerPrefs.SetInt("PlayerCapacity", Mathf.RoundToInt(tempcapacity));
                            FindObjectOfType<PlayerCasino>().GetComponentInChildren<FoodHolderPlayers>().capacity = Mathf.RoundToInt(tempcapacity);
                            PlayerPrefs.SetInt("PlayerCapacity", PlayerPrefs.GetInt("PlayerCapacity", 2) + 1);
                            PlayerPrefs.SetInt("PlayerBurgers", PlayerPrefs.GetInt("PlayerBurgers", 3) + 1);
                        }
                        else if (gameObject.name.Contains("PokerTable"))
                        {
                            foreach (PokerTable table in FindObjectsOfType<PokerTable>())
                            {
                                table.transform.GetChild(Mathf.RoundToInt(tempcount)).gameObject.SetActive(true);
                            }
                        }
                        break;

                    case 2:
                        tempspeed = speed.Evaluate(PlayerPrefs.GetInt(upgradableNames[2]) / (float)canUpgradeThisTimes);


                        if (gameObject.name.Contains("Security"))
                        {
                            PlayerPrefs.SetFloat("SecuritySpeed", tempspeed);
                            foreach (Security secu in FindObjectsOfType<Security>())
                                secu.GetComponentInChildren<NavMeshAgent>().speed = tempspeed;
                        }
                        else if (gameObject.name.Contains("ChipMan"))
                        {
                            PlayerPrefs.SetFloat("ChipManSpeed", tempspeed);
                            foreach (ChipMan chipm in FindObjectsOfType<ChipMan>())
                                chipm.GetComponentInChildren<NavMeshAgent>().speed = tempspeed;
                        }
                        break;
                }

                string eventNameToDisplay = "";
                int eventCountToPass = 0;
                switch (upgradableNames[index])
                {
                    case "speedSecurity":
                        eventNameToDisplay = "upgradedCashierCount";
                        PlayerPrefs.SetInt("upgradedCashierCount", PlayerPrefs.GetInt("upgradedCashierCount", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedCashierCount", 0);
                        break;

                    case "countPokerTable":
                        eventNameToDisplay = "upgradedChefCount";
                        PlayerPrefs.SetInt("upgradedChefCount", PlayerPrefs.GetInt("upgradedChefCount", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedChefCount", 0);
                        break;

                    case "countChipMan":
                        eventNameToDisplay = "upgradedWaiterCount";
                        PlayerPrefs.SetInt("upgradedWaiterCount", PlayerPrefs.GetInt("upgradedWaiterCount", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedWaiterCount", 0);
                        break;

                    //-----

                    case "countSecurity":
                        eventNameToDisplay = "upgradedPlayerCapacity";
                        PlayerPrefs.SetInt("upgradedPlayerCapacity", PlayerPrefs.GetInt("upgradedPlayerCapacity", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedPlayerCapacity", 0);
                        break;

                    case "capacityPokerTable":
                        eventNameToDisplay = "upgradedChefCapacity";
                        PlayerPrefs.SetInt("upgradedChefCapacity", PlayerPrefs.GetInt("upgradedChefCapacity", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedChefCapacity", 0);
                        break;

                    case "capacityChipMan":
                        eventNameToDisplay = "upgradedWaiterCapacity";
                        PlayerPrefs.SetInt("upgradedWaiterCapacity", PlayerPrefs.GetInt("upgradedWaiterCapacity", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedWaiterCapacity", 0);
                        break;

                    case "speedChipMan":
                        eventNameToDisplay = "upgradedWaiterSpeed";
                        PlayerPrefs.SetInt("upgradedWaiterSpeed", PlayerPrefs.GetInt("upgradedWaiterSpeed", 0) + 1);
                        eventCountToPass = PlayerPrefs.GetInt("upgradedWaiterSpeed", 0);
                        break;
                }
                Debug.Log("SUCCESSFULLY UPGRADED " + eventNameToDisplay + "_" + eventCountToPass.ToString());

                Invoke("UpdateTexts", 0.01f);
            }
            else
            {
                anims[index + 1].Play("FailedAnim");
                Debug.Log("NOT ENOUGH MONEY");
            }
        }
    }

    private GameObject enableCharacter;
    private void EnableGameObjectLater()
    {
        enableCharacter.SetActive(true);
    }
}

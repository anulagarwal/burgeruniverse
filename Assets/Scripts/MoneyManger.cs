using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManger : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public static MoneyManger Instance;

    //[HideInInspector]
    //public float passiveIncreaseTime;

    //[HideInInspector]
    //public int tapIncreaseValue;

    //public int passiveIncreaseValue;

    public int tempMoneyCount;

    //public AnimationCurve passiveMoneyPerTime, tapMoneyValue;

    private Animation moneyTextAnim;
    private Camera mainCam;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //moneyPopUps = GameObject.FindGameObjectWithTag("MoneyPopUp").transform;
        //mainCam = Camera.main;
        moneyTextAnim = moneyText.GetComponent<Animation>();

        tempMoneyCount = PlayerPrefs.GetInt("Money", 0);
        //passiveIncreaseTime = passiveMoneyPerTime.Evaluate(PlayerPrefs.GetInt("Tap", 0) / 20f);
        //tapIncreaseValue = Mathf.FloorToInt(tapMoneyValue.Evaluate(PlayerPrefs.GetInt("Tap", 0) / 20f));

        UpdateText();

        //for (int i = 0; i < 1000; i++)
        //{
        //IncreaseMoneyBy(-1);

        //}
        //StartCoroutine(PassiveIncrease());

        //IncreaseMoneyBy(1000);
    }

    private void UpdateText()
    {
        moneyText.text = tempMoneyCount.ToString();
        moneyTextAnim.Play();
    }

    //public void UpgradeTapMoney()
    //{
    //    //PlayerPrefs.SetInt("Tap", PlayerPrefs.GetInt("Tap", 0) + 1);
    //    tapIncreaseValue = Mathf.FloorToInt(tapMoneyValue.Evaluate(PlayerPrefs.GetInt("Tap", 0) / 20f));
    //}

    //public void UpgradePassiveMoney()
    //{
    //    //PlayerPrefs.SetInt("Bolt", PlayerPrefs.GetInt("Bolt", 0) + 1);
    //    passiveIncreaseTime = passiveMoneyPerTime.Evaluate(PlayerPrefs.GetInt("Tap", 0) / 20f);
    //}

    //IEnumerator PassiveIncrease()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(passiveIncreaseTime);
    //        IncreaseMoneyBy(passiveIncreaseValue);
    //    }
    //}

    //private Transform moneyPopUps;

    public void IncreaseMoneyBy(int value)
    {
        //Instantiate(Resources.Load("Money") as GameObject, new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0f), Quaternion.identity);

        //int randomIndex = UnityEngine.Random.Range(0, moneyPopUps.childCount);
        //moneyPopUps.GetChild(randomIndex).GetComponent<Animation>().Play();
        //moneyPopUps.GetChild(randomIndex).transform.position = Input.mousePosition + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f));

        //if (value == -1)
        //    value = tapIncreaseValue;

        tempMoneyCount += value;
        PlayerPrefs.SetInt("Money", tempMoneyCount);
        UpdateText();
    }
}

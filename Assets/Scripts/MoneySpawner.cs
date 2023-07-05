using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lofelt.NiceVibrations;

public class MoneySpawner : MonoBehaviour
{
    public static MoneySpawner Instance;

    private Camera cam;
    private Animation anim;


    private void Awake()
    {
        Instance = this;
    }

    private int tempIndex = 0;

    public void SpawnMoneyAt(Vector3 pos, int amount)
    {
        transform.GetChild(tempIndex).GetComponent<MoneyIcon>().countOfMoney = amount;
        transform.GetChild(tempIndex).transform.position = cam.WorldToScreenPoint(pos);
        transform.GetChild(tempIndex).gameObject.SetActive(true);

        tempIndex++;

        if (tempIndex >= transform.childCount)
            tempIndex = 0;
    }

    private TextMeshProUGUI moneyCounter;
    [HideInInspector] public int tempMoney = 0;

    void Start()
    {
        if (gameObject.name.Contains("MC"))
        {

            tempMoney = PlayerPrefs.GetInt("Money", 10);

            //PlayerPrefs.DeleteAll();
            ////tempMoney = 19950;
        }
        else
            tempMoney = PlayerPrefs.GetInt("Money", 0);

        anim = GetComponent<Animation>();
        moneyCounter = transform.GetComponentInParent<TextMeshProUGUI>();
        UpdateMoney();
        cam = Camera.main;
    }


    public void UpdateMoney(int value = 0)
    {
        //if (value > 0)
        //    Debug.Log("Semmi");
        ////HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        if (value < 0 && value > -5)
            VibrationManager.Instance.Vibr_LightImpact();
        else if (value <= -5)
            VibrationManager.Instance.Vibr_SoftImpact();
        //HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);

        if (value > 0)
            anim.Play();

        tempMoney += value;


        if (tempMoney < 0)
            tempMoney = 0;
        moneyCounter.text = tempMoney.ToString();
        PlayerPrefs.SetInt("Money", tempMoney);

        if ((PlayerPrefs.GetInt("Reached70", 0) == 0) && (tempMoney >= 70) && Time.timeSinceLevelLoad > 8f)
        {
            PlayerPrefs.SetInt("Reached70", 1);

            if (ArrowTutorialHolder.Instance != null)
                ArrowTutorialHolder.Instance.EnableNextTut(5);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

            UpdateMoney((1000 - 10));
        }

    }
}

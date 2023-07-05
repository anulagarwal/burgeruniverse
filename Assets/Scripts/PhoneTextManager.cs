using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTextManager : MonoBehaviour
{
    private int tempIndex = 0;

    public GameObject[] enemyTexts, enemyTexts2, playerTexts, playerTexts2, choices;

    public void Chose(int index)
    {
        choices[0].GetComponentInParent<Animation>().Play("ChoicesDown");

        if (index == 0)
        {
            playerTexts[tempIndex].SetActive(true);
            Invoke("EnemyTexts", 1f);
        }
        else
        {
            playerTexts2[tempIndex].SetActive(true);
            Invoke("EnemyTexts2", 1f);
        }
    }

    private void EnemyTexts()
    {
        textToEnable = enemyTexts[tempIndex].transform.GetChild(0).GetChild(1).gameObject;
        Invoke("EnableRealText", 1f);
        enemyTexts[tempIndex].SetActive(true);
        tempIndex++;

        if (tempIndex < choices.Length)
            Invoke("ChoicesShow", 0.6f);
    }

    private void EnemyTexts2()
    {
        textToEnable = enemyTexts2[tempIndex].transform.GetChild(0).GetChild(1).gameObject;
        Invoke("EnableRealText", 1f);
        enemyTexts2[tempIndex].SetActive(true);
        tempIndex++;

        //Invoke("ChoicesShow", 0.6f);
    }

    GameObject textToEnable;

    private void EnableRealText()
    {
        if (tempIndex == playerTexts.Length)
            transform.Find("Conversations").GetComponent<Animation>().Play();

        textToEnable.SetActive(true);
    }

    private void ChoicesShow()
    {
        choices[0].GetComponentInParent<Animation>().Play();
        choices[tempIndex].SetActive(true);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

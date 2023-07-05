using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoneyPos : MonoBehaviour
{
    public int spawnMoneyPosIndex;

    public AnimationCurve spawnTimeCurve;

    public bool canSpawn = true;

    public bool isBarSpawner = false;

    Transform moneyStackTransform;

    private void CountMoney()
    {
        PlayerPrefs.SetInt("MoneyAt" + spawnMoneyPosIndex, moneyStackTransform.GetComponentsInChildren<Money>().Length);
        Invoke("CountMoney", 1f);
    }

    void Start()
    {
        moneyStackTransform = transform.parent.Find("MoneyStack");
        if (spawnMoneyPosIndex != 0)
            Invoke("SpawnMoney", 1f);

        if (!isBarSpawner)
            Invoke("CallGuests", 1f);

        if ((PlayerPrefs.GetInt("MoneyAt" + spawnMoneyPosIndex, 0) != 0) && (spawnMoneyPosIndex != 0))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("MoneyAt" + spawnMoneyPosIndex, 0); i++)
            {
                int randomIndex = Random.Range(0, 8);

                GameObject tempMoney = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
                tempMoney.transform.Rotate(Vector3.right, -90f);
                tempMoney.GetComponent<Money>().MoveToTarget(moneyStackTransform.GetChild(randomIndex).position + (Vector3.up * 0.185f * moneyStackTransform.GetChild(randomIndex).childCount));
                tempMoney.transform.parent = moneyStackTransform.GetChild(randomIndex);
            }
        }

        if (spawnMoneyPosIndex != 0)
            Invoke("CountMoney", 1f);
    }

    private void CallGuests()
    {
        foreach (Guest guest in FindObjectsOfType<Guest>())
        {
            if (!guest.gameObject.name.Contains("MC"))
                guest.FindSeat();
        }
    }

    public GameObject moneyPrefab;
    public int playersAtTable = 0;

    public void SpawnMoney()
    {
        if (!canSpawn)
            return;

        CancelInvoke("SpawnMoney");

        if (playersAtTable <= 0)
        {
            Invoke("SpawnMoney", 1f);
        }
        else
        {
            PlayerPrefs.SetInt("MoneyAt" + spawnMoneyPosIndex, moneyStackTransform.GetComponentsInChildren<Money>().Length);

            int randomIndex = 0;
            if (moneyStackTransform.GetComponentsInChildren<Money>().Length > 35)
                randomIndex = Random.Range(0, 11);
            else if (moneyStackTransform.GetComponentsInChildren<Money>().Length > 27)
                randomIndex = Random.Range(0, 10);
            else if (moneyStackTransform.GetComponentsInChildren<Money>().Length > 15)
                randomIndex = Random.Range(0, 8);
            else
                randomIndex = Random.Range(0, 6);

            Debug.Log(randomIndex);

            GameObject tempMoney = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
            tempMoney.transform.Rotate(Vector3.right, -90f);
            tempMoney.GetComponent<Money>().MoveToTarget(moneyStackTransform.GetChild(randomIndex).position + (Vector3.up * 0.185f * moneyStackTransform.GetChild(randomIndex).childCount));
            tempMoney.transform.parent = moneyStackTransform.GetChild(randomIndex);

            Invoke("SpawnMoney", spawnTimeCurve.Evaluate(playersAtTable));
        }
    }

    public void SpawnMoneyOnce(float delayInv = 0f)
    {
        Invoke("SpawnMoneyOnceReal", delayInv);
    }

    private void SpawnMoneyOnceReal()
    {
        PlayerPrefs.SetInt("MoneyAt" + spawnMoneyPosIndex, moneyStackTransform.GetComponentsInChildren<Money>().Length);

        int randomIndex = 0;
        if (moneyStackTransform.GetComponentsInChildren<Money>().Length > 35)
            randomIndex = Random.Range(0, 6);
        else if (moneyStackTransform.GetComponentsInChildren<Money>().Length > 27)
            randomIndex = Random.Range(0, 5);
        else if (moneyStackTransform.GetComponentsInChildren<Money>().Length > 15)
            randomIndex = Random.Range(0, 4);
        else
            randomIndex = Random.Range(0, 3);


        GameObject tempMoney = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        tempMoney.transform.Rotate(Vector3.right, -90f);
        tempMoney.GetComponent<Money>().MoveToTarget(moneyStackTransform.GetChild(randomIndex).position + (Vector3.up * 0.185f * moneyStackTransform.GetChild(randomIndex).childCount));
        tempMoney.transform.parent = moneyStackTransform.GetChild(randomIndex);
    }
}

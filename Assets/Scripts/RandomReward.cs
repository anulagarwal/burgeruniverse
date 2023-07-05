using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomReward : MonoBehaviour
{
    public Transform[] positions;

    public GameObject[] objectsToDisableAtEnd;

    public AnimationCurve moneySpawnCurve;

    private int moneySpawnCount;

    IEnumerator Start()
    {
        GetComponentInChildren<MoneyStack>(true).GetComponent<CapsuleCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        OfferReward();
    }

    public void OfferReward()
    {
        ChoosePosition();

        GetComponentInChildren<MoneyStack>(true).GetComponent<CapsuleCollider>().enabled = false;

        for (int i = 0; i < objectsToDisableAtEnd.Length; i++)
            objectsToDisableAtEnd[i].SetActive(true);

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);


        if (PlayerPrefs.GetInt("Eat", 0) < 300)
            moneySpawnCount = Mathf.RoundToInt(moneySpawnCurve.Evaluate(PlayerPrefs.GetInt("Eat", 0)));
        else
            moneySpawnCount = 82;

        moneySpawnCount += Random.Range(-(moneySpawnCount / 10), moneySpawnCount / 10);

        GetComponentInChildren<TextMeshPro>().text = (moneySpawnCount * 10).ToString();

        for (int i = 0; i < moneySpawnCount; i++)
        {
            GetComponentInChildren<SpawnMoneyPos>().SpawnMoneyOnce(i * 0.05f);
        }

        transform.GetChild(2).GetComponent<ParticleSystem>().Play();

        Invoke("StopReward", 20f);
    }

    public void DontDestroy()
    {
        if (objectsToDisableAtEnd[0].activeInHierarchy)
            CancelInvoke("StopReward");
    }

    public void InvokeDestroy()
    {
        Invoke("StopReward", 10f);
    }

    public void GiveReward_DisableChildren()
    {
        for (int i = 0; i < objectsToDisableAtEnd.Length; i++)
            objectsToDisableAtEnd[i].SetActive(false);

        Invoke("StopReward", 2f);
    }

    public void StopReward()
    {

        foreach (Money money in GetComponentsInChildren<Money>())
        {
            Destroy(money.gameObject);
        }


        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        transform.GetChild(2).GetComponent<ParticleSystem>().Play();

        Invoke("OfferReward", Random.Range(25, 55));
    }

    private void ChoosePosition()
    {
        int choosenPosIndex = 0;
        do
        {
            choosenPosIndex = Random.Range(0, positions.Length);
        } while (!positions[choosenPosIndex].gameObject.activeInHierarchy);

        transform.position = positions[choosenPosIndex].position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStack : MonoBehaviour
{
    private Collider coll;

    void Start()
    {
        coll = GetComponent<Collider>();

        if (coll.enabled)
            KeepResetingCollider();
    }

    public void KeepResetingCollider()
    {
        if (coll.enabled)
        {
            coll.enabled = false;
            Invoke("KeepResetingCollider", 0.05f);
        }
        else
        {
            coll.enabled = true;
            Invoke("KeepResetingCollider", 0.45f);
        }
    }

    void StartMoneyDestroyer()
    {
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            StartMoneyDestroyer();
        }

        if (other.gameObject.name.Contains("DELIVERY"))
        {
            int countOfMoneyToSpawn = other.GetComponent<ChipMan>().moneyToSpawn;

            if (countOfMoneyToSpawn == 0)
                return;

            other.GetComponent<ChipMan>().moneyToSpawn = 0;
            for (int i = 0; i < countOfMoneyToSpawn; i++)
            {
                transform.parent.GetComponentInChildren<SpawnMoneyPos>().SpawnMoneyOnce();
            }

            other.GetComponent<ChipMan>().GetChips();
        }
    }
}

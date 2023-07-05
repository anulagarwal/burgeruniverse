using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour
{
    public GameObject aiGuest, cheater;

    public bool canSpawnCheater = false;

    public WaitingLine[] waitingLines;

    int linePosCount;


    void Start()
    {
        linePosCount = (waitingLines[0].transform.childCount - 1) + (waitingLines[1].transform.childCount - 1);

        if (gameObject.name.Contains("MC"))
        {
            StartCoroutine(TryToSpawnAI());

            return;
        }

        for (int i = 0; i < 3; i++)
        {
            if (canSpawnCheater)
            {
                if (Random.Range(0, 4) == 0)
                    Instantiate(cheater, transform.GetChild(Random.Range(0, transform.childCount)).position, Quaternion.identity);
                else
                    Instantiate(aiGuest, transform.GetChild(Random.Range(0, transform.childCount)).position, Quaternion.identity);
            }
            else
                Instantiate(aiGuest, transform.GetChild(Random.Range(0, transform.childCount)).position, Quaternion.identity);
        }

        StartCoroutine(TryToSpawnAI());
    }

    public int maxGuestCount = 24;

    public void SpawnFirstVIP()
    {

    }

    public float timeBetweenSpawns = 1.5f;

    IEnumerator TryToSpawnAI()
    {
        yield return new WaitForSeconds(0.15f);
        while (true)
        {
            if ((FindObjectsOfType<Guest>().Length < GameObject.FindGameObjectsWithTag("Seat").Length) /*&& (FindObjectsOfType<Guest>().Length < maxGuestCount)*/)
            {

                if ((waitingLines[1].GetEmptyTransform() != null) | (waitingLines[0].GetEmptyTransform() != null))
                {
                    if (canSpawnCheater)
                    {
                        if (Random.Range(0, 6) == 0)
                            Instantiate(cheater, transform.GetChild(Random.Range(0, transform.childCount)).position, Quaternion.identity);
                        else
                            Instantiate(aiGuest, transform.GetChild(Random.Range(0, transform.childCount)).position, Quaternion.identity);
                    }
                    else
                        Instantiate(aiGuest, transform.GetChild(Random.Range(0, transform.childCount)).position, Quaternion.identity);

                }
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}

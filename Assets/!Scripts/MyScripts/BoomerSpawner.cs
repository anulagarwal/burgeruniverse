using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerSpawner : MonoBehaviour
{
    public float width = 3f;

    public GameObject[] boomers;

    void Start()
    {
        //StartCoroutine(SpawnBoomers());
    }

    IEnumerator SpawnBoomers()
    {
        while (true)
        {
            SpawnBoomer();
            yield return new WaitForSeconds(Random.Range(0.4f, 1f));
        }
    }

    public void FirstSpawn()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject tempBoomer = Instantiate(boomers[Random.Range(0, boomers.Length)], new Vector3(Random.Range(-width, width), transform.position.y, transform.position.z + Random.Range(0f, 12f)), Quaternion.identity);
            tempBoomer.AddComponent<Boomer>();
        }

        StartCoroutine(SpawnBoomers());
    }

    public void SpawnBoomer()
    {
        GameObject tempBoomer = Instantiate(boomers[Random.Range(0, boomers.Length)], new Vector3(Random.Range(-width, width), transform.position.y, transform.position.z), Quaternion.identity);
        //tempBoomer.AddComponent<Animator>();
        tempBoomer.AddComponent<Boomer>();
    }
}

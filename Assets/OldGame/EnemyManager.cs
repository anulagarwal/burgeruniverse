using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;

    public static EnemyManager Instance;

    private GameObject tempEnemy;
    private bool isInWater = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Animal")[0].transform.position.x < -1f)
            tempEnemy = GameObject.FindGameObjectsWithTag("Animal")[0];
        else
            tempEnemy = GameObject.FindGameObjectsWithTag("Animal")[1];
    }

    public void SpawnNewEnemy(bool isInsideWater)
    {
        isInWater = isInsideWater;

        if (!IsInvoking("Spawn"))
            Invoke("Spawn", Random.Range(0.9f, 2.2f));
    }

    private void Spawn()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, 3);
            Vector3 tempEnemyPosition = tempEnemy.transform.position;

            if (!enemies[randomIndex].activeInHierarchy)
            {
                tempEnemy.SetActive(false);
                tempEnemy = enemies[randomIndex];
                tempEnemy.SetActive(true);
                tempEnemy.transform.position = new Vector3(tempEnemyPosition.x, 0f, tempEnemyPosition.z);
                isInWater = false;
                return;
            }
        }
    }
}

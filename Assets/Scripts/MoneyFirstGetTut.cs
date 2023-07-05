using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFirstGetTut : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;

        if (PlayerPrefs.GetInt("Tut", 0) == 0)
        {
            for (int i = 0; i < enableBuyable.Length; i++)
            {
                enableBuyable[i].SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }

    public GameObject[] enableBuyable;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            if (FindObjectOfType<MoneySpawner>().tempMoney >= 20)
            {
                PlayerPrefs.SetInt("Tut", 1);

                ArrowTutorialHolder.Instance.EnableNextTut();
                
                for (int i = 0; i < enableBuyable.Length; i++)
                {
                    enableBuyable[i].SetActive(true);
                }



                Destroy(gameObject);
            }
            else
            {
                GetComponent<Collider>().enabled = false;

                Invoke("ReEnableColl", 0.1f);
            }
        }

    }

    private void ReEnableColl()
    {
        GetComponent<Collider>().enabled = true;
    }
}

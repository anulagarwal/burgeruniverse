using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksHolder : MonoBehaviour
{
    public GameObject locked;
    public GameObject[] objectsToEnable;

    public void SetDrinksTrue()
    {
        Debug.Log("DRINKS_ON");

        PlayerPrefs.SetInt("Drinks", 1);

        locked.SetActive(false);

        ////TRUCK
        //objectsToEnable[0].transform.parent = null;

        for (int i = 0; i < objectsToEnable.Length; i++)
            objectsToEnable[i].SetActive(true);
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("Drinks", 0) == 1)
        {
            locked.SetActive(false);

            ////TRUCK
            //objectsToEnable[0].transform.parent = null;

            for (int i = 0; i < objectsToEnable.Length; i++)
                objectsToEnable[i].SetActive(true);
        }
        else
        {
            locked.SetActive(true);

            for (int i = 0; i < objectsToEnable.Length; i++)
                objectsToEnable[i].SetActive(false);
        }

    }
}

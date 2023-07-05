using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriesHolder : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.SetInt("Fries", 1);



        if (PlayerPrefs.GetInt("Fries", 0) == 1)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}

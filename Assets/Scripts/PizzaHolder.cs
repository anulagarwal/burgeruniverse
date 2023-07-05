using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaHolder : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.SetInt("Pizza", 1);



        if (PlayerPrefs.GetInt("Pizza", 0) == 1)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}

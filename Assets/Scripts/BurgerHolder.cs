using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerHolder : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("Burger", 0) == 1)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}

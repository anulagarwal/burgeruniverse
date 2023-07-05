using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamHolder : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("IceCream", 0) == 1)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}

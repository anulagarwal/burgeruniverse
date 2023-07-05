using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesOutdoorHolder : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.SetInt("Pizza", 1);



        if (PlayerPrefs.GetInt("Outdoor", 0) == 1)
            transform.GetChild(0).gameObject.SetActive(false);
        else
            transform.GetChild(0).gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayEnable : MonoBehaviour
{
    public FoodIconChoose foodIcon;
    public bool isVIP = false;


    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        //if (PlayerPrefs.GetInt("Poop", 0) == 1)
        //{
        //    transform.Find("Poop").gameObject.SetActive(true);
        //    return;
        //}

        if (isVIP)
            transform.Find(foodIcon.tempIndex.ToString()).gameObject.SetActive(true);
        else
            transform.Find(transform.parent.GetComponentInChildren<FoodIconChoose>().tempIndex.ToString()).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnbaleGameobjectOnEnable : MonoBehaviour
{
    public GameObject[] enable, disable;

    public bool onlyEnableAfterTimePassed = false;

    private void OnEnable()
    {
        //if (onlyEnableAfterTimePassed)
        //{
        //    if (Time.timeScale < 1f)
        //        return;
        //}

        foreach (GameObject obj in enable)
            obj.SetActive(true);

        foreach (GameObject obj in disable)
            obj.SetActive(false);
    }
}

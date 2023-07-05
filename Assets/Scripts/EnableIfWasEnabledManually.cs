using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfWasEnabledManually : MonoBehaviour
{
    public string nameOfPlayerPref;

    void Start()
    {
        //if (PlayerPrefs.GetInt(nameOfPlayerPref, 0) == 0)
        //{
        //    gameObject.SetActive(false);
        //}
        //else
        //{
        //    gameObject.SetActive(true);
        //}
    }

    private void OnEnable()
    {
        //if (Time.timeSinceLevelLoad > 0.6f)
        //{
        //    PlayerPrefs.SetInt(nameOfPlayerPref, 1);
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevel : MonoBehaviour
{
    public string saveName;

    public void EnableNewLevel()
    {
        PlayerPrefs.SetInt(saveName, 1);

        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive((PlayerPrefs.GetInt(saveName, 0) == 1));
    }
}

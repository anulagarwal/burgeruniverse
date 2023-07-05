using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaszNyil : MonoBehaviour
{
    public int index = 0;
    void Start()
    {
        if (PlayerPrefs.GetInt("Nyil" + index + ToString(), 0) == 1)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            PlayerPrefs.SetInt("Nyil" + index + ToString(), 1);
            gameObject.SetActive(false);
        }
    }
}

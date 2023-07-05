using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrBarBoomer : MonoBehaviour
{
    public GameObject cleared;

    private void Clear()
    {
        FindObjectOfType<BoomerSpawner>().gameObject.SetActive(false);
        foreach (Boomer boomer in FindObjectsOfType<Boomer>())
        {
            boomer.DieOfConfusion();
        }

        transform.parent.parent.Find("Powers").gameObject.SetActive(false);

        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
        cleared.SetActive(true);

        gameObject.SetActive(false);
    }
}

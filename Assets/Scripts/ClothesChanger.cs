using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesChanger : MonoBehaviour
{
    public GameObject[] deactivate, activate;
    void Start()
    {
        for (int u = 0; u < deactivate.Length; u++)
        {
            deactivate[u].gameObject.SetActive(false);
        }
        for (int u = 0; u < activate.Length; u++)
        {
            activate[u].gameObject.SetActive(true);
        }
    }

    void Update()
    {

    }
}

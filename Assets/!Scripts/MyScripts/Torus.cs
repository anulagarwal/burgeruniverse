using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torus : MonoBehaviour
{
    void Start()
    {
        Invoke("StartUfoAnim", 0.31f);
    }

    int tempIndex = 1;

    private void StartUfoAnim()
    {
        transform.GetChild(tempIndex).gameObject.SetActive(true);
        tempIndex++;
        if (tempIndex == 2)
            Invoke("StartUfoAnim", 0.32f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableDisable : MonoBehaviour
{
    public GameObject[] objectsToDisable, objectsToEnable;

    private void OnEnable()
    {
        for (int i = 0; i < objectsToDisable.Length; i++)
            objectsToDisable[i].SetActive(false);

        for (int i = 0; i < objectsToEnable.Length; i++)
            objectsToEnable[i].SetActive(true);
    }
}

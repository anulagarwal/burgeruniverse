using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesMachine : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public GameObject upgradeManager;

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject.name.Contains("Player"))
    //    {
    //        upgradeManager.SetActive(true);
    //    }

    //}

    private bool canEnable = true;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            upgradeManager.SetActive(false);

            canEnable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canEnable)
            return;

        if (other.gameObject.name.Contains("Player"))
        {
            if (!Input.GetMouseButton(0))
            {
                upgradeManager.SetActive(true);
                canEnable = false;
            }
        }
    }
}

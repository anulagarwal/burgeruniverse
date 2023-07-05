using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUprgade : MonoBehaviour
{

    public BrickMachineUpgrades upgr;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            upgr.ShowUpgrades(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            upgr.ShowUpgrades(false);
        }
    }

}

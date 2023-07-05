using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneygive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
            MoneySpawner.Instance.UpdateMoney(9999);
    }
}

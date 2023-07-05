using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            if (other.GetComponent<PlayerCasino>() != null)
                other.GetComponent<PlayerCasino>().ThrowCheater();
        }
        if (other.gameObject.name.Contains("Security"))
        {
            if (other.GetComponent<Security>() != null)
                other.GetComponent<Security>().ThrowCheater();
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

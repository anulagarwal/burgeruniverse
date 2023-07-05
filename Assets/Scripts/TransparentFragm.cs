using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentFragm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            Destroy(gameObject);
        }

    }
}

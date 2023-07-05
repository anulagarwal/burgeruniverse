using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("CubesHolder"))
        {
            other.GetComponent<CubesHolder>().Stop();
        }
    }
}

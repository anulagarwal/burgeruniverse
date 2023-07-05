using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("UFO"))
        {
            other.GetComponent<UfoController>().Finish();
        }
    }
}

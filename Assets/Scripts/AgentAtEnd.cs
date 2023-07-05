using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAtEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            other.GetComponent<AgentRun>().ShootEnd();
        }

    }
}

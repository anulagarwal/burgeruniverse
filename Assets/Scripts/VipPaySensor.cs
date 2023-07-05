using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipPaySensor : MonoBehaviour
{
    private List<int> indexesOfVIPs = new List<int>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Guest") && !indexesOfVIPs.Contains(other.GetInstanceID()))
        {
            indexesOfVIPs.Add(other.GetInstanceID());
            for (int i = 0; i < 6; i++)
                GetComponent<SpawnMoneyPos>().SpawnMoneyOnce();
        }
    }
}

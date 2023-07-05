using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHolder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Hammer"))
        {
            GetComponentInChildren<ParticleSystem>().Play();
            Destroy(GetComponent<Collider>());
            GetComponent<Animation>().Play();
        }
    }

    private void AddForceToChildren()
    {
        foreach (Rigidbody rigidbody in transform.GetComponentsInChildren<Rigidbody>())
        {
            //rigidbody.AddForce(transform.up * 240f);
            rigidbody.AddForce(transform.up * 100f);
        }
    }
}

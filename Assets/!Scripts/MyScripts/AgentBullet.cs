using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBullet : MonoBehaviour
{
    public float speed = -200f;

    void Start()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().AddForce(transform.up * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Target"))
        {
            if (other.GetComponent<PlayerCrates>() != null)
                other.GetComponent<PlayerCrates>().EnableRagdoll(true);
            else
                other.GetComponentInParent<PlayerCrates>().EnableRagdoll(true);
            other.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}

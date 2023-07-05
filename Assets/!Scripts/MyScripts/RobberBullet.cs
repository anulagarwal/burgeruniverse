using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberBullet : MonoBehaviour
{
    void Start()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().AddForce(transform.up * -200f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            other.GetComponentInChildren<ParticleSystem>().Play();
            other.GetComponentInParent<Animator>().SetBool("die", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAwayTargetOfHandHold : MonoBehaviour
{
    void Start()
    {
        transform.parent = null;
        transform.LookAt(GameObject.FindGameObjectWithTag("CasinoExit").transform.position + Vector3.up * 0.5f);
        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 333f);
    }

    void Update()
    {

    }
}

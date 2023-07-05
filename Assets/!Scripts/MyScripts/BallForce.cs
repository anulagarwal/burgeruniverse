using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * -555f);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("COLLIDED BALL WITH " + collision.gameObject.name);
    //}

    // Update is called once per frame
    void Update()
    {

    }
}

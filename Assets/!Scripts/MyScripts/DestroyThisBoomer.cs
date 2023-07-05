using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisBoomer : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyThis", 2.4f);
    }

    private void DestroyThis()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2f);
    }
}

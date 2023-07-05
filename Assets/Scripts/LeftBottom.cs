using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBottom : MonoBehaviour
{
    public Material bakeMat;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Contains("friess") || collision.gameObject.name.Contains("Ice_CUBE"))
        {
            if (collision.gameObject.transform.parent != transform.parent)
            {
                collision.gameObject.transform.parent = transform.parent;
                if (collision.gameObject.name.Contains("friess"))
                    collision.gameObject.GetComponent<MeshRenderer>().material = bakeMat;
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

    }
}

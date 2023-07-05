using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unparent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;

        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.up, 180);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

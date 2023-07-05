using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPosOn : MonoBehaviour
{
    public bool x, y, z;

    private float startX, startY, startZ;

    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;
    }

    void FixedUpdate()
    {
        if (x)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
        if (y)
        {
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }
        if (z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
        }
    }
}

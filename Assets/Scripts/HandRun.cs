using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRun : MonoBehaviour
{
    public float forwardSpeed = 10f, leftSpeed = 5f, forwardDefSpeed;

    void Start()
    {
        forwardDefSpeed = forwardSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.right * leftSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * -leftSpeed * Time.deltaTime, Space.World);
    }
}

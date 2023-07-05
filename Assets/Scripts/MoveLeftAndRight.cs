using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndRight : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.right * 1f * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * -1f * Time.deltaTime, Space.World);
    }
}

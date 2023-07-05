using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public bool canRun = false;

    void Update()
    {
        if (canRun)
            transform.Translate(Vector3.forward * 4.2f * Time.deltaTime);
        else
            transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
    }
}

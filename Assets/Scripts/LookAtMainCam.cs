using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCam : MonoBehaviour
{
    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}

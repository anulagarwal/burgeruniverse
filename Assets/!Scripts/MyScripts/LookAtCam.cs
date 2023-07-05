using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    public float moveBy = 0.1f;

    private bool isCircle = false;
    private Transform cam;

    void Start()
    {
        if (gameObject.name.Contains("Circle"))
            isCircle = true;
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cam);

        if (isCircle)
        {
            transform.localPosition = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, cam.position, moveBy);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCard : MonoBehaviour
{
    public Transform followHand;
    private Vector3 offset;
    private Transform mainCam;

    void Start()
    {
        mainCam = Camera.main.transform;
        offset = transform.position - followHand.position;
    }

    void LateUpdate()
    {
        transform.position = followHand.position + offset;
        transform.LookAt(mainCam);
    }
}

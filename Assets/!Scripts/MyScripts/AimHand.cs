using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHand : MonoBehaviour
{
    public Transform target;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.position = cam.WorldToScreenPoint(target.position);

        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;

        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    transform.position = cam.WorldToScreenPoint(hitInfo.point);
        //}
    }
}

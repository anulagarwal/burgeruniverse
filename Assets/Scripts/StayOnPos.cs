using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPos : MonoBehaviour
{
    private Vector3 tempPosinWorld;

    void Start()
    {
        tempPosinWorld = Camera.main.ScreenToWorldPoint(transform.position);
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(tempPosinWorld);
    }
}

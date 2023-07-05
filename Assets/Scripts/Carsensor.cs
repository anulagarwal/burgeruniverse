using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carsensor : MonoBehaviour
{
    void Start()
    {
        cars = transform.parent.Find("Cars");
    }

    void Update()
    {

    }

    Transform cars;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("CAR"))
        {

        }

    }
}

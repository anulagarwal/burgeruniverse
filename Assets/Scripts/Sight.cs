using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("HEAD"))
        {
            if (!Input.GetMouseButton(0))
                return;

            FindObjectOfType<RestaurantGirl>().GameFailed();
            foreach (Waitress character in FindObjectsOfType<Waitress>())
            {
                character.Failed();
            }
        }

    }
}

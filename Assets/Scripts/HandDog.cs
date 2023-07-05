using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDog : MonoBehaviour
{
    void Start()
    {

    }

    public void HandOut()
    {
        FindObjectOfType<DogHolder>().StartMovingBack();

        GetComponent<Animation>().Play("HandOut");

    }
}

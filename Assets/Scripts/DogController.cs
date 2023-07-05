using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetSad()
    {
        GetComponent<Animator>().SetBool("sad", true);
    }

    public void SetHappy()
    {
        GetComponent<Animator>().SetBool("happy", true);
    }
}

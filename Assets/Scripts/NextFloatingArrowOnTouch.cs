using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloatingArrowOnTouch : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            ArrowTutorialHolder.Instance.EnableNextTut();
        }

    }
}

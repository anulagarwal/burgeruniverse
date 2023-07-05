using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDestroyer : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Card>() || other.GetComponentInChildren<Plus3Cards>() || other.GetComponentInChildren<CardStop>() || other.GetComponentInChildren<ColorChange>() || other.GetComponentInChildren<CardChangeHand>())
        {
            if (other.transform.parent.gameObject.name.Contains("Card(") || other.transform.parent.gameObject.name.Contains("Card_"))
                Destroy(other.transform.parent.gameObject);
            else
                Destroy(other.gameObject);
        }
    }
}

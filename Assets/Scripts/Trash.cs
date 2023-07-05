using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public BoxCollider trigger;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            trigger.enabled = true;
            trigger.isTrigger = true;
            GetComponentInChildren<Animation>().Play();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            trigger.enabled = false;
            trigger.isTrigger = false;
            GetComponentInChildren<Animation>().Play("TrashDown");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterSensor : MonoBehaviour
{
    Collider coll;
    void Start()
    {
        coll = GetComponent<Collider>();

        KeepResetingCollider();
    }

    public void KeepResetingCollider()
    {
        if (coll.enabled)
        {
            coll.enabled = false;

            if (collidedObjects.Count > 0)
                collidedObjects.RemoveAt(0);
            bool canSpawn = false;
            sensor.CanSpawne(canSpawn);

            Invoke("KeepResetingCollider", 0.1f);
        }
        else
        {
            coll.enabled = true;
            Invoke("KeepResetingCollider", 0.45f);
        }
    }

    void Update()
    {

    }


    public List<GameObject> collidedObjects = new List<GameObject>();

    private void EnableColl()
    {
        GetComponent<Collider>().enabled = true;
    }

    public PosFirstSensor sensor;

    //private Animator waitressAnimator;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("WAITRESS") || other.gameObject.name.Contains("PlayerCasino"))
        {
            //if (waitressAnimator == null && other.gameObject.name.Contains("WAITRESS"))
            //    waitressAnimator = other.GetComponent<Animator>();

            if (!collidedObjects.Contains(other.gameObject))
                collidedObjects.Add(other.gameObject);

            sensor.CanSpawne(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name.Contains("Waiter") || other.gameObject.name.Contains("Player"))
        {
            if (collidedObjects.Contains(other.gameObject))
                collidedObjects.Remove(other.gameObject);

            bool canSpawn = false;

            for (int i = 0; i < collidedObjects.Count; i++)
            {
                if (other.gameObject.name.Contains("Waiter") || other.gameObject.name.Contains("Player"))
                    canSpawn = true;
            }

            sensor.CanSpawne(canSpawn);
        }

    }
}

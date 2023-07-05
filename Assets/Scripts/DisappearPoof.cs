using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearPoof : MonoBehaviour
{
    public GameObject[] objectToActivate, objectToDeactivate;
    public bool canRotate = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            GameObject obj2 = Instantiate(Resources.Load("Poof") as GameObject, transform.position, Quaternion.identity);
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);

            if (objectToActivate != null)
            {
                for (int i = 0; i < objectToActivate.Length; i++)
                    objectToActivate[i].SetActive(true);
            }
            if (objectToDeactivate != null)
            {
                for (int i = 0; i < objectToDeactivate.Length; i++)
                    objectToDeactivate[i].SetActive(false);
            }

            Destroy(obj2, 1.7f);

            if (canRotate)
                Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (canRotate)
            transform.Rotate(Vector3.up, 100f * Time.deltaTime);
    }

}

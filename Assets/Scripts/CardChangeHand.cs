using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChangeHand : MonoBehaviour
{
    public GameObject camHolderHolder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HAND")
        {
            camHolderHolder.SetActive(true);

            GameObject obj2 = Instantiate(Resources.Load("ColorChangeParticle") as GameObject, transform.position - Vector3.up * 0.35f, Quaternion.identity);
            obj2.transform.Rotate(Vector3.right, -90f);

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using HighlightPlus;

public class Safe : MonoBehaviour
{
    public string nameOfCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(nameOfCollision))
        {
            if (collision.transform.Find("Key Gold").gameObject.activeInHierarchy)
            {
                collision.transform.Find("Key Gold").gameObject.SetActive(false);
                GetComponent<Collider>().enabled = false;
                transform.GetComponentsInChildren<Animation>()[1].Play();
                //GetComponent<HighlightEffect>().highlighted = false;
                Invoke("SecondAnim", 1.3f);
            }
        }
    }

    private void SecondAnim()
    {
        transform.GetComponentsInChildren<Animation>()[0].Play();
        transform.GetComponentInChildren<ParticleSystem>().Play();
        Destroy(this, 0.1f);
    }
}

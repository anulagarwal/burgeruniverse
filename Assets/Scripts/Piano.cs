using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Obi;

public class Piano : MonoBehaviour
{
    public Transform ropeUp;

    public ParticleSystem balloonPopPart;

    public GameObject circleDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            balloonPopPart.Play();

            circleDisplay.SetActive(false);

            transform.parent = null;
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<PlayerCrates>().EnableRagdoll(true);
            Destroy(GetComponent<Rigidbody>());

            //foreach (ObiParticleAttachment attachment in FindObjectsOfType<ObiParticleAttachment>())
            //{
            //    attachment.enabled = false;
            //    attachment.transform.parent.parent.parent = null;
            //}

            ropeUp.GetComponent<Animation>().Play();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplashParticle : MonoBehaviour
{
    private bool canActivate = true;

    private void OnTriggerEnter(Collider other)
    {
        if (canActivate && other.gameObject.layer == 20)
        {
            canActivate = false;
            GetComponent<ParticleSystem>().Play();
            transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 0.3f);
        }
    }
}

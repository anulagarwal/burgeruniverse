using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRobber : MonoBehaviour
{
    public Material goodMat, wrongMat;
    public GameObject bulletPref;

    private MeshRenderer rend;

    public void Shoot()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            rend.material = goodMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            rend.material = wrongMat;
        }
    }
}

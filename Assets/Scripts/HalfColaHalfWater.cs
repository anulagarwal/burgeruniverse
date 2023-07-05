using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfColaHalfWater : MonoBehaviour
{
    private ParticleSystem cola, water;

    void Start()
    {
        cola = transform.Find("PourPart").GetComponent<ParticleSystem>();
        water = transform.Find("PourPart_White").GetComponent<ParticleSystem>();

    }

    public void StartPouring()
    {
        StartCoroutine(ColaAndWater());
    }

    public void StopPouring()
    {
        StopAllCoroutines();
        water.Stop();
        cola.Stop();
    }

    IEnumerator ColaAndWater()
    {
        bool isCola = true;
        while (true)
        {
            if (isCola)
            {
                isCola = false;
                water.Stop();
                cola.Play();
            }
            else
            {
                isCola = true;
                water.Play();
                cola.Stop();
            }


            yield return new WaitForSeconds(Random.Range(0.4f, 1.2f));
        }
    }


    void Update()
    {

    }
}

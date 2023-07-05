using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriesEmit : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void StartEmitting()
    {
        StartCoroutine(FriesFall());
    }

    private void StopEmitting()
    {
        StopAllCoroutines();
    }

    public GameObject fry;

    IEnumerator FriesFall()
    {
        while (true)
        {
            GameObject obj1 = Instantiate(fry, transform.GetChild(0).position, Quaternion.identity);
            obj1.transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

            yield return new WaitForSeconds(0.08f);
        }
    }

}

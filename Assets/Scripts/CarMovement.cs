using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(EnableCars());
    }


    IEnumerator EnableCars()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            transform.GetChild(3).gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);
            transform.GetChild(2).gameObject.SetActive(true);

            //yield return new WaitForSeconds(2f);
            //transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    void Update()
    {

    }

}

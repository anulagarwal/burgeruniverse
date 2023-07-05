using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Faszjancsi : MonoBehaviour
{
    bool puncika;
    GameObject obj;

    void Start()
    {

    }

    void Update()
    {
        if (puncika)
        {
            puncika = false;

            for (int i = 0; i < transform.childCount; i++)
            {
                Rigidbody rb = obj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
                //transform.GetChild(i).gameObject.GetComponent<Rigidbody>() = rb;
            }
        }
    }
}

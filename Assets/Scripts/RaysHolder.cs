using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysHolder : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (transform.localScale.y < 1f)
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 1.5f * Time.deltaTime, transform.localScale.z);
            else
                transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.y > 0.4458418f)
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 1.5f * Time.deltaTime, transform.localScale.z);
            else
                transform.localScale = new Vector3(transform.localScale.x, 0.4458418f, transform.localScale.z);
        }
    }
}

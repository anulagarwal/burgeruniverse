using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    public float fillSpeed = 0.06f;
    public float wobbleSpeed = 0.06f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Renderer>().material.SetFloat("_Fill", GetComponent<Renderer>().material.GetFloat("_Fill") + fillSpeed);
            GetComponent<Renderer>().material.SetFloat("_WobbleX", GetComponent<Renderer>().material.GetFloat("_WobbleX") + wobbleSpeed);

        }
    }
}

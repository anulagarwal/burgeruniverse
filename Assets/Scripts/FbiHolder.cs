using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FbiHolder : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInChildren<Animator>().SetBool("dance", true);
        }
    }

    private void StopRunning()
    {
        GetComponentInChildren<Animator>().SetBool("run", false);
    }
}

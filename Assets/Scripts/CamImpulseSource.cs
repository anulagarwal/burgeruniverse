using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamImpulseSource : MonoBehaviour
{
    void Start()
    {

    }

    public void Shake()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(2f);
    }

    void Update()
    {

    }
}

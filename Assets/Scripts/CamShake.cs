using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    private CinemachineImpulseSource impulse;

    void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();

        //Shake();
    }

    void Update()
    {

    }

    public void Shake()
    {
        impulse.GenerateImpulse(2f);
    }
}

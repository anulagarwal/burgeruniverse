using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLeg : MonoBehaviour
{
    private void TouchedGround()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        FindObjectOfType<CratesManager>().SetPlayerToCrate();
    }

    private void TouchedGround2()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

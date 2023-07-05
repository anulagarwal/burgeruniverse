using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLegEnemy : MonoBehaviour
{
    private void TouchedGround()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponentInParent<EnemyCrates>().SetPlayerToCrate();
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

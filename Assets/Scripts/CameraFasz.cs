using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFasz : MonoBehaviour
{
    public GameObject crosshair, bullet;

    private void EnableCrosshair()
    {
        crosshair.SetActive(true);
    }

    private void Shoot()
    {
        bullet.SetActive(true);
    }
}

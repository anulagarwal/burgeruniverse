using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMachineUpgrades : MonoBehaviour
{
    public Transform posOfUpgrades;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = mainCam.WorldToScreenPoint(posOfUpgrades.position);
    }

    public void ShowUpgrades(bool canShow = true)
    {
        transform.GetChild(0).gameObject.SetActive(canShow);
        transform.GetChild(1).gameObject.SetActive(canShow);
    }
}

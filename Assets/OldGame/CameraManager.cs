using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private int tempCamPriority = 10;
    private string tempName = "";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdatePlayerCamTransform(AnimalChange.tempAnimal);
    }

    public void EnableCamera(string camName)
    {
        if (tempName != camName)
        {
            tempName = camName;
            tempCamPriority += 2;
            transform.Find(camName).GetComponent<CinemachineVirtualCamera>().Priority = tempCamPriority;
        }
    }

    public void UpdatePlayerCamTransform(Transform targetTransform)
    {
        transform.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>().m_Follow = targetTransform;
    }

    public void DisableCameras()
    {
        foreach (CinemachineVirtualCamera camera in transform.GetComponentsInChildren<CinemachineVirtualCamera>())
            camera.m_Follow = null;
    }
}

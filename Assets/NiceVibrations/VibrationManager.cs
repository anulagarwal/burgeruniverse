using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;


public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool canVibrate = true;
    private float coolDownTime = 0.2f, addedAmplitude = 0f;
    private float startLightImpactValue = 0.05f;

    private void RestoreAddedAmplitude()
    {
        addedAmplitude = 0f;
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            startLightImpactValue = 0.05f;
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            startLightImpactValue = 0.31f;
    }

    public void Vibr_LightImpact()
    {
        if (canVibrate)
        {
            if (!HapticController.IsPlaying())
            {
                CancelInvoke("RestoreAddedAmplitude");

                HapticPatterns.PlayConstant(startLightImpactValue + addedAmplitude, 0.5f, 0.015f);

                addedAmplitude += 0.035f;

                if (addedAmplitude > 0.8f)
                    addedAmplitude = 0.8f;

                Invoke("RestoreAddedAmplitude", 0.6f);
            }
        }
    }
    public void Vibr_SoftImpact()
    {
        if (canVibrate)
        {
            if (!HapticController.IsPlaying())
                HapticPatterns.PlayConstant(0.3f, 0.3f, 0.09f);
        }
    }

    public void Vibr_Error()
    {
        if (canVibrate)
        {
            if (!HapticController.IsPlaying())
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
        }
    }

    private void CanVibrate()
    {
        canVibrate = true;
    }
}

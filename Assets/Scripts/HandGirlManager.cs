using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGirlManager : MonoBehaviour
{
    public Avatar av;

    public void Eat()
    {
        //RemoveAvatar();
        GetComponentInChildren<Animator>().Play("Eat");

        Invoke("PlayHot", 1.3f);
    }

    public GameObject endPanel;

    public GameObject fragments;
    public Rigidbody[] rbsToSetNonKin;
    public GameObject[] disappearObjects;

    public void ReactToWater()
    {
        GetComponentInChildren<Animator>().SetBool("punch", true);

        Invoke("EndPanelActivate", 1.6f);
    }

    public void FragmentsEnable()
    {
        //for (int i = 0; i < rbsToSetNonKin.Length; i++)
        //    rbsToSetNonKin[i].isKinematic = false;
        for (int i = 0; i < disappearObjects.Length; i++)
            disappearObjects[i].SetActive(false);

        GetComponentInChildren<Animator>().SetBool("punch", false);

        fragments.SetActive(true);
    }

    private void EndPanelActivate()
    {
        endPanel.SetActive(true);
    }

    public void GetRose()
    {
        GetComponentInChildren<Animator>().SetBool("rose", true);
    }

    public void Angry()
    {
        Invoke("AngryInvoke", 0.5f);
    }

    private void AngryInvoke()
    {
        GetComponentInChildren<Animator>().SetBool("angry", true);
    }

    public void PlayHot()
    {
        RemoveAvatar();
        GetComponentInChildren<Animator>().Play("Chili");
        FindObjectOfType<TargetYesOrNo>().transform.SetPositionAndRotation(FindObjectOfType<TargetYesOrNo>().intoMouthTransform.position, FindObjectOfType<TargetYesOrNo>().intoMouthTransform.rotation);
        FindObjectOfType<TargetYesOrNo>().DropPepper();
        FindObjectOfType<TargetYesOrNo>().WeightToZero();

        Invoke("InvokeWaterIn", 1f);
    }

    private void InvokeWaterIn()
    {
        FindObjectOfType<HandHolder>().GetComponent<Animation>().Play("HandWaterIn");
    }

    public void AddAvatar()
    {
        GetComponentInChildren<Animator>().avatar = av;
    }

    public void RemoveAvatar()
    {
        av = GetComponentInChildren<Animator>().avatar;
        Destroy(GetComponentInChildren<Animator>().avatar);
        GetComponentInChildren<Animator>().avatar = null;
    }
}

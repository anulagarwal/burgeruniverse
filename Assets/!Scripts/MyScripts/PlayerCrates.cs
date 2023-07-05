using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrates : MonoBehaviour
{
    public void EnableRagdoll(bool canEnable = true)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = canEnable;
        }
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = !canEnable;
        }

        if (canEnable && (GetComponent<Animator>() != null))
            GetComponent<Animator>().enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;
        if (canEnable)
            Destroy(GetComponent<Collider>());
    }

    public Transform runParent;
    private bool startedRun = false;

    private void RunHitman()
    {
        if (!startedRun)
        {
            startedRun = true;
            transform.parent = runParent;
            runParent.GetComponent<Animation>().Play();
        }
    }

    void Start()
    {
        //EnableRagdoll(false);

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = true;
        }
    }

    public GameObject[] replaceNames;

    private void Dance()
    {
        for (int i = 0; i < replaceNames.Length; i++)
        {
            string tempName = replaceNames[i].name;
            tempName = tempName.Replace("TP", "TT");
            replaceNames[i].name = tempName;
        }

        GetComponent<Animation>().Play();
    }

    public void StartDance()
    {
        Invoke("Dance", 1f);
    }
}

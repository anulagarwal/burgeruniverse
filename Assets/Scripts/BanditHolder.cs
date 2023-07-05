using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BanditHolder : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public GameObject sprayPoof, hiddenBandit;

    private void StopRunning()
    {
        GetComponentInChildren<Animator>().SetBool("run", false);
    }

    public void Spray()
    {
        sprayPoof.SetActive(true);
        Invoke("DisablePlayer", 1.35f);
    }

    private void DisablePlayer()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("EnableHidden", 0.02f);
    }

    private void EnableHidden()
    {
        hiddenBandit.SetActive(true);
        //Invoke("EnableRig", 1.3f);
    }

    public void EnableRig()
    {
        hiddenBandit.transform.Find("Rig 1").gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeSelfie : MonoBehaviour
{
    public Animation camAnim;
    public GameObject textToEnable, textToDisable;
    public GameObject[] objectsToHide;

    public GameObject selfieEffects;

    private int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (index == 0)
            {
                TakeIt();
            }
            else if (index == 1)
            {
                PhoneBack();
            }
            index++;
        }
    }

    public void TakeIt()
    {
        textToDisable.SetActive(false);
        textToEnable.SetActive(true);

        selfieEffects.SetActive(true);
        for (int i = 0; i < objectsToHide.Length; i++)
        {
            objectsToHide[i].SetActive(false);
        }
        GetComponent<Image>().enabled = false;

        Invoke("StartCamAnim", 0.8f);
    }

    private void StartCamAnim()
    {
        camAnim.Play();
    }

    public void PhoneBack()
    {
        selfieEffects.SetActive(false);
        for (int i = 0; i < objectsToHide.Length; i++)
        {
            objectsToHide[i].SetActive(true);
        }

        objectsToHide[3].SetActive(false);

        GetComponent<Image>().enabled = true;

        Invoke("SelfieSend", 0.8f);
    }

    public GameObject playerSelfie, enemyRespondToSelfie;

    private void SelfieSend()
    {
        playerSelfie.SetActive(true);

        Invoke("EnemyRespond", 1f);
    }

    private void EnemyRespond()
    {
        enemyRespondToSelfie.SetActive(true);
        Invoke("RealText", 1f);
    }

    public GameObject clearedPanel;

    private void RealText()
    {
        enemyRespondToSelfie.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        Invoke("Cleared", 1f);

    }

    private void Cleared()
    {
        textToDisable.transform.parent.gameObject.SetActive(false);
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
        clearedPanel.SetActive(true);
    }
}

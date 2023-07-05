using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMC : MonoBehaviour
{
    private int tempIndex = -1;

    public Transform taskTexts;
    public GameObject clearedPanel, endPanel, endCam;

    public Animator girlAnimator;
    public GameObject orderBubble, cashregister, friesCam, friesEmitter, handInfiniteTut;
    public Animation friesHolderAnim;

    public GameObject[] orderOfItemsInRegister;
    public GameObject followHandCamClose, followHandCam, indexMeter, payCam, handTutDownUp, tray, endcamplayer;

    private void DisableHandInfiniteTut()
    {
        handInfiniteTut.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (tempIndex == -1)
            {
                orderBubble.SetActive(true);
            }
            else if (tempIndex == 0)
            {
                cashregister.SetActive(true);
            }
            else if (tempIndex == 1)
            {
                orderOfItemsInRegister[0].SetActive(true);
            }
            else if (tempIndex == 2)
            {
                orderOfItemsInRegister[1].SetActive(true);
            }
            else if (tempIndex == 3)
            {
                orderOfItemsInRegister[2].SetActive(true);
            }
            else if (tempIndex == 4)
            {
                orderBubble.SetActive(false);
                cashregister.GetComponent<Animation>().Play("CashDown");
                girlAnimator.SetBool("happy", true);
                Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                Invoke("StopConfetti", 1.3f);
                Invoke("Hand", 2.2f);
            }
            else if (tempIndex == 5)
            {
                FindObjectOfType<HalfColaHalfWater>().StopPouring();

                Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                Invoke("StopConfetti", 1.3f);
                //followHandCam.SetActive(true);
                //followHandCamClose.SetActive(false);
            }
            else if (tempIndex == 6)
            {
                taskTexts.GetChild(1).gameObject.SetActive(false);
                taskTexts.GetChild(2).gameObject.SetActive(true);
                friesCam.SetActive(true);
            }
            else if (tempIndex == 7)
            {
                friesHolderAnim.Play();
                friesEmitter.SetActive(true);
                handInfiniteTut.SetActive(true);
                Invoke("DisableHandInfiniteTut", 3f);
            }
            else if (tempIndex == 8)
            {
                handPourTut.SetActive(true);
                handPourTut.transform.GetChild(1).gameObject.SetActive(false);
                handPourTut.transform.GetChild(2).gameObject.SetActive(true);
                friesHolderAnim.Play("GoBack");
                Invoke("DisableHandPourTut", 3f);
                friesCam.GetComponent<Animation>().Play();
                indexMeter.SetActive(true);
            }
            else if (tempIndex == 9)
            {
                Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                Invoke("StopConfetti", 1.3f);
                indexMeter.SetActive(false);
            }
            else if (tempIndex == 10)
            {
                FindObjectOfType<GirlAskForFoodHolder>().GetComponentInChildren<Animator>().SetBool("happy", false);
                taskTexts.GetChild(2).gameObject.SetActive(false);
                taskTexts.GetChild(3).gameObject.SetActive(true);
                payCam.SetActive(true);
            }
            else if (tempIndex == 11)
            {
                card.SetActive(true);
                handTutDownUp.SetActive(true);
                Invoke("DisableHandTutDownUp", 3f);
            }
            else if (tempIndex == 12)
            {
                tray.SetActive(true);
                cardReaderAnim.Play("CardReaderAnim");
            }
            else if (tempIndex == 13)
            {
                endcamplayer.SetActive(true);
                FindObjectOfType<GirlAskForFoodHolder>().GetComponentInChildren<Animator>().SetBool("happy", true);
                taskTexts.gameObject.SetActive(false);
            }

            tempIndex++;
        }

        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.C))
        {
            clearedPanel.SetActive(true);
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            endCam.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            endPanel.SetActive(true);
        }
    }

    private void DisableHandTutDownUp()
    {
        handTutDownUp.SetActive(false);
    }

    public GameObject handPourTut, card;

    private void DisableHandPourTut()
    {
        handPourTut.SetActive(false);
    }

    public GameObject hand;

    public Animation cardReaderAnim;

    private void Hand()
    {
        hand.SetActive(true);
        taskTexts.GetChild(0).gameObject.SetActive(false);
        taskTexts.GetChild(1).gameObject.SetActive(true);
    }

    private void StopConfetti()
    {
        Camera.main.transform.GetChild(0).gameObject.SetActive(false);
    }
}

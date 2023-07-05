using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    int index = 0;

    public GameObject clearedPanel, girl, virtualPhone, talkIceCream, textTalk, textShow, textDraw, texts_golf, texts_iceCream, texts_water;

    public bool isDrawing, isDescribing, isShowing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (index == 0)
            {
                girl.GetComponent<Animator>().SetBool("pickRight", true);
                FindObjectOfType<CamActivity>().BackToStartPos();

                if (isShowing)
                    texts_golf.SetActive(true);
                else if (isDescribing)
                    texts_iceCream.SetActive(true);
                else
                    texts_water.SetActive(true);
            }
            if (index == 1)
            {
                FindObjectOfType<CamActivity>().HideCards();
                if (isDescribing)
                    textTalk.SetActive(true);
                else if (isShowing)
                    textShow.SetActive(true);
                else
                    textDraw.SetActive(true);
            }
            if (index == 2)
            {
                virtualPhone.SetActive(true);

                textTalk.SetActive(false);
                textShow.SetActive(false);
                textDraw.SetActive(false);

                if (isDescribing)
                {
                    girl.GetComponent<Animator>().SetBool("talk", true);
                    talkIceCream.transform.GetChild(0).gameObject.SetActive(true);//DESCRIBE1
                }
                else if (isShowing)
                {
                    girl.GetComponent<Animator>().SetBool("golf", true);
                    FindObjectOfType<CamActivity>().CamToGolf();
                    textDraw.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    girl.GetComponent<Animator>().SetBool("draw", true);
                    textDraw.transform.parent.gameObject.SetActive(false);
                    girl.transform.Find("Board").gameObject.SetActive(true);
                }
            }
            if (index == 3)
            {
                virtualPhone.transform.Find("WrongButton").GetComponent<Animation>().Play();//BAD TIP
            }
            if (index == 4)
            {
                if (isDescribing)
                {
                    talkIceCream.transform.GetChild(0).gameObject.SetActive(false);
                    talkIceCream.transform.GetChild(1).gameObject.SetActive(true);//DESCRIBE2
                }
            }
            if (index == 5)
            {
                virtualPhone.transform.Find("WrongButton").GetComponent<Animation>().Play();//BAD TIP
            }
            if (index == 6)
            {
                if (isDescribing)
                {
                    talkIceCream.transform.GetChild(1).gameObject.SetActive(false);
                    talkIceCream.transform.GetChild(2).gameObject.SetActive(true);//DESCRIBE3
                }
            }
            if (index == 7)
            {
                virtualPhone.transform.Find("WrongButton").GetComponent<Animation>().Play();//BAD TIP
            }
            if (index == 8)
            {
                if (isDescribing)
                {
                    talkIceCream.transform.GetChild(2).gameObject.SetActive(false);
                    talkIceCream.transform.GetChild(3).gameObject.SetActive(true);//DESCRIBE3
                }
            }
            if (index == 9)
            {
                if (isShowing)
                    FindObjectOfType<CamActivity>().CamToGolfBack();

                virtualPhone.transform.Find("GoodButton").GetComponent<Animation>().Play();//GOOD TIP
                girl.GetComponent<Animator>().Play("Win");
                talkIceCream.transform.parent.gameObject.SetActive(false);
                Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                Invoke("Cleared", 1.5f);
            }
            index++;
        }
    }

    private void Cleared()
    {
        virtualPhone.SetActive(false);
        clearedPanel.SetActive(true);
    }
}

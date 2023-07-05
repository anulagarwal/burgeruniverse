using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlStuff : MonoBehaviour
{
    public GameObject camGirl, camGrid, foundTextHolder, talkBubbleHolder, dish, clothes, bomb;


    public GameObject clearedPanel, endPanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            transform.Find("happy").gameObject.SetActive(false);
            GetComponent<Animator>().SetBool("happy", false);
            talkBubbleHolder.transform.Find("Clothes_Good").gameObject.SetActive(false);
            talkBubbleHolder.transform.Find("Hungry_Good").gameObject.SetActive(false);
            talkBubbleHolder.transform.Find("Hungry").gameObject.SetActive(true);
            Invoke("SwitchToGrid", 3f);
        }

        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.C))
        {
            talkBubbleHolder.SetActive(false);
            clearedPanel.SetActive(true);
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            //endCam.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            talkBubbleHolder.SetActive(false);
            endPanel.SetActive(true);
        }
    }

    public GameObject tutGrid;
    private void DisableTutGrid()
    {
        tutGrid.SetActive(false);
    }

    public int tempIndex = 0;
    bool activatedNext = false;
    public bool isFail = false;

    private void CanActivateNext()
    {
        activatedNext = false;
    }

    public void ActivateNext()
    {
        if (isFail)
        {
            ActivateClothes();
            return;
        }

        if (activatedNext)
            return;

        activatedNext = true;
        Invoke("CanActivateNext", 1f);

        if (tempIndex == 0)
        {
            ActivateClothes();
        }
        else if (tempIndex == 1)
        {
            ActivateDish();
        }

        tempIndex++;

        if (bomb.activeInHierarchy)
        {
            React(false);
            return;
        }

        if (tempIndex == 1)
            React(true);
        //else
        //    React(false);
    }

    public void SwitchToGrid()
    {
        camGrid.SetActive(true);
        talkBubbleHolder.transform.Find("Clothes").gameObject.SetActive(false);
        talkBubbleHolder.transform.Find("Hungry").gameObject.SetActive(false);
        tutGrid.SetActive(true);
        Invoke("DisableTutGrid", 3f);
    }

    public void SwitchToGirl()
    {
        camGrid.SetActive(false);
        foundTextHolder.SetActive(false);

        if (tempIndex == 2)
            dish.GetComponent<Animation>().Play();
    }

    public void React(bool happy)
    {
        if (happy)
        {
            transform.Find("happy").gameObject.SetActive(true);
            GetComponent<Animator>().SetBool("happy", true);
            if (tempIndex == 1)
                talkBubbleHolder.transform.Find("Clothes_Good").gameObject.SetActive(true);
            else if (tempIndex == 2)
                talkBubbleHolder.transform.Find("Hungry_Good").gameObject.SetActive(true);
        }
        else
        {
            bomb.SetActive(true);
            transform.Find("angry").gameObject.SetActive(true);
            GetComponent<Animator>().SetBool("angry", true);


            //talkBubbleHolder.transform.Find("Clothes_Bad").gameObject.SetActive(true);

            //if (tempIndex == 1)
            //else if (tempIndex == 2)
            //talkBubbleHolder.transform.Find("Hungry_Bad").gameObject.SetActive(true);
        }
    }

    private bool isActivatedDish = false, isActivatedClothes = false;

    public void ActivateClothes()
    {
        if (isActivatedClothes)
            return;

        isActivatedClothes = true;

        clothes.SetActive(true);
        //foundTextHolder.SetActive(true);

        //Invoke("SwitchToGirl", 0.1f);
    }

    public void ActivateDish()
    {
        if (isActivatedDish)
            return;

        isActivatedDish = true;

        dish.SetActive(true);
        foundTextHolder.SetActive(true);

        Invoke("SwitchToGirl", 2f);
    }

    void Start()
    {
        Invoke("SwitchToGrid", 4f);
    }

}

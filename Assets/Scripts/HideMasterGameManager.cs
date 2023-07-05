using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using HighlightPlus;

public class HideMasterGameManager : MonoBehaviour
{
    public GameObject tryHideButton, fbiText, hideBGImage;

    public Animation doorAnim, fbiAnim;
    private float tempIndex = 0;
    public GameObject clearedPanel, endPanel, endCam, handRestaurant, girlRestaurant, dirLight;
    public Transform tasksDisplay;

    private void StopConfetti()
    {
        Camera.main.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void CopIn()
    {
        hideBGImage.SetActive(true);
        FindObjectOfType<CamShake>().Shake();

        doorAnim.Play("DoorBreak");
        fbiAnim.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //if (tempIndex == 0)
            //{
            //    tryHideButton.SetActive(true);
            //}
            /*else*/
            if (tempIndex == 0)
            {
                FindObjectOfType<ProgressBarFill>().transform.parent.gameObject.SetActive(false);

                foreach (LookAtCam circle in FindObjectsOfType<LookAtCam>())
                {
                    circle.gameObject.SetActive(false);
                }

                tryHideButton.SetActive(false);
                endCam.SetActive(true);
                //FindObjectOfType<HiddenPlayer>().GetComponentInChildren<HighlightEffect>().highlighted = false;
                FindObjectOfType<HiddenPlayer>().transform.Find("Rig 1").gameObject.SetActive(false);

                fbiText.SetActive(false);
                Invoke("CopIn", 1.4f);
            }

            //else if (tempIndex == 2)
            //{
            //    tasksDisplay.GetChild(0).gameObject.SetActive(false);
            //    tasksDisplay.GetChild(1).gameObject.SetActive(true);

            //    girlRestaurant.SetActive(true);
            //    handRestaurant.SetActive(true);
            //    dirLight.GetComponent<Light>().intensity = 0.5f;
            //}
            //else if (tempIndex == 3)
            //{
            //    tasksDisplay.GetChild(1).gameObject.SetActive(false);
            //    tasksDisplay.GetChild(2).gameObject.SetActive(true);
            //}

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



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmanGameManager : MonoBehaviour
{
    public GameObject clearedPanel, endPanel, endCam, playerRun;

    public GameObject[] cameras, taskTexts;

    public Animation pisitolAnim;

    public GameObject phoneCanv, phoneScreen;

    private int tempIndex = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (tempIndex == 0)
            {
                cameras[0].SetActive(true);
            }
            else if (tempIndex == 1)
            {
                pisitolAnim.Play();
            }
            else if (tempIndex == 2)
            {
                phoneScreen.SetActive(true);
                phoneCanv.SetActive(true);

                cameras[0].SetActive(false);

                taskTexts[0].SetActive(false);
                taskTexts[1].SetActive(true);
            }
            else if (tempIndex == 3)
            {
                Camera.main.transform.GetChild(0).gameObject.SetActive(false);
                phoneScreen.SetActive(false);
                phoneCanv.SetActive(false);
                clearedPanel.SetActive(false);

                taskTexts[1].SetActive(false);
                taskTexts[2].SetActive(true);

                cameras[0].SetActive(false);
                cameras[1].SetActive(true);

                playerRun.SetActive(true);
            }

            tempIndex++;
        }



        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.C))
        {
            clearedPanel.SetActive(true);
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            //endCam.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            endPanel.SetActive(true);
        }

    }
}

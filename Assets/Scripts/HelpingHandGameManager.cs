using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpingHandGameManager : MonoBehaviour
{
    private float tempIndex = 1;
    public GameObject clearedPanel, endPanel, endCam, handRestaurant, girlRestaurant, dirLight;
    public Transform tasksDisplay;

    private void StopConfetti()
    {
        Camera.main.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (tempIndex == 0)
            {
                Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                Invoke("StopConfetti", 1.3f);
            }
            else if (tempIndex == 1)
            {
                Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                Invoke("StopConfetti", 1.3f);
            }
            else if (tempIndex == 2)
            {
                tasksDisplay.GetChild(0).gameObject.SetActive(false);
                tasksDisplay.GetChild(1).gameObject.SetActive(true);

                girlRestaurant.SetActive(true);
                handRestaurant.SetActive(true);
                dirLight.GetComponent<Light>().intensity = 0.5f;
            }
            else if (tempIndex == 3)
            {
                tasksDisplay.GetChild(1).gameObject.SetActive(false);
                tasksDisplay.GetChild(2).gameObject.SetActive(true);
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



}

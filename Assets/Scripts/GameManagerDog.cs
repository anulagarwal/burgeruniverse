using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDog : MonoBehaviour
{
    private int index = 0;

    void Start()
    {

    }

    public GameObject catGood;

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.R))
        //    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);


        if (Input.GetKeyDown(KeyCode.N))
        {
            switch (index)
            {
                case 0:
                    transform.GetChild(0).gameObject.SetActive(true);
                    //transform.GetChild(0).gameObject.SetActive(false);
                    break;
                case 1:
                    transform.GetChild(1).gameObject.SetActive(true);
                    //transform.GetChild(1).gameObject.SetActive(false);
                    break;
                case 2:
                    transform.GetChild(2).gameObject.SetActive(true);
                    break;
                case 3:
                    catGood.SetActive(true);
                    break;

            }


            index++;
        }

    }
}

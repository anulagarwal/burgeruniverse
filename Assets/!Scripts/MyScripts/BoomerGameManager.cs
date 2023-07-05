using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoomerGameManager : MonoBehaviour
{
    public GameObject[] talkingAtStart;
    public GameObject cam2, cam3, tavcsoUI, powers, boomersstart, swipeAttack, swipeBall, progressBar;
    int index = -1;
    void Start()
    {
        cam2.SetActive(false);
        cam3.SetActive(false);
        tavcsoUI.SetActive(true);
        powers.SetActive(false);

        //boomersstart.SetActive(false);
        //FindObjectOfType<BoomerSpawner>().FirstSpawn();

        tavcsoUI.SetActive(false);
        cam2.SetActive(true);
    }

    private void PushOff()
    {
        cam2.transform.Find("Girl").GetComponent<Animator>().SetBool("push", false);
    }

    private void BoomerFall()
    {
        cam2.transform.Find("BOOMER").Rotate(Vector3.up, -20f);
        cam2.transform.Find("BOOMER").GetComponent<Animator>().avatar = null;
        cam2.transform.Find("BOOMER").GetComponent<Animator>().Play("Fall");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyUp(KeyCode.N))
        {
            if (index == -1)
            {
                talkingAtStart[0].SetActive(false);
                talkingAtStart[1].SetActive(true);
                cam2.transform.Find("Girl").Rotate(Vector3.up, 73f);
                Invoke("BoomerFall", 1.4f);
                cam2.transform.Find("Girl").Find("ChatAnims").gameObject.SetActive(false);
                cam2.transform.Find("Girl").GetComponent<Animator>().SetBool("push", true);
                Invoke("PushOff", 0.2f);
            }
            if (index == 0)
            {
                talkingAtStart[1].SetActive(false);
                FindObjectOfType<BoomerSpawner>().FirstSpawn();

                tavcsoUI.SetActive(false);
                cam2.SetActive(true);
            }
            if (index == 1)
            {
                //boomersstart.SetActive(false);
                //FindObjectOfType<BoomerSpawner>().FirstSpawn();
                boomersstart.SetActive(false);


                cam3.SetActive(true);
                powers.SetActive(true);
                cam2.SetActive(false);
                progressBar.SetActive(true);
            }
            if (index == 2)
            {
                swipeAttack.SetActive(true);
                swipeBall.SetActive(true);
            }
            if (index == 3)
            {
                swipeAttack.SetActive(false);
                swipeBall.SetActive(false);
            }

            index++;
        }
    }
}

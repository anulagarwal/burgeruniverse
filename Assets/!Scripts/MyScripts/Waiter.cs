using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Waiter : MonoBehaviour
{
    public Transform enemyDrinkParent, playerDrinkParent, enemyPlateParent, playerPlateParent;
    public Animator sittingIdle;

    private bool canMove = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Stop"))
        {
            canMove = false;
            GetComponent<Animator>().SetBool("stand", true);
            GameObject.FindGameObjectWithTag("EndCam").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("EndCam").transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().m_Priority = 50;

            GameObject.FindGameObjectWithTag("EnemyDrink").transform.SetParent(enemyDrinkParent);
            enemyDrinkParent.transform.GetChild(0).transform.localPosition = Vector3.zero;
            GameObject.FindGameObjectWithTag("EnemyPlate").transform.SetParent(enemyPlateParent);
            enemyPlateParent.transform.GetChild(0).transform.localPosition = Vector3.zero;
            GameObject.FindGameObjectWithTag("PlayerDrink").transform.SetParent(playerDrinkParent);
            playerDrinkParent.transform.GetChild(0).transform.localPosition = Vector3.zero;
            GameObject.FindGameObjectWithTag("PlayerPlate").transform.SetParent(playerPlateParent);
            playerPlateParent.transform.GetChild(0).transform.localPosition = Vector3.zero;

            //foreach (Rigidbody rb in enemyDrinkParent)
            //    rb.isKinematic = true;

            //foreach (Rigidbody rb in playerDrinkParent)
            //    rb.isKinematic = true;

            //foreach (Rigidbody rb in enemyPlateParent)
            //    rb.isKinematic = true;

            //foreach (Rigidbody rb in playerPlateParent)
            //    rb.isKinematic = true;

            Invoke("NextCam", 2f);

            //SCORE
            if (gameObject.name.Contains("Player"))
            {
                if (ProgressBarSide.PlayerInstance.tempScore >= ProgressBarSide.EnemyInstance.tempScore)
                {
                    CoinsHolder.HappyEmojiInstance.PlayThisAt(ProgressBar2.Instance.buttonImages[1].transform);
                    ProgressBarSide.PlayerInstance.IncreaseBars(0.2f);
                }
                else
                {
                    CoinsHolder.SadEmojiInstance.PlayThisAt(ProgressBar2.Instance.buttonImages[1].transform);
                    //ProgressBarSide.PlayerInstance.IncreaseBars(0f);
                }
            }
        }
    }

    private void NextCam()
    {
        //SCORE
        if (gameObject.name.Contains("Player"))
        {
            if (ProgressBarSide.PlayerInstance.tempScore >= ProgressBarSide.EnemyInstance.tempScore)
                Debug.Log("xy");
            else
                ProgressBarSide.EnemyInstance.IncreaseBars(0.2f);
        }

        GameObject.FindGameObjectWithTag("EndCam").transform.GetChild(1).GetComponent<CinemachineVirtualCamera>().m_Priority = 60;
        Invoke("LastCam", 2f);
    }

    private void LastCam()
    {
        GameObject.FindGameObjectWithTag("EndCam").transform.GetChild(2).GetComponent<CinemachineVirtualCamera>().m_Priority = 70;
        Invoke("Win", 1.5f);
    }

    bool isWinner = true;

    public void Win()
    {
        if (ProgressBarSide.PlayerInstance.tempScore >= ProgressBarSide.EnemyInstance.tempScore)
        {
            if (gameObject.name.Contains("Player"))
            {
                sittingIdle.SetBool("won", true);
                sittingIdle.transform.Find("WonEmoji").gameObject.SetActive(true);
                isWinner = true;
            }
            else
            {
                sittingIdle.SetBool("lost", true);
                sittingIdle.transform.Find("LostEmoji").gameObject.SetActive(true);
                isWinner = false;
            }
        }
        else
        {
            if (gameObject.name.Contains("Player"))
            {
                sittingIdle.SetBool("lost", true);
                sittingIdle.transform.Find("LostEmoji").gameObject.SetActive(true);
                isWinner = false;
            }
            else
            {
                sittingIdle.SetBool("won", true);
                sittingIdle.transform.Find("WonEmoji").gameObject.SetActive(true);
                isWinner = true;
            }
        }

        Invoke("ChooseWinner", 1.5f);
    }


    private void ChooseWinner()
    {
        if (gameObject.name.Contains("Player"))
            transform.Rotate(Vector3.up, -90f);
        else
            transform.Rotate(Vector3.up, 90f);

        if (isWinner)
        {
            GetComponent<Animator>().SetBool("stand", false);
            GetComponent<Animator>().SetBool("kick", true);
            Invoke("DanceRot", 1.1f);
        }
        else
        {
            GetComponent<Animator>().SetBool("stand", false);
            Invoke("Die", 0.5f);
        }
    }

    private void DanceRot()
    {
        transform.rotation = Quaternion.identity;
        if (gameObject.name.Contains("Player"))
        {
            GameObject.FindGameObjectWithTag("Confettis").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Panels").transform.GetChild(0).gameObject.SetActive(true);
        }
        //GameObject.FindGameObjectWithTag("Themes").SetActive(false);
    }

    private void Die()
    {
        GetComponent<Animator>().SetBool("die", true);
        if (gameObject.name.Contains("Player"))
            GameObject.FindGameObjectWithTag("Panels").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Themes").SetActive(false);
    }

    void Update()
    {
        if (canMove)
            transform.Translate(Vector3.forward * 2f * Time.deltaTime);
    }
}

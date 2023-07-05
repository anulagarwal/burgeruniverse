using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHolder : MonoBehaviour
{

    void Start()
    {
        endCam.SetActive(true);

        if (gameObject.name.Contains("Good"))
        {
            FindObjectOfType<DogHolder>().GetComponent<Animation>().Play("DogAngry");
            FindObjectOfType<DogHolder>().GetComponentInChildren<Animator>().SetBool("angry", true);
        }
        else
        {
            FindObjectOfType<DogHolder>().GetComponent<Animation>().Play("DogScared");
            FindObjectOfType<DogHolder>().GetComponentInChildren<Animator>().SetBool("scared", true);
        }

    }

    public void CatBack()
    {
        GetComponent<Animation>().Play("CatMoveBack");
        GetComponentInChildren<Animator>().SetBool("walkBack", true);
    }


    public void InvokeGameOver()
    {
        Invoke("GameOver", 2f);
    }

    public GameObject clearedPanel, endPanel, endCam;

    private void GameOver()
    {
        endPanel.SetActive(true);

    }

    void Update()
    {

    }

    private void CatAttack()
    {
        GetComponentInChildren<Animator>().SetBool("attack", true);
    }
}

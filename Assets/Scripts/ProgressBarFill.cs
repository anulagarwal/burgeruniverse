using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarFill : MonoBehaviour
{
    public bool isHidingGame = false;

    public Image im;
    private bool isOver = false;

    public Gradient gradient;
    public float speed = 10f, speedBack = 0.05f;

    void Start()
    {
        im = GetComponent<Image>();
        im.fillAmount = 0f;
    }

    public void TakePhoto()
    {
        if (gameObject.name.Contains("STALKER"))
        {
            isOver = true;
            if (objectsToActivate.Length >= 1)
            {
                StartCoroutine(FinishCleared());
                return;
            }
            foreach (Waitress character in FindObjectsOfType<Waitress>())
                character.Win();
            FindObjectOfType<RestaurantGirl>().GameWon();
        }
    }

    void Update()
    {
        if (isOver)
            return;




        if (isHidingGame)
        {
            //if(Input.g)
            im.color = gradient.Evaluate(im.fillAmount);


            return;
        }


        if (Input.GetMouseButton(0))
            im.fillAmount += speed * Time.deltaTime;
        else
            im.fillAmount -= speedBack * Time.deltaTime;
        im.color = gradient.Evaluate(im.fillAmount);

        if (im.fillAmount >= 1f)
        {
            if (isOver)
                return;

            isOver = true;
            if (objectsToActivate.Length >= 1)
            {
                StartCoroutine(FinishCleared());
                return;
            }
            foreach (Waitress character in FindObjectsOfType<Waitress>())
                character.Win();
            FindObjectOfType<RestaurantGirl>().GameWon();

        }
    }

    public GameObject[] objectsToActivate, objectsToActivate2, objectsToActivate3;

    IEnumerator FinishCleared()
    {
        int tempWaitressPos = FindObjectOfType<WaitressHolder>().tempPosIndex;
        if (tempWaitressPos == 1 || tempWaitressPos == 4)
        {
            objectsToActivate[0].SetActive(true);
            objectsToActivate[1].SetActive(true);
            yield return new WaitForSeconds(1);
            objectsToActivate[2].SetActive(true);
            yield return new WaitForSeconds(1.3f);
            objectsToActivate[3].SetActive(true);
        }
        else if (tempWaitressPos == 3 || tempWaitressPos == 6)
        {
            objectsToActivate2[0].SetActive(true);
            objectsToActivate2[1].SetActive(true);
            yield return new WaitForSeconds(1);
            objectsToActivate2[2].SetActive(true);
            yield return new WaitForSeconds(1.3f);
            objectsToActivate2[3].SetActive(true);
        }
        else if (tempWaitressPos == 2 || tempWaitressPos == 5)
        {
            objectsToActivate3[0].SetActive(true);
            objectsToActivate3[1].SetActive(true);
            yield return new WaitForSeconds(1);
            objectsToActivate3[2].SetActive(true);
            yield return new WaitForSeconds(1.3f);
            objectsToActivate3[3].SetActive(true);
        }


    }

}

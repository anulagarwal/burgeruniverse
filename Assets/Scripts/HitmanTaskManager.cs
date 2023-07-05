using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmanTaskManager : MonoBehaviour
{
    private int tempIndex = 1;

    public GameObject[] textsToActivate, imagesToActivate;

    public void ActivateNextText()
    {
        textsToActivate[tempIndex].SetActive(true);
        imagesToActivate[tempIndex - 1].SetActive(true);

        textsToActivate[tempIndex - 1].SetActive(false);

        if (tempIndex - 2 >= 0)
            imagesToActivate[tempIndex - 2].SetActive(false);

        tempIndex++;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

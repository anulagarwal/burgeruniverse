using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutImagesManager : MonoBehaviour
{
    public GameObject[] tutorials;

    void Start()
    {
        tutorials[0].SetActive(false);
        tutorials[1].SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            tutorials[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tutorials[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tutorials[2].SetActive(true);
        }
    }
}

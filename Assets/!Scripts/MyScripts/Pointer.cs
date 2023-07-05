using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    [HideInInspector] public Transform target;

    public bool isUsingW_Key = true;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    public void ShowImagePointer(bool canShow)
    {
        GetComponentInChildren<Image>().enabled = canShow;

    }


    void LateUpdate()
    {
        if (target != null)
            transform.position = cam.WorldToScreenPoint(target.position);

        if (isUsingW_Key)
        {
            if (Input.GetKeyUp(KeyCode.W))
                GetComponentInChildren<Image>().enabled = !GetComponentInChildren<Image>().enabled;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                GetComponentInChildren<Image>().enabled = true;
            if (Input.GetMouseButtonUp(0))
                GetComponentInChildren<Image>().enabled = false;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //    GetComponentInChildren<Image>().enabled = !GetComponentInChildren<Image>().enabled;

        transform.position = Input.mousePosition;
    }
}

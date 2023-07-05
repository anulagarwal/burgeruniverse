using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTarget : MonoBehaviour
{
    public GameObject clearedPanel, endCam;
    public Transform leftHand, rightHand, aimLeftHand, aimRightHand;
    public Transform tempTarget;
    private Camera cam;

    public Vector2 clampScaleOfTarget;
    public float offsetZ;

    void Start()
    {
        cam = Camera.main;
        tempTarget.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            clearedPanel.SetActive(true);
            endCam.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //transform.position = leftHand.position;
            Debug.Log(Input.mousePosition);

            leftHand.GetComponent<HandController>().enabled = false;
            rightHand.GetComponent<HandController>().enabled = false;


            //if (Input.mousePosition.x < 235f)
            if (Input.mousePosition.x < 950f)
            {
                tempTarget.GetComponent<Image>().color = aimLeftHand.GetComponent<Image>().color;
                leftHand.GetComponent<HandController>().enabled = true;
                transform.position = leftHand.position;
            }
            else
            {
                tempTarget.GetComponent<Image>().color = aimRightHand.GetComponent<Image>().color;
                rightHand.GetComponent<HandController>().enabled = true;
                transform.position = rightHand.position;
            }

            aimLeftHand.gameObject.SetActive(false);
            aimRightHand.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            tempTarget.gameObject.SetActive(true);
            float tempVal = 0f;
            tempVal = transform.position.z - offsetZ;
            tempTarget.localScale = new Vector3(tempVal, tempVal, 1f);
            tempTarget.localScale = new Vector3(Mathf.Clamp(tempTarget.localScale.x, clampScaleOfTarget.x, clampScaleOfTarget.y), Mathf.Clamp(tempTarget.localScale.y, clampScaleOfTarget.x, clampScaleOfTarget.y), Mathf.Clamp(tempTarget.localScale.z, clampScaleOfTarget.x, clampScaleOfTarget.y));

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                transform.position = Vector3.MoveTowards(transform.position, hitInfo.point, 5f * Time.deltaTime);
                tempTarget.position = cam.WorldToScreenPoint(transform.position);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftHand.position, 5f * Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(0))
        {
            rightHand.GetComponent<HandController>().enabled = false;
            rightHand.GetComponent<HandTouch>().Release();
            leftHand.GetComponent<HandController>().enabled = false;
            leftHand.GetComponent<HandTouch>().Release();

            tempTarget.gameObject.SetActive(false);
            aimLeftHand.gameObject.SetActive(true);
            aimRightHand.gameObject.SetActive(true);
        }
    }
}

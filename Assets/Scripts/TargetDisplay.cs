using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDisplay : MonoBehaviour
{
    public LayerMask mask;
    public Transform targetTransform, targetPlace;

    private Camera cam;

    private bool isFollowingMouse = false;

    public Color dragColor, defColor, goodColor;

    void Start()
    {
        cam = Camera.main;
        transform.position = cam.WorldToScreenPoint(targetTransform.position);
    }

    public bool x, y, z;
    public float offset = 0.5f;

    public GameObject leftRay, rightRay;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Distance(transform.position, Input.mousePosition) < 20f)
            {
                if ((leftRay != null) && name.Contains("Left"))
                {
                    rightRay.SetActive(false);
                    leftRay.SetActive(true);
                }
                else if ((rightRay != null) && name.Contains("Right"))
                {
                    rightRay.SetActive(true);
                    leftRay.SetActive(false);
                }

                GetComponent<Image>().color = dragColor;
                isFollowingMouse = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isFollowingMouse)
                GetComponent<Image>().color = goodColor;
            isFollowingMouse = false;
            leftRay.SetActive(false);
            rightRay.SetActive(false);
        }

        if (isFollowingMouse)
        {
            transform.position = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 1000f, mask, QueryTriggerInteraction.Collide))
            {
                Debug.Log("POINTER IS AT: " + hitInfo.point);

                //if (x)
                //{
                //    targetPlace.position = new Vector3(targetPlace.position.x, hitInfo.point.y, hitInfo.point.z);
                //}
                //else if (y)
                //{
                //    targetPlace.position = new Vector3(hitInfo.point.x, targetPlace.position.y, hitInfo.point.z);
                //}
                //else if (z)
                //{
                //    targetPlace.position = new Vector3(hitInfo.point.x, hitInfo.point.y, targetPlace.position.z);
                //}
                //else
                targetPlace.position = hitInfo.point;

                targetPlace.rotation = targetTransform.parent.rotation;
                //targetPlace.Rotate(targetPlace.up, 45f);

                Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cam.WorldToScreenPoint(targetTransform.position), 1000f * Time.deltaTime);
        }



    }
}

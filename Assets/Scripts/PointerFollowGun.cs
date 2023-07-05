using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerFollowGun : MonoBehaviour
{
    public Transform followThis;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (followThis)
        {
            transform.position = cam.WorldToScreenPoint(followThis.position);
        }
    }

    public void StartFollow(Transform foll)
    {
        followThis = foll;
        transform.position = cam.WorldToScreenPoint(followThis.position);
        GetComponentInChildren<Image>().enabled = true;
    }

    public void StopFollowing()
    {
        followThis = null;
        GetComponentInChildren<Image>().enabled = false;
    }
}

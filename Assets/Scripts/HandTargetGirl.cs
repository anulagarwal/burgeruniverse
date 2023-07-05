using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTargetGirl : MonoBehaviour
{
    public Transform roseTransform, roseNewParent;

    private void GetRose()
    {
        roseTransform.parent = roseNewParent;
    }

    private void SetHappyGirl()
    {
        FindObjectOfType<HandGirlManager>().GetComponentInChildren<Animator>().SetBool("happy", true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

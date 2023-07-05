using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineScript : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public Animator animatorOfPlayer;

    public void EnableAfraid()
    {
        //animatorOfPlayer.SetLayerWeight(2, 1f);
        animatorOfPlayer.SetBool("afraid", true);
    }

    public void EnableAngry()
    {
        //animatorOfPlayer.SetLayerWeight(2, 0f);
        animatorOfPlayer.SetBool("angry", true);
        FindObjectOfType<GirlStuff>().React(false);
    }
}

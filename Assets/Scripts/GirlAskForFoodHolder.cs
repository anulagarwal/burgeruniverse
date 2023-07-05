using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAskForFoodHolder : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Rotate()
    {
        animator.SetBool("rotate", true);
        animator.SetBool("rotateLeft", false);
        animator.SetBool("walk", false);
    }

    private void RotateLeft()
    {
        animator.SetBool("rotateLeft", true);
        animator.SetBool("rotate", false);
        animator.SetBool("walk", false);
    }

    public void SitDown()
    {
        animator.SetBool("sit", true);
    }

    public void GoToChair()
    {
        animator.SetBool("walk", true);
        GetComponent<Animation>().enabled = true;
        GetComponent<Animation>().Play();
    }

    void Update()
    {

    }
}

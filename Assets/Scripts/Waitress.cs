using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Waitress : MonoBehaviour
{
    public Animation handIKanim;
    public bool canBeHappy = true;

    private Rig rig;

    void Start()
    {
        animator = GetComponent<Animator>();
        rig = gameObject.GetComponentInChildren<Rig>();
    }

    private bool isOver = false;

    void Update()
    {
        if (isOver)
            return;

        if (!canBeHappy)
            return;

        if (Input.GetMouseButtonDown(0))
            SetHappy();
        else if (Input.GetMouseButtonUp(0))
            SetHappy(false);
    }

    private Animator animator;

    public void SetHappy(bool isHappy = true)
    {
        animator.SetBool("happy", isHappy);
        if (isHappy)
        {
            if (!gameObject.name.Contains("Waitress"))
            {
                animator.SetLayerWeight(1, 1f);
                animator.SetLayerWeight(2, 0f);
            }
            else
            {
                if (rig != null)
                {
                    //StopAllCoroutines();
                    //StartCoroutine(RigDown());
                    rig.weight = 0f;
                }
            }
            if (GetComponentInChildren<ParticleSystem>() != null)
                GetComponentInChildren<ParticleSystem>().Play();
        }
        else
        {
            if (!gameObject.name.Contains("Waitress"))
            {
                animator.SetLayerWeight(1, 0f);
                animator.SetLayerWeight(2, 1f);
            }
            else
            {
                if (rig != null)
                {
                    //StopAllCoroutines();
                    //StartCoroutine(RigUp());
                    rig.weight = 1f;
                }
            }
            if (GetComponentInChildren<ParticleSystem>() != null)
                GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    IEnumerator RigUp()
    {
        while (rig.weight < 1f)
        {
            rig.weight += 2f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator RigDown()
    {
        while (rig.weight > 0f)
        {
            rig.weight -= 2f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void Win()
    {
        FindObjectOfType<RestaurantGirl>().GameWon();
        isOver = true;
        if (!gameObject.name.Contains("Waitress"))
        {
            animator.SetBool("win", true);
            GetComponentInChildren<RaysHolder>().gameObject.SetActive(false);
        }
        else
            Invoke("FaceKissGirl", 1f);

        if (!gameObject.name.Contains("Waitress"))
        {
            animator.SetLayerWeight(1, 0f);
            animator.SetLayerWeight(2, 0f);
            animator.SetLayerWeight(3, 0f);
            animator.SetLayerWeight(4, 0f);
        }

        FindObjectOfType<ProgressBarFill>().transform.parent.gameObject.SetActive(false);
    }

    public Animation chair;
    private void ChairPush()
    {
        chair.Play();
    }

    private void FaceKissBoy()
    {
        animator.SetLayerWeight(2, 0f);
        animator.SetLayerWeight(3, 0f);
        animator.SetLayerWeight(4, 0f);
        animator.SetLayerWeight(5, 1f);
    }

    public GameObject winCam;
    private void FaceKissGirl()
    {
        if (rig != null)
            rig.weight = 0f;
        animator.SetBool("win", true);
        animator.SetLayerWeight(1, 0f);
        animator.SetLayerWeight(2, 1f);
        winCam.SetActive(true);
    }

    public void Failed()
    {
        FindObjectOfType<ProgressBarFill>().transform.parent.gameObject.SetActive(false);
        isOver = true;
        animator.SetBool("fail", true);
        if (!gameObject.name.Contains("Waitress"))
        {
            animator.SetLayerWeight(2, 0f);
            animator.SetLayerWeight(3, 1f);
            GetComponentInChildren<RaysHolder>().gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHolder : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public GameObject camToEnable, lampHolder, camLAstCat;
    public Animation animOfRig;

    private void DisableThis()
    {
        FindObjectOfType<CatHolder>().InvokeGameOver();
        lampHolder.SetActive(true);
        camToEnable.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Jump()
    {
        GetComponentInChildren<Animator>().SetBool("jump", true);
    }

    private void MoveHead()
    {
        animOfRig.Play();

    }

    public GameObject camSecond, handHolder;

    private void InvokeToMoveBack()
    {
        animOfRig.Play("RigDown");
        GetComponent<Animation>().Play("DogMoveBack");

    }

    public void StartMovingBack()
    {
        Invoke("InvokeToMoveBack", 1f);

    }

    public GameObject clearedPanel;
    public void ClearedLevel()
    {
        clearedPanel.SetActive(true);
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void InvokeCatBack()
    {
        FindObjectOfType<CatHolder>().CatBack();
    }

    public void Bark()
    {
        Invoke("InvokeCatBack", 0.7f);

        GetComponentInChildren<Animator>().SetBool("bark", true);
        Invoke("ClearedLevel", 2f);
    }

    public void MoveBackFromCouch()
    {
        camSecond.SetActive(false);
        GetComponentInChildren<Animator>().SetBool("walk", true);

    }

    public void MoveToCouch()
    {
        Invoke("HandEnable", 1f);
        camSecond.SetActive(true);
        GetComponentInChildren<Animator>().SetBool("walk", true);
        GetComponent<Animation>().Play();
    }

    public GameObject tutHand;

    private void HandEnable()
    {
        handHolder.SetActive(true);
        tutHand.SetActive(true);

        Invoke("Hypnotize", 3f);
    }

    public GameObject hypnotizedParticle;

    public void Hypnotize()
    {
        hypnotizedParticle.SetActive(true);
    }

    private void StopMoving()
    {
        GetComponentInChildren<Animator>().SetBool("walk", false);

    }
}

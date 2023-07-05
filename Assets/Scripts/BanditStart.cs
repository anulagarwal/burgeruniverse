using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditStart : MonoBehaviour
{
    public Transform money;
    public GameObject fbiText, money1, money2, blue, yellow;
    public Animation doorAnim, roll1, roll2;

    private void FbiKnock()
    {
        FindObjectOfType<CamShake>().Shake();

        fbiText.SetActive(true);
        doorAnim.Play();
    }

    private void PutDownMoney()
    {
        money1.SetActive(false);
        money2.SetActive(false);
        money.transform.parent = null;
    }

    private void RunHide()
    {
        transform.parent.GetComponent<Animation>().Play();
        GetComponent<Animator>().SetBool("run", true);
        money1.SetActive(false);
        money2.SetActive(false);
    }

    public Transform rightHand, leftHand;

    private void GetBlue()
    {
        blue.transform.parent = leftHand;
    }

    private void GetYellow()
    {
        yellow.transform.parent = rightHand;
    }

    private void Spray()
    {
        transform.GetComponentInParent<BanditHolder>().Spray();

        Invoke("Roll1", 0.3f);
        Invoke("Roll2", 1.1f);
    }

    private void Roll1()
    {
        roll1.Play();
    }

    private void Roll2()
    {
        roll2.Play();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

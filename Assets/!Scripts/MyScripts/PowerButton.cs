using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    public int tempIndex = 0;

    public GameObject girl;

    public void SocialButton()
    {
        if (tempIndex == 0)
            GetComponentInChildren<Animation>().Play("CoolDownFast");
        else if (tempIndex == 1)
            GetComponentInChildren<Animation>().Play("CoolDownMiddle");
        else
            GetComponentInChildren<Animation>().Play("CoolDownSlow");

        StartCoroutine(Social());
    }

    IEnumerator Social()
    {
        girl.SetActive(true);
        girl.transform.parent.GetComponent<Animation>().Play("RightIn");

        yield return new WaitForSeconds(0.7f);

        FindObjectOfType<BallSpawner>().SpawnBall();

        yield return new WaitForSeconds(1.6f);

        girl.transform.parent.GetComponent<Animation>().Play("RightOut");
        yield return new WaitForSeconds(0.3f);
        girl.SetActive(false);
    }

    public void HearthButton()
    {
        if (tempIndex == 0)
            GetComponentInChildren<Animation>().Play("CoolDownFast");
        else if (tempIndex == 1)
            GetComponentInChildren<Animation>().Play("CoolDownMiddle");
        else
            GetComponentInChildren<Animation>().Play("CoolDownSlow");

        SwipeAttack();
        //StartCoroutine(Heart());
    }

    private void SwipeAttack()
    {
        FindObjectOfType<SwipeAttack>().GetComponent<Animation>().Play();
    }

    IEnumerator Heart()
    {
        girl.SetActive(true);
        //girl.transform.parent.GetComponent<Animation>().Play("LeftIn");
        girl.transform.parent.GetComponent<Animation>().Play("RightIn");
        yield return new WaitForSeconds(1.5f);

        int destroyedIndex = 0;
        List<Boomer> boomers = new List<Boomer>();

        foreach (Boomer boomer in FindObjectsOfType<Boomer>())
        {
            if (Random.Range(0, 2) == 0)
            {
            }
            destroyedIndex++;
            boomers.Add(boomer);
            boomer.StartConfusion();

            if (destroyedIndex >= 30)
                break;
        }

        yield return new WaitForSeconds(2.5f);
        //girl.transform.parent.GetComponent<Animation>().Play("LeftOut");
        girl.transform.parent.GetComponent<Animation>().Play("RightOut");
        yield return new WaitForSeconds(0.3f);
        girl.SetActive(false);
    }

    public void EarthButton()
    {
        if (tempIndex == 0)
            GetComponentInChildren<Animation>().Play("CoolDownFast");
        else if (tempIndex == 1)
            GetComponentInChildren<Animation>().Play("CoolDownMiddle");
        else
            GetComponentInChildren<Animation>().Play("CoolDownSlow");


        StartCoroutine(Heat());
    }

    IEnumerator Heat()
    {
        girl.SetActive(true);
        //girl.transform.parent.GetComponent<Animation>().Play("LeftIn");
        girl.transform.parent.GetComponent<Animation>().Play("LeftIn");
        yield return new WaitForSeconds(1.5f);

        int destroyedIndex = 0;
        List<Boomer> boomers = new List<Boomer>();

        foreach (Boomer boomer in FindObjectsOfType<Boomer>())
        {
            if (Random.Range(0, 2) == 0)
            {
            }
            destroyedIndex++;
            boomers.Add(boomer);
            boomer.StartGlobalWarming();

            if (destroyedIndex >= 30)
                break;
        }

        yield return new WaitForSeconds(2.5f);
        //girl.transform.parent.GetComponent<Animation>().Play("LeftOut");
        girl.transform.parent.GetComponent<Animation>().Play("LeftOut");
        yield return new WaitForSeconds(0.3f);
        girl.SetActive(false);
    }
}

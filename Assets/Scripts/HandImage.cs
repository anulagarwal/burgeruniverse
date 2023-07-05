using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using HighlightPlus;

public class HandImage : MonoBehaviour
{
    public GameObject nextHandImage;

    private Image im;

    private Transform handPos;

    void Start()
    {
        if (nextHandImage != null)
        {
            nextHandImage.SetActive(false);
            nextHandImage.GetComponent<HandImage>().enabled = false;
            //nextHandImage.GetComponent<HandImage>().highLight.highlighted = false;
            //nextHandImage.GetComponent<HandImage>().highLight.enabled = false;
        }

        Invoke("DisableHighlight", 0.1f);

        cameraTransform = Camera.main.transform;
        handPos = transform.Find("HandPos");
        handPos.parent = null;
        //highLight.enabled = true;
        //highLight.highlighted = true;
        im = transform.GetComponentInChildren<Image>();
        im.fillAmount = 0f;
    }

    private void DisableHighlight()
    {
        if (nextHandImage != null)
        {
            nextHandImage.SetActive(false);
            nextHandImage.GetComponent<HandImage>().enabled = false;
            //nextHandImage.GetComponent<HandImage>().highLight.highlighted = false;
            //nextHandImage.GetComponent<HandImage>().highLight.enabled = false;
        }
    }

    private Transform cameraTransform;

    private void LateUpdate()
    {
        transform.LookAt(cameraTransform);
        transform.Rotate(Vector3.up, 180f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("HAND"))
        {
            other.GetComponent<HandMoving>().canMove = false;
            StartCoroutine(FillingUp());

            GetComponent<Collider>().enabled = false;
        }
    }

    public string nameOfHAndAnimToPlay;
    //public HighlightEffect highLight;

    IEnumerator FillingUp()
    {
        Transform hand = FindObjectOfType<HandMoving>().transform;
        Transform target = handPos;

        while (im.fillAmount < 1f)
        {
            im.fillAmount += 1.2f * Time.deltaTime;

            hand.position = Vector3.MoveTowards(hand.position, target.position, 0.7f * Time.deltaTime);
            hand.rotation = Quaternion.LerpUnclamped(hand.rotation, target.rotation, 1.8f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        if (nameOfHAndAnimToPlay == "CupPush" || nameOfHAndAnimToPlay == "FountainChallenge")
        {
            FindObjectOfType<HandHolder>().EnableCloseCam();
            FindObjectOfType<HandHolder>().EnableShower();

            if (nameOfHAndAnimToPlay == "CupPush")
                FindObjectOfType<HalfColaHalfWater>().StartPouring();
        }

        FindObjectOfType<HandHolder>().GetComponent<Animation>().Play(nameOfHAndAnimToPlay);

        hand.position = target.position;
        hand.rotation = target.rotation;

        if (nextHandImage != null)
        {
            nextHandImage.SetActive(true);
            nextHandImage.GetComponent<HandImage>().enabled = true;
            //nextHandImage.GetComponent<HandImage>().highLight.enabled = true;
        }
        gameObject.SetActive(false);

        //highLight.highlighted = false;
    }
}

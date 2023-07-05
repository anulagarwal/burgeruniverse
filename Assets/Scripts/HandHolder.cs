using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using HighlightPlus;

public class HandHolder : MonoBehaviour
{
    public GameObject foamSpheres, foamMiddle, foamDown;
    public Transform parentOfGrabbedObject, cup;

    private void ActivateFoams()
    {
        foamSpheres.SetActive(true);
        //FindObjectOfType<HandGirlManager>().Angry();
    }

    private void ActivateFoamMiddle()
    {
        foamMiddle.SetActive(true);
        //FindObjectOfType<HandGirlManager>().Angry();
    }

    private void ActivateFoamDown()
    {
        foamDown.SetActive(true);
        FindObjectOfType<HandGirlManager>().Angry();
    }

    //public Obi.ObiEmitter emitter;
    public MeshRenderer waterRend;
    public Material transparentWaterMat;
    private void PourWater()
    {
        //emitter.enabled = true;
        waterRend.materials[1] = waterRend.materials[2] = transparentWaterMat;

        Invoke("InvokeMelting", 0.4f);
    }

    private void InvokeMelting()
    {
        foreach (FoamSphere foam in FindObjectsOfType<FoamSphere>())
        {
            foam.Melt();
        }
    }

    private void HoldWater()
    {
        GetComponent<Animation>().Play("HandWaterHold");
    }

    private void UseWaterNo()
    {
        GetComponent<Animation>().Play("HandWaterUse");
    }

    private void CharacterWaterReaction()
    {
        FindObjectOfType<HandGirlManager>().ReactToWater();
    }

    private void GetPepper()
    {
        FindObjectOfType<TargetYesOrNo>().GetPepper();
    }

    private void HandPepperIn()
    {
        FindObjectOfType<HandGirlManager>().GetComponentInChildren<Animator>().SetBool("angry", false);
        GetComponent<Animation>().Play("HandPepperIn");

        FindObjectOfType<TargetYesOrNo>().ButtonChooseIdleInvoke();

    }

    private void HandPepperHold()
    {
        GetComponent<Animation>().Play("HandPepperHold");
    }

    private void HandPepperUse()
    {
        GetComponent<Animation>().Play("HandPepperHold");

    }

    private void HandCanPlay()
    {
        GetComponent<Animation>().Play("HandSprayCan");
    }

    private void GrabCup()
    {
        cup.parent = parentOfGrabbedObject;
        cup.transform.localPosition = parentOfGrabbedObject.Find("ColaTransform").localPosition;
        cup.transform.localRotation = parentOfGrabbedObject.Find("ColaTransform").localRotation;
        //cup.GetComponent<HighlightEffect>().highlighted = false;
    }

    private void CanMoveHand()
    {
        transform.GetComponentInParent<HandMoving>().MoveBack();
    }

    public ParticleSystem pourParticle;

    private void PourCola()
    {
        //pourParticle.Play();
        shower.GetComponent<ShowWaterLevel>().canMove = true;
    }

    public Transform roseToGrab;
    private void GrabRose()
    {
        roseToGrab.parent = parentOfGrabbedObject;
        //roseToGrab.GetComponent<HighlightEffect>().highlighted = false;
    }

    public GameObject handPourTut;

    private void DisableHandPourTut()
    {
        handPourTut.SetActive(false);
    }

    private void WinHand()
    {

    }

    public Transform chairToGrab;

    private void GrabChair()
    {
        chairToGrab.parent = parentOfGrabbedObject;
    }

    private void ReleaseChair()
    {
        chairToGrab.parent = null;

        MakeGirlSit();
    }

    public GirlAskForFoodHolder girlRestaurant;
    private void GirlSit()
    {
        girlRestaurant.GoToChair();
    }

    private void MakeGirlSit()
    {
        girlRestaurant.SitDown();
    }

    public Animation roseTargetAnim;
    private void HandOverRose()
    {
        //FindObjectOfType<HandGirlManager>().GetRose();
        Debug.Log("PLAYINGANIM");
        roseTargetAnim.Play();
    }

    public void EnableCloseCam()
    {
        Invoke("DisableHandPourTut", 3f);
        handPourTut.SetActive(true);
        transform.parent.Find("FollowHandCamClose").gameObject.SetActive(true);
    }

    public void DisableCloseCam()
    {
        transform.parent.Find("FollowHandCamClose").gameObject.SetActive(false);
    }

    private void StopPouringCola()
    {
        //pourParticle.Stop();
        shower.GetComponent<ShowWaterLevel>().canMove = false;
    }

    public GameObject shower;

    public void EnableShower()
    {
        shower.SetActive(true);
    }

    private int index = 1;

    public ParticleSystem[] pourParticles;
    private int helpIndex = 0;
    private void FountainStop()
    {
        pourParticles[helpIndex].Stop();
    }

    private void FountainPlay()
    {
        helpIndex++;
        pourParticles[helpIndex].Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (index == 0)
            {
                GetComponent<Animation>().Play("CupColaBack");
                //GetComponent<Animation>().Play("CupPush");
                index = 1;
            }
            else
            {
                GetComponent<Animation>().Play("CupCola");
                //GetComponent<Animation>().Play("CupBack");
                index = 0;
            }
        }
    }
}

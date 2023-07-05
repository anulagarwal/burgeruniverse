using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
//using Obi;

public class RestaurantGirl : MonoBehaviour
{
    private GameObject sight;
    public Rig rig;

    //-----STALKER
    public ParticleSystem heartParticles;
    public Animation animOfProgBar;
    public GameObject camGame, tutBeginning;
    public TwoBoneIKConstraint handLeft, handRight;

    private void EnableHeartParticles(bool canEnable = true)
    {
        if (canEnable)
        {
            heartParticles.Play();
            animOfProgBar.Play("BarDown");
        }
        else
        {
            heartParticles.Stop();
            animOfProgBar.Play("BarUp");
        }
    }

    private void StartGame()
    {
        camGame.SetActive(true);
        tutBeginning.SetActive(true);
        Invoke("StartLooking", Random.Range(2f, 5f));
    }

    //-----ENDSTALKER

    void Start()
    {
        sight = GameObject.FindGameObjectWithTag("Sight");
        sight.SetActive(false);
        if (heartParticles != null)
            Invoke("StartGame", 2f);
        else
            Invoke("StartLooking", Random.Range(2f, 5f));
    }

    public MultiAimConstraint headConstraint;
    public float headUpSpeed = 10f;

    public Animation warning;

    public void StartLooking()
    {
        warning.Play();
        StartCoroutine(HeadUp());

        Invoke("StopLooking", Random.Range(2f, 5f));

        if (heartParticles != null)
            EnableHeartParticles(false);
        if (heartParticles != null)
        {
            GetComponent<Animator>().SetBool("looking", true);

            GetComponent<Animator>().SetLayerWeight(1, 0f);
            GetComponent<Animator>().SetLayerWeight(2, 1f);

            handLeft.GetComponent<Animation>().Play("WeightUp");
            handRight.GetComponent<Animation>().Play("WeightUp");
            //handLeft.weight = handRight.weight = 1f;
        }
    }

    //public ObiEmitter wineEmitter;

    public GameObject phone, wine;

    private void DisablePhone()
    {
        phone.SetActive(false);
        sight.SetActive(false);
        sight.GetComponent<MeshRenderer>().enabled = false;
    }

    private void SetWineAsChild()
    {
        wine.transform.parent = phone.transform.parent;
    }

    public GameObject talkbubble;
    private void DisableTalkBubble()
    {
        talkbubble.SetActive(false);
    }

    private void EmitWine()
    {
        //wineEmitter.enabled = true;
        loseCamera.SetActive(true);
    }

    public GameObject powParticle;
    private void Slap()
    {
        powParticle.SetActive(true);
        FindObjectOfType<Detective>().EnableRagdoll();
    }

    public GameObject loseCamera, angryEmoji;

    public void GameFailed()
    {
        rig.weight = 0f;
        GetComponent<Animator>().avatar = null;
        GetComponent<Animator>().enabled = false;
        GetComponent<Animation>().Play("PourWineAnim");
        //transform.GetChild(0).GetComponent<Animation>().Play();
        angryEmoji.SetActive(true);
        warning.gameObject.SetActive(false);

    }

    public void GameWon()
    {
        StopAllCoroutines();
        CancelInvoke("StopLooking");
        CancelInvoke("StartLooking");
        sight.SetActive(false);

        if (heartParticles != null)
        {
            heartParticles.Stop();
            animOfProgBar.Play("BarDown");
        }
    }

    public bool fasterHead = false;
    IEnumerator HeadUp()
    {
        sight.SetActive(true);
        if (fasterHead)
            headConstraint.weight = 0.2f;
        while (headConstraint.weight < 1f)
        {
            headConstraint.weight += headUpSpeed * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator HeadDown()
    {
        while (headConstraint.weight > 0f)
        {
            headConstraint.weight -= headUpSpeed * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        sight.SetActive(false);
    }

    public void StopLooking()
    {
        StartCoroutine(HeadDown());

        Invoke("StartLooking", Random.Range(2f, 5f));

        if (heartParticles != null)
            EnableHeartParticles(true);
        if (heartParticles != null)
        {
            GetComponent<Animator>().SetBool("looking", false);

            GetComponent<Animator>().SetLayerWeight(1, 1f);
            GetComponent<Animator>().SetLayerWeight(2, 0f);

            handLeft.GetComponent<Animation>().Play("WeightDown");
            handRight.GetComponent<Animation>().Play("WeightDown");
            //handLeft.weight = handRight.weight = 0f;
        }
    }
}

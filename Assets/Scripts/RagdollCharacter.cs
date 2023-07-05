using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class RagdollCharacter : MonoBehaviour
{
    public GameObject leftElbow;

    private Rigidbody leftElbowRigidBody;

    public Security tempSecu;

    void Start()
    {
        leftElbowRigidBody = leftElbow.GetComponent<Rigidbody>();
        DisableRagdoll();

        //Invoke("EnableRagdoll", 2f);
    }

    IEnumerator TakeMoney()
    {
        while (isCheating)
        {
            int countOfMinus = -5;
            transform.Find("-20Money").GetComponent<Animation>().Play();
            MoneySpawner.Instance.UpdateMoney(countOfMinus);
            transform.Find("-20Money").GetComponentInChildren<TextMeshPro>().text = countOfMinus.ToString();

            yield return new WaitForSeconds(2f);
        }
    }

    public static bool isFirst = true;

    private void DisableCam()
    {
        transform.Find("ArrowFloating").Find("CamCasino_Tut_Up").gameObject.SetActive(false);
        transform.Find("ArrowFloating").Find("CamCasino_Tut_Left").gameObject.SetActive(false);
        transform.Find("ArrowFloating").Find("CamCasino_Tut_Right").gameObject.SetActive(false);
    }

    public void Cheat()
    {
        if (isFirst)
        {
            isFirst = false;
            transform.Find("ArrowFloating").gameObject.SetActive(true);
            if (transform.parent.parent.gameObject.name.Contains("Up"))
                transform.Find("ArrowFloating").Find("CamCasino_Tut_Up").gameObject.SetActive(true);
            else if (transform.parent.parent.gameObject.name.Contains("Left"))
                transform.Find("ArrowFloating").Find("CamCasino_Tut_Left").gameObject.SetActive(true);
            else
                transform.Find("ArrowFloating").Find("CamCasino_Tut_Right").gameObject.SetActive(true);

            Invoke("DisableCam", 2f);
        }

        isCheating = true;
        GetComponent<Animator>().SetBool("play", true);
        StartCoroutine(TakeMoney());
        if (FindObjectsOfType<Security>() != null)
            TryToFindSecurity();
    }

    private void TryToFindSecurity()
    {
        if (!FindObjectsOfType<Security>()[Random.Range(0, FindObjectsOfType<Security>().Length)].GoToCheater(transform.position, this))
        {
            Invoke("TryToFindSecurity", 1f);
        }
    }

    public void DisableRagdoll()
    {
        transform.GetComponentInParent<NavMeshAgent>().enabled = true;
        transform.GetComponentInParent<Rigidbody>().isKinematic = false;
        transform.GetComponentInParent<Collider>().enabled = true;
        GetComponent<Animator>().enabled = true;
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    [HideInInspector] public bool isCheating = false;

    public void EnableRagdoll(Transform posOfHand, Security secuColl = null)
    {
        if (!isCheating)
            return;

        transform.parent.parent.parent.GetComponentInChildren<SpawnMoneyPos>().playersAtTable--;

        if (secuColl != tempSecu)
            tempSecu.MoveBackToStart();

        isCheating = false;

        GameObject tempParent = transform.parent.gameObject;
        transform.parent = null;
        Destroy(tempParent);

        transform.Find("AngryEmoji").gameObject.SetActive(false);
        transform.Find("-20Money").gameObject.SetActive(false);

        leftElbow.transform.rotation = posOfHand.rotation;
        transform.position = new Vector3(transform.position.x, .7f, transform.position.z);


        //transform.GetComponentInParent<NavMeshAgent>().enabled = false;
        //transform.GetComponentInParent<Rigidbody>().isKinematic = true;
        //transform.GetComponentInParent<Collider>().enabled = false;


        GetComponent<Animator>().enabled = false;
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.gameObject.GetComponent<Collider>().enabled = true;
        }

        handFollowTransform = posOfHand;

        leftElbowRigidBody.isKinematic = true;

        isTouchingArm = true;

        //transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);

    }

    [HideInInspector] public bool isTouchingArm = false;
    private Transform handFollowTransform;

    public void Release()
    {
        isTouchingArm = false;
        leftElbowRigidBody.isKinematic = false;

        Invoke("DisableColliders", 0.7f);

        //FindObjectOfType<AiSpawner>().SpawnAI();
    }

    private void DisableColliders()
    {
        foreach (Collider coll in GetComponentsInChildren<Collider>())
            coll.enabled = false;
        Destroy(gameObject, 2f);
    }

    void FixedUpdate()
    {
        if (isTouchingArm)
        {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
                rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -5, 5), Mathf.Clamp(rb.velocity.y, -5, 5), Mathf.Clamp(rb.velocity.z, -5, 5));

            leftElbowRigidBody.MovePosition(handFollowTransform.position);


            //leftElbow.transform.position = handFollowTransform.position;

            //leftElbow.transform.rotation = handFollowTransform.rotation;

            //leftElbow.transform.parent.GetComponent<Rigidbody>().MovePosition(handFollowTransform.GetChild(0).position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Security : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform exit;
    private Animator animator;

    private Vector3 startPos;
    private Quaternion startRotation;

    private bool hasCheater = false;

    public GameObject newTargetOfHandHold;

    private RagdollCharacter cheaterRagdoll;

    private void OnEnable()
    {
        if (PlayerPrefs.GetFloat("SecuritySpeed", 0f) != 0f)
            GetComponentInChildren<NavMeshAgent>().speed = PlayerPrefs.GetFloat("SecuritySpeed");
    }

    public void MoveTo(Vector3 targetPos)
    {
        StopAllCoroutines();

        canMove = true;
        agent.SetDestination(targetPos);
        //agent.SetDestination(new Vector3(Random.Range(-2f, 2f), transform.position.y, Random.Range(-2f, 2f)));
    }

    private void RealThrow()
    {
        Invoke("MoveBackToStart", 1f);

        GameObject obj1 = Instantiate(newTargetOfHandHold, transform.Find("TargetOfHandHold"));
        obj1.transform.localPosition = Vector3.zero;
        obj1.transform.localRotation = Quaternion.identity;
        obj1.transform.localScale = Vector3.one;
        obj1.transform.parent = obj1.transform.parent.parent;

        transform.Find("TargetOfHandHold").gameObject.AddComponent<ThrowAwayTargetOfHandHold>();

        obj1.name = "TargetOfHandHold";

        Invoke("ReleaseCheater", 0.3f);
    }

    public void ThrowCheater()
    {
        if (!hasCheater)
            return;


        hasCheater = false;

        canMove = false;
        agent.SetDestination(transform.position);
        //agent.isStopped = true;
        animator.SetBool("walk", false);

        RigOff();

        animator.SetBool("throw", true);


        Invoke("RealThrow", 0.5f);
    }

    public void RigOn()
    {
        transform.Find("Secu").Find("Rig 1").GetComponent<Animation>().Play("RigWeightUp");
    }

    public void RigOff()
    {
        transform.Find("Secu").Find("Rig 1").GetComponent<Animation>().Play("RigWeightDown");
    }

    private void ReleaseCheater()
    {
        cheaterRagdoll.Release();
    }

    void Start()
    {
        startRotation = transform.rotation;
        startPos = transform.position;
        animator = GetComponentInChildren<Animator>();
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        agent = GetComponent<NavMeshAgent>();

        //MoveTo(exit.position);
        //Invoke("FindSeat", Random.Range(0f, 2f));
    }

    private bool canGoToCheater = true;


    IEnumerator CheckIfThereIsStillCheater()
    {
        while (tempRagd.isCheating)
        {

            yield return new WaitForSeconds(0.2f);
        }

        animator.SetBool("walk", false);
        agent.SetDestination(transform.position);
        MoveBackToStart();
        StopAllCoroutines();
    }

    private RagdollCharacter tempRagd;

    public bool GoToCheater(Vector3 pos, RagdollCharacter ragd)
    {
        bool isGoing = false;
        if (canGoToCheater)
        {
            tempRagd = ragd;

            isGoing = true;
            canGoToCheater = false;

            ragd.tempSecu = this;

            //StartCoroutine(CheckIfThereIsStillCheater());

            MoveTo(pos);
        }
        return isGoing;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Cheater"))
        {
            cheaterRagdoll = other.GetComponentInChildren<RagdollCharacter>();
            if (!cheaterRagdoll.isCheating)
                return;

            RigOn();

            hasCheater = true;
            cheaterRagdoll.EnableRagdoll(transform.Find("TargetOfHandHold"), this);
            MoveTo(exit.position);
        }

    }

    [HideInInspector] public bool canMove = false;

    void Update()
    {
        if (canMove)
        {
            if (agent.remainingDistance > 0.01f)
            {
                if (!animator.GetBool("walk"))
                {
                    animator.SetBool("walk", true);

                    agent.isStopped = false;
                }
            }
            else
            {
                if (animator.GetBool("walk"))
                {
                    animator.SetBool("walk", false);
                    canMove = false;

                    agent.isStopped = true;

                    if (Vector3.Distance(startPos, transform.position) < 0.1f)
                        StartCoroutine(RotateToStartRot());
                }
            }
        }
    }

    IEnumerator RotateToStartRot()
    {
        while (Mathf.Abs(transform.localRotation.eulerAngles.y) > 0.5f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, 3f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        transform.localRotation = Quaternion.identity;
    }

    public void MoveBackToStart()
    {
        canGoToCheater = true;
        //MoveTo(Vector3.zero);
        MoveTo(startPos);
    }
}

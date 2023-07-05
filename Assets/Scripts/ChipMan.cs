using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChipMan : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform exit;
    private Animator animator;

    public Transform chips;


    private void CheckShelfs()
    {
        if (GameObject.FindGameObjectsWithTag("Chips").Length == 1)
            PlayerPrefs.SetInt("Shelf0", 1);
        else if (GameObject.FindGameObjectsWithTag("Chips").Length == 2)
            PlayerPrefs.SetInt("Shelf1", 1);
        else if (GameObject.FindGameObjectsWithTag("Chips").Length == 3)
            PlayerPrefs.SetInt("Shelf2", 1);

        FindObjectOfType<EnableShelfs>().CheckWhatToEnable();
    }


    private void OnEnable()
    {
        if (gameObject.name.Contains("DELIVER"))
            return;

        if (gameObject.name.Contains("CHEF"))
        {
            Invoke("CheckShelfs", 0.06f);

            if (PlayerPrefs.GetInt("ChefCapacity", 0) != 0)
                GetComponentInChildren<FoodHolderPlayers>().capacity = PlayerPrefs.GetInt("ChefCapacity");
            return;
        }
        else if (gameObject.name.Contains("WAITER"))
        {
            if (PlayerPrefs.GetFloat("ChipManSpeed", 0f) != 0f)
                GetComponentInChildren<NavMeshAgent>().speed = PlayerPrefs.GetFloat("ChipManSpeed");
            if (PlayerPrefs.GetInt("ChipManCapacity", 0) != 0)
                GetComponentInChildren<FoodHolderPlayers>().capacity = PlayerPrefs.GetInt("ChipManCapacity");
            if (PlayerPrefs.GetFloat("ChipManSpeed", 0f) != 0f)
                GetComponentInChildren<NavMeshAgent>().speed = PlayerPrefs.GetFloat("ChipManSpeed");
            return;
        }
    }

    Collider coll;
    public void KeepResetingCollider()
    {
        if (coll.enabled)
        {
            coll.enabled = false;
            Invoke("KeepResetingCollider", 0.15f);
        }
        else
        {
            coll.enabled = true;
            Invoke("KeepResetingCollider", 0.45f);
        }
    }

    IEnumerator Start()
    {
        coll = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        agent = GetComponent<NavMeshAgent>();

        yield return new WaitForSeconds(0.05f);


        if (!gameObject.name.Contains("DELIVERY"))
        {
            chips = GameObject.FindGameObjectsWithTag("Chips")[Random.Range(0, GameObject.FindGameObjectsWithTag("Chips").Length)].transform;
            if (!isRandom)
                chips = GameObject.FindGameObjectsWithTag("Chips")[0].transform;
        }


        if (gameObject.name.Contains("WAITER"))
        {
            KeepResetingCollider();
            if (gameObject.name.Contains("DELIVERY"))
            {
                //chips = GameObject.FindGameObjectWithTag("DeliveryPos").transform;
            }
            else
                chips = GameObject.FindGameObjectsWithTag("BoxesUp")[Random.Range(0, GameObject.FindGameObjectsWithTag("BoxesUp").Length)].transform;
        }

        GetChips();
    }

    bool hasChips = false;
    [HideInInspector] public int moneyToSpawn = 0;


    IEnumerator OnVespa()
    {
        while (true)
        {
            transform.localPosition = new Vector3(0f, transform.position.y, 0f);
            transform.localRotation = Quaternion.identity;
            yield return new WaitForEndOfFrame();
        }
    }

    private void VespaBack()
    {
        transform.localPosition = new Vector3(0f, transform.position.y, 0f);
        transform.localRotation = Quaternion.identity;

        moneyToSpawn = transform.parent.parent.GetComponentInChildren<PutDownBag>().DestroyFoods();

        if (transform.parent.GetComponentInParent<Animation>() != null)
            transform.parent.GetComponentInParent<Animation>().Play("VespaAnim2");

        Invoke("GetOffVespa", 3.4f);
    }

    private void GetOffVespa()
    {
        transform.localPosition = new Vector3(0f, transform.position.y, 0f);
        transform.localRotation = Quaternion.identity;
        StopAllCoroutines();

        animator.SetBool("motor", false);
        animator.SetBool("motorOff", true);

        transform.parent = null;
        KeepResetingCollider();

        Invoke("GoToPay", 1.8f);
    }

    private void GoToPay()
    {
        GetChips();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("ChipSpawner"))
        {
            Debug.Log("WAITER" + agent.isStopped);
            if (hasChips && !gameObject.name.Contains("WAITER"))
                return;

            if (gameObject.name.Contains("WAITER") && animator.GetBool("walk"))
                return;

            hasChips = true;

            if (IsInvoking("DropChipsAtTable") || IsInvoking("GetChips"))
                return;

            agent.SetDestination(transform.position);
            canMove = false;
            agent.isStopped = true;
            animator.SetBool("walk", false);

            RigOn_Chips();

            if (gameObject.name.Contains("WAITER"))
            {
                GetComponent<Collider>().enabled = false;
            }
        }
        if (other.gameObject.name.Contains("PutDown") && (gameObject.name.Contains("CHEF") || gameObject.name.Contains("DELIVERY")))
        {
            if (GetComponentInChildren<FoodBoxPrefab>() != null)
                hasChips = true;

            if (!hasChips)
                return;

            Debug.Log(agent.remainingDistance);

            if (agent.remainingDistance > 1f)
                return;

            hasChips = false;

            if (IsInvoking("DropChipsAtTable") || IsInvoking("GetChips"))
                return;

            agent.SetDestination(transform.position);
            canMove = false;
            agent.isStopped = true;
            animator.SetBool("walk", false);

            if (gameObject.name.Contains("CHEF"))
                Invoke("GetChips", Random.Range(0.5f, 1f));
            else
            {
                CancelInvoke("KeepResetingCollider");
                GetComponent<Collider>().enabled = false;

                transform.parent = vespaTransf;
                transform.localPosition = new Vector3(0f, transform.position.y, 0f);
                transform.localRotation = Quaternion.identity;


                agent.SetDestination(transform.position);
                agent.isStopped = true;
                StopAllCoroutines();
                StartCoroutine(OnVespa());

                if (vespaTransf.GetComponentInParent<Animation>() != null)
                    vespaTransf.GetComponentInParent<Animation>().Play();
                animator.SetBool("motorOff", false);
                animator.SetBool("motor", true);

                Invoke("VespaBack", Random.Range(6f, 10f));
            }

            RigOff_Chips();

            return;
        }
        if (other.gameObject.name.Contains("PokerTable") && agent != null && (agent.remainingDistance < 1.2f))
        {
            if (!hasChips)
                return;


            if (gameObject.name.Contains("WAITER"))
            {
                RigOff_Chips();
            }


            Debug.Log(agent.remainingDistance);

            if (agent.remainingDistance > 1.2f)
                return;

            hasChips = false;

            if (IsInvoking("DropChipsAtTable") || IsInvoking("GetChips"))
                return;

            agent.SetDestination(transform.position);
            canMove = false;
            agent.isStopped = true;
            animator.SetBool("walk", false);
            Invoke("GetChips", Random.Range(0.5f, 1f));

            if (other.GetComponentInChildren<PokerTableChips>() != null)
                other.GetComponentInChildren<PokerTableChips>().MoveAllChipsToTable(transform);

            RigOff_Chips();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canMove)
            return;

        if (other.gameObject.name.Contains("ChipSpawner"))
        {
            //if (other.GetComponentInChildren<ChipSpawnerMachine>() != null)
        }
        if (other.gameObject.name.Contains("PokerTable"))
        {
            //PlayerChipsHolder.Instance.StopAllCoroutines();
        }
    }

    public Animation rightHandChip, leftHandChip;

    private bool isRigOn = false;

    private Vector3 crashedPos;

    public void RigOn_Chips(bool isMovingBox = false)
    {
        if (!isRigOn)
        {
            isRigOn = true;

            if (!IsInvoking("HasWaiterCrashed"))
            {
                crashedPos = transform.position;
                Invoke("HasWaiterCrashed", 3f);
            }

            rightHandChip.Play("IK_Up");
            leftHandChip.Play("IK_Up");
        }
    }

    private void HasWaiterCrashed()
    {
        if (!gameObject.name.Contains("WAITER"))
            return;
        if (gameObject.name.Contains("DELIVERY"))
            return;

        if (Vector3.Distance(transform.position, crashedPos) < 0.5f)
        {
            //CRASHED

            Transform newEnabledWaiter = transform.parent.GetChild(transform.parent.childCount - 1);
            newEnabledWaiter.SetSiblingIndex(0);
            newEnabledWaiter.transform.position = transform.position;
            newEnabledWaiter.gameObject.SetActive(true);

            Destroy(gameObject);
        }
        else
        {
            crashedPos = transform.position;
            Invoke("HasWaiterCrashed", 3f);
        }
    }

    public void RigOff_Chips()
    {
        if (isRigOn)
        {
            isRigOn = false;

            rightHandChip.Play("IK_Down");
            leftHandChip.Play("IK_Down");
        }
    }

    public bool isRandom = true;
    int tempChips = -1;

    public void GetChips()
    {
        agent.isStopped = false;

        if (gameObject.name.Contains("CHEF"))
        {
            chips = GameObject.FindGameObjectsWithTag("Chips")[Random.Range(0, GameObject.FindGameObjectsWithTag("Chips").Length)].transform;

            if (!isRandom)
            {
                switch (tempChips)
                {
                    case -1:
                        tempChips = 0;
                        break;
                    case 0:
                        tempChips = 1;
                        break;
                    case 1:
                        tempChips = 2;
                        break;
                    case 2:
                        tempChips = 0;
                        break;
                }
                chips = GameObject.FindGameObjectsWithTag("Chips")[tempChips].transform;
            }
        }

        if (gameObject.name.Contains("WAITER"))
        {
            if (gameObject.name.Contains("DELIVERY"))
            {
                //chips = GameObject.FindGameObjectWithTag("DeliveryPos").transform;
            }
            else
                chips = GameObject.FindGameObjectsWithTag("BoxesUp")[Random.Range(0, GameObject.FindGameObjectsWithTag("BoxesUp").Length)].transform;
        }
        MoveTo(chips.position);
    }

    public void InvokeDropChipsAtTable(float time)
    {
        if (!IsInvoking("DropChipsAtTable"))
            Invoke("DropChipsAtTable", time);
    }

    public Transform vespaTransf;

    public Transform[] foodShelfs;

    public void DropChipsAtTable()
    {
        agent.isStopped = false;

        if (gameObject.name.Contains("CHEF"))
        {
            int shelfIndex = 0;

            if (GetComponentInChildren<FoodBoxPrefab>() != null)
            {
                if (GetComponentInChildren<FoodBoxPrefab>().gameObject.name.Contains("1"))
                    shelfIndex = 1;
                else if (GetComponentInChildren<FoodBoxPrefab>().gameObject.name.Contains("2"))
                    shelfIndex = 2;
            }

            randomTablePos = foodShelfs[shelfIndex].transform.position;
        }
        else if (gameObject.name.Contains("WAITER"))
        {
            if (gameObject.name.Contains("DELIVERY"))
            {
                MoveTo(vespaTransf.position);

                return;
            }

            McTable randomTable = FindObjectsOfType<McTable>()[Random.Range(0, FindObjectsOfType<McTable>().Length)];
            randomTablePos = randomTable.transform.position;

            if (randomTable.DoesNeedFood())
            {

            }
            else
            {
                Invoke("DropChipsAtTable", 0.1f);
                return;
            }
        }
        else
        {
            randomTablePos = FindObjectsOfType<PokerTable>()[Random.Range(0, FindObjectsOfType<PokerTable>().Length)].transform.position;
        }

        MoveTo(randomTablePos);
    }

    Vector3 randomTablePos;

    public void FindSeat()
    {
        if (transform.parent != null)
            return;

        foreach (GameObject seat in GameObject.FindGameObjectsWithTag("Seat"))
        {
            if (seat.transform.childCount == 0)
            {
                transform.parent = seat.transform;
                MoveTo(seat.transform.position);
                return;
            }
        }
    }

    public void MoveTo(Vector3 targetPos)
    {
        StopAllCoroutines();

        canMove = true;
        agent.SetDestination(targetPos);
    }

    [HideInInspector] public bool canMove = false;


    IEnumerator RotateToParent()
    {
        while (Mathf.Abs(transform.localRotation.eulerAngles.y) > 0.5f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, 3f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        transform.localRotation = Quaternion.identity;
    }

    public void GetAngry()
    {
        animator.SetBool("angry", true);
        transform/*.GetChild(0)*/.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void StopAngry()
    {
        animator.SetBool("angry", false);
        transform/*.GetChild(0)*/.GetComponentInChildren<ParticleSystem>().Stop();
    }

    private void CheckIfTablekHasChips()
    {
        if (GetComponentInParent<PokerTable>().GetComponentInChildren<PokerTableChips>().hasChips)
        {

        }
        else
            GetAngry();
    }

    void Update()
    {

        if (canMove)
        {
            if (agent.remainingDistance > 0.1f)
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

                    if (gameObject.name.Contains("WAITER"))
                    {
                        agent.isStopped = true;
                        //GetChips();
                    }
                }
            }
        }
    }
}

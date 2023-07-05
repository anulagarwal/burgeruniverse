using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter_Mc : MonoBehaviour
{
    private NavMeshAgent agent;
    private Collider collOfFoodsHolder;
    private FoodHolderPlayers foodsHolder;
    private Animator animator;

    void Start()
    {
        choosenShelf = transform;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        foodsHolder = GetComponentInChildren<FoodHolderPlayers>();
        collOfFoodsHolder = foodsHolder.GetComponent<Collider>();

        GetRandomFoods();

        if (PlayerPrefs.GetFloat("ChipManSpeed", 0f) != 0f)
            GetComponentInChildren<NavMeshAgent>().speed = PlayerPrefs.GetFloat("ChipManSpeed");
        if (PlayerPrefs.GetInt("ChipManCapacity", 0) != 0)
            GetComponentInChildren<FoodHolderPlayers>().capacity = PlayerPrefs.GetInt("ChipManCapacity");

    }

    public void MoveTo(Transform target)
    {
        agent.SetDestination(target.position);
        agent.isStopped = false;
        animator.SetBool("walk", true);
    }

    public void StopAgent()
    {
        agent.isStopped = true;
        animator.SetBool("walk", false);
    }

    #region Decisions

    private Transform choosenTable, choosenShelf;

    private bool isGoingToGetFood = false;

    IEnumerator ChooseTable()
    {
        while (!choosenTable.GetComponent<McTable>().DoesNeedFood())
        {
            int randomTableIndex = Random.Range(0, FindObjectsOfType<McTable>().Length);

            choosenTable = FindObjectsOfType<McTable>()[randomTableIndex].transform;

            yield return new WaitForSeconds(0.5f);
        }

        MoveTo(choosenTable);

        CancelInvoke("CheckIfBuggy");
        Invoke("CheckIfBuggy", buggyCheckTime);
    }

    public void GoToRandomTable(bool doAnyways = false)
    {
        if (isGoingToGetFood || doAnyways)
        {
            isGoingToGetFood = false;

            StopAllCoroutines();
            startedToLookForAnotherShelf = false;

            collOfFoodsHolder.enabled = false;
            foodsHolder.enabled = false;

            int randomTableIndex = Random.Range(0, FindObjectsOfType<McTable>().Length);

            choosenTable = FindObjectsOfType<McTable>()[randomTableIndex].transform;

            if (!choosenTable.GetComponent<McTable>().DoesNeedFood())
            {
                StartCoroutine(ChooseTable());
            }
            else
            {
                MoveTo(choosenTable);
            }

            //RENAME FOODS
            foreach (FoodBoxPrefab boxPref in GetComponentsInChildren<FoodBoxPrefab>())
                boxPref.gameObject.name = "Box012";
        }

        CancelInvoke("CheckIfBuggy");
        Invoke("CheckIfBuggy", buggyCheckTime);
    }

    void CheckIfBuggy()
    {
        Transform latestWaiter = transform.parent.GetChild(transform.childCount - 1);
        latestWaiter.transform.SetSiblingIndex(0);
        latestWaiter.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    float buggyCheckTime = 70f;

    public void GetRandomFoods(bool canChange = false)
    {
        if (!isGoingToGetFood || canChange)
        {
            if (GetComponentInChildren<FoodBoxPrefab>() != null)
            {
                GoToRandomTable(true);
                return;
            }

            isGoingToGetFood = true;

            collOfFoodsHolder.enabled = false;
            foodsHolder.enabled = false;

            int randomShelfIndex;

            do
            {
                randomShelfIndex = Random.Range(0, GameObject.FindGameObjectsWithTag("BoxesUp").Length);
                if (GameObject.FindGameObjectsWithTag("BoxesUp").Length == 1)
                    break;
            } while (choosenShelf.GetInstanceID() == GameObject.FindGameObjectsWithTag("BoxesUp")[randomShelfIndex].transform.GetInstanceID());

            choosenShelf = GameObject.FindGameObjectsWithTag("BoxesUp")[randomShelfIndex].transform;

            MoveTo(choosenShelf);

            CancelInvoke("CheckIfBuggy");
            Invoke("CheckIfBuggy", buggyCheckTime);
        }
    }


    IEnumerator ChooseAnotherShelf()
    {
        while (isGoingToGetFood)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            if (isGoingToGetFood)
                GetRandomFoods(true);
        }

        CancelInvoke("CheckIfBuggy");
        Invoke("CheckIfBuggy", buggyCheckTime);
    }

    #endregion

    #region Hand Anims

    public Animation rightHandChip, leftHandChip;

    private bool isRigOn = false;

    public void RigOn_Chips()
    {
        if (!isRigOn)
        {
            isRigOn = true;
            rightHandChip.Play("IK_Up");
            leftHandChip.Play("IK_Up");
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

    #endregion


    private bool startedToLookForAnotherShelf = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<McTable>() != null)
        {
            if (!isGoingToGetFood)
            {
                if (other.GetComponentInChildren<McTable>().transform.GetInstanceID() == choosenTable.GetInstanceID())
                {
                    //CAN DROP FOODS
                    collOfFoodsHolder.enabled = true;
                    foodsHolder.enabled = true;

                    StopAgent();
                }
            }
        }

        if (other.gameObject.name.Contains("WaiterStop"))
        {
            if (isGoingToGetFood)
            {
                if (other.transform.parent.GetComponentInChildren<FoodUpShelf>().transform.GetInstanceID() == choosenShelf.GetInstanceID())
                {
                    //CAN GET FOODS
                    collOfFoodsHolder.enabled = true;
                    foodsHolder.enabled = true;

                    StopAgent();

                    if (!startedToLookForAnotherShelf)
                    {
                        startedToLookForAnotherShelf = true;
                        StartCoroutine(ChooseAnotherShelf());
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter_Drinks : MonoBehaviour
{
    private NavMeshAgent agent;
    private Collider collOfFoodsHolder;
    private FoodHolderPlayers foodsHolder;
    private Animator animator;

    public Material normalMat;

    private GameObject tempParent;

    void Start()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = normalMat;
        transform.parent.GetChild(0).gameObject.SetActive(false);
        transform.parent.GetChild(1).gameObject.SetActive(false);
        transform.parent.GetChild(2).gameObject.SetActive(true);
        transform.parent.GetChild(2).GetComponent<ParticleSystem>().Play();
        tempParent = transform.parent.gameObject;
        transform.parent = null;
        Destroy(tempParent, 3f);

        choosenShelf = transform;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        foodsHolder = GetComponentInChildren<FoodHolderPlayers>();
        collOfFoodsHolder = foodsHolder.GetComponent<Collider>();

        GetDrink();


        if (PlayerPrefs.GetFloat("ChipManSpeed", 0f) != 0f)
            GetComponentInChildren<NavMeshAgent>().speed = PlayerPrefs.GetFloat("ChipManSpeed");
        if (PlayerPrefs.GetInt("ChipManCapacity", 0) != 0)
            GetComponentInChildren<FoodHolderPlayers>().capacity = PlayerPrefs.GetInt("ChipManCapacity");

        Debug.Log(PlayerPrefs.GetInt("ChipManCapacity") + "_WAITER_CAPACITY");
    }

    public void MoveTo(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
            return;

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

            //do
            //{
            choosenTable = FindObjectsOfType<McTable>()[randomTableIndex].transform;
            //} while (choosenTable.GetComponent<FaszomPincerJohet>().isVIP);


            yield return new WaitForSeconds(0.5f);
        }

        MoveTo(choosenTable);

        CancelInvoke("CheckIfBuggy");
        Invoke("CheckIfBuggy", buggyCheckTime);
    }

    public void GoPutDownDrinks(bool doAnyways = false)
    {
        if (isGoingToGetFood || doAnyways)
        {
            isGoingToGetFood = false;

            StopAllCoroutines();
            startedToLookForAnotherShelf = false;

            collOfFoodsHolder.enabled = false;
            foodsHolder.enabled = false;

            int randomTableIndex = Random.Range(0, FindObjectsOfType<McTable>().Length);

            //do
            //{
            choosenTable = drinksDownPos;
            //} while (choosenTable.GetComponent<FaszomPincerJohet>().isVIP);

            //if (!choosenTable.GetComponent<McTable>().DoesNeedFood())
            //{
            //    StartCoroutine(ChooseTable());
            //}
            //else
            //{
            MoveTo(choosenTable);
            //}

            //RENAME FOODS

            //foreach (FoodBoxPrefab boxPref in GetComponentsInChildren<FoodBoxPrefab>())
            //    boxPref.gameObject.name = "Box012";
        }

        CancelInvoke("CheckIfBuggy");
        Invoke("CheckIfBuggy", buggyCheckTime);
    }

    void CheckIfBuggy()
    {
        Debug.Log("BUGGY_WAITER_SPAWN_NEW_WAITER");
        Transform latestWaiter = transform.parent.GetChild(transform.childCount - 1);
        latestWaiter.transform.SetSiblingIndex(0);
        latestWaiter.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    float buggyCheckTime = 70f;

    public void GetDrink(bool canChange = false)
    {
        if (!isGoingToGetFood || canChange)
        {
            if (GetComponentInChildren<FoodBoxPrefab>() != null)
            {
                GoPutDownDrinks(true);
                return;
            }

            isGoingToGetFood = true;

            collOfFoodsHolder.enabled = false;
            foodsHolder.enabled = false;

            int randomShelfIndex;

            //do
            //{
            //    randomShelfIndex = Random.Range(0, GameObject.FindGameObjectsWithTag("BoxesUp").Length);
            //    if (GameObject.FindGameObjectsWithTag("BoxesUp").Length == 1)
            //        break;
            //} while (choosenShelf.GetInstanceID() == GameObject.FindGameObjectsWithTag("BoxesUp")[randomShelfIndex].transform.GetInstanceID());

            //choosenShelf = GameObject.FindGameObjectsWithTag("BoxesUp")[randomShelfIndex].transform;

            choosenShelf = posOfDrinksToPickUp;

            MoveTo(choosenShelf);

            CancelInvoke("CheckIfBuggy");
            Invoke("CheckIfBuggy", buggyCheckTime);
        }
    }


    //IEnumerator ChooseAnotherShelf()
    //{
    //    while (isGoingToGetFood)
    //    {
    //        yield return new WaitForSeconds(Random.Range(2f, 5f));
    //        if (isGoingToGetFood)
    //            GetDrink(true);
    //    }

    //    CancelInvoke("CheckIfBuggy");
    //    Invoke("CheckIfBuggy", buggyCheckTime);
    //}

    #endregion

    #region Hand Anims

    public Animation rightHandChip, leftHandChip;

    private bool isRigOn = false;

    public void RigOn_Chips()
    {
        //rigAnim.Play("RigWeightUp");
        if (!isRigOn)
        {
            isRigOn = true;
            rightHandChip.Play("IK_Up");
            leftHandChip.Play("IK_Up");
        }
    }

    public void RigOff_Chips()
    {
        //rigAnim.Play("RigWeightDown");
        if (isRigOn)
        {
            isRigOn = false;
            rightHandChip.Play("IK_Down");
            leftHandChip.Play("IK_Down");
        }
    }

    #endregion


    private bool startedToLookForAnotherShelf = false;
    public Transform posOfDrinksToPickUp, drinksDownPos;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponentInChildren<McTable>() != null)
        //{
        //    if (!isGoingToGetFood)
        //    {
        //        if (other.GetComponentInChildren<McTable>().transform.GetInstanceID() == choosenTable.GetInstanceID())
        //        {
        //            //CAN DROP FOODS
        //            collOfFoodsHolder.enabled = true;
        //            foodsHolder.enabled = true;

        //            StopAgent();

        //            //Invoke("GetRandomFoods", 2.1f);
        //        }
        //    }
        //}

        if (other.gameObject.name.Contains("WaiterStop"))
        {
            if (isGoingToGetFood)
            {
                //if (other.transform.parent.GetComponentInChildren<FoodUpShelf>().transform.GetInstanceID() == choosenShelf.GetInstanceID())
                //{
                //CAN GET FOODS
                collOfFoodsHolder.enabled = true;
                foodsHolder.enabled = true;

                StopAgent();

                //if (!startedToLookForAnotherShelf)
                //{
                //    startedToLookForAnotherShelf = true;
                //    StartCoroutine(ChooseAnotherShelf());
                //}
                //}
            }
        }


        if (other.gameObject.name.Contains("WaiterDrinkDown"))
        {
            if (!isGoingToGetFood)
            {
                //if (other.transform.parent.GetComponentInChildren<FoodUpShelf>().transform.GetInstanceID() == choosenShelf.GetInstanceID())
                //{
                //CAN GET FOODS
                collOfFoodsHolder.enabled = true;
                foodsHolder.enabled = true;

                StopAgent();

                //if (!startedToLookForAnotherShelf)
                //{
                //    startedToLookForAnotherShelf = true;
                //    StartCoroutine(ChooseAnotherShelf());
                //}
                //}
            }
        }
    }
}

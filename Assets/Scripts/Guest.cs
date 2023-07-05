using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guest : MonoBehaviour
{
    [HideInInspector] public Transform tempCupHolder;
    public Transform cupHolderGirl, cupHolderBoy;

    public static bool isThereVIP = false;

    public Animation cupGirlAnim, cupBoyAnim;
    public GameObject[] mcChars;

    private NavMeshAgent agent;

    private Transform exit;
    private Animator animator;

    private Vector3 startPosGuest;

    public bool needFood = false;
    public int foodIndex;

    int tempLineIndex = -1;

    public void CupHolderUp()
    {
        animator.SetBool("walkDrink", true);
    }

    public void CupHolderDown()
    {
        animator.SetBool("walkDrink", false);
    }

    void Start()
    {
        //PlayerPrefs.SetInt("Poop", 1);

        if (Time.timeSinceLevelLoad < 0.4f)
            isThereVIP = false;

        needFood = false;
        animator = GetComponentInChildren<Animator>();

        #region Skin
        //SKIN
        if (mcChars.Length > 0 && gameObject.name.Contains("MC"))
        {
            mcChars[0].gameObject.SetActive(false);
            mcChars[1].gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("FirstVIP", 0) == 1)
            {
                //ClikManager.Instance.SendCustomEvent("vipGuestPurchased", 1);

                PlayerPrefs.SetInt("FirstVIP", 0);

                //if (ArrowTutorialHolder.Instance != null)
                //    ArrowTutorialHolder.Instance.EnableNextTut(11);

                VipTable.Instance.EnableVipTable();
                isThereVIP = true;
                InfoCountText.vip.transform.parent.gameObject.SetActive(true);
                mcChars[2].gameObject.SetActive(true);
                animator = mcChars[2].GetComponentInChildren<Animator>();
            }
            else
            {
                if ((Random.Range(0, 32) == 0) && (!isThereVIP) && (PlayerPrefs.GetInt("VIP", 0) == 1))
                {
                    //VIP
                    mcChars[2].gameObject.transform.Find("CAM").gameObject.SetActive(false);

                    VipTable.Instance.EnableVipTable();
                    isThereVIP = true;
                    InfoCountText.vip.transform.parent.gameObject.SetActive(true);
                    mcChars[2].gameObject.SetActive(true);
                    animator = mcChars[2].GetComponentInChildren<Animator>();
                }
                else
                {
                    if (Random.Range(0, 2) == 0 /*|| (PlayerPrefs.GetInt("Poop", 0) == 1)*/)
                    {
                        //Destroy(transform.GetChild(1).gameObject);
                        foreach (Transform child in transform.GetChild(0))
                        {
                            if (child.name != mcChars[0].name && child.name != mcChars[2].name)
                                Destroy(child.gameObject);
                        }
                        tempCupHolder = cupHolderBoy;
                        mcChars[0].gameObject.SetActive(true);
                        animator = mcChars[0].GetComponentInChildren<Animator>();
                    }
                    else
                    {
                        //Destroy(transform.GetChild(1).gameObject);
                        foreach (Transform child in transform.GetChild(0))
                        {
                            if (child.name != mcChars[1].name && child.name != mcChars[2].name)
                                Destroy(child.gameObject);
                        }
                        tempCupHolder = cupHolderGirl;
                        mcChars[1].gameObject.SetActive(true);
                        animator = mcChars[1].GetComponentInChildren<Animator>();
                    }

                    if (PlayerPrefs.GetInt("Eat", 0) > 25 && PlayerPrefs.GetInt("Eat", 0) < 50)
                    {
                        if (Random.Range(0, 3) == 0)
                            doesWant2Food = true;
                    }
                    else if (PlayerPrefs.GetInt("Eat", 0) >= 50 && PlayerPrefs.GetInt("Eat", 0) < 90)
                    {
                        if (Random.Range(0, 2) == 0)
                            doesWant2Food = true;
                    }
                    else if (PlayerPrefs.GetInt("Eat", 0) >= 90)
                    {
                        if (Random.Range(0, 3) != 0)
                            doesWant2Food = true;
                    }
                }
            }

        }
        //---
        #endregion

        startPosGuest = transform.position;

        agent = GetComponent<NavMeshAgent>();

        if (mcChars[2].gameObject.activeInHierarchy)
            FindSeat();

        else
        {
            //PARENTING AND FIRST MOVING TO
            GoToLine();
        }
    }

    private void CheckIfOtherLineIsBetter()
    {

    }

    private void GoToLineDrink()
    {
        Transform targ = FindObjectOfType<WaitingLine2>().GetEmptyTransform();
        if (targ == null)
        {
            FindSeat();
        }
        else
        {
            transform.parent = targ;
            MoveTo(transform.parent.position);
            RotateToParent();
        }
    }

    private void GoToLine()
    {
        StartCoroutine(RealGoToLine());
    }

    IEnumerator RealGoToLine()
    {
        do
        {
            if (tempLineIndex == -1)
                tempLineIndex = Random.Range(0, 2);
            else
            {
                if (tempLineIndex == 0)
                    tempLineIndex = 1;
                else
                    tempLineIndex = 0;
            }
            if (FindObjectsOfType<WaitingLine>().Length > 1)
                transform.parent = FindObjectsOfType<WaitingLine>()[tempLineIndex].GetEmptyTransform();
            else
                transform.parent = FindObjectsOfType<WaitingLine>()[0].GetEmptyTransform();

            if (transform.parent == null)
                yield return new WaitForSeconds(0.1f);
        } while (transform.parent == null);

        MoveTo(transform.parent.position);
        RotateToParent();
    }

    #region Eating

    private int gotFoodIndex = 0;

    public void GotFood(GameObject obj = null)
    {

        if (!mcChars[2].gameObject.activeInHierarchy || (mcChars[2].gameObject.activeInHierarchy && gotFoodIndex == 2))
            needFood = false;

        if (mcChars[2].gameObject.activeInHierarchy)
        {
            if (transform.parent.name.Contains("NEW"))
                obj.GetComponent<FoodIconChoose>().PlayDownAnim();
            else
                obj.GetComponent<FoodIconChoose>().PlayDownAnim();
        }
        else
        {
            if (transform.parent.name.Contains("NEW"))
                transform.parent.GetComponentsInChildren<FoodIconChoose>()[gotFoodIndex].PlayDownAnim();
            else
                transform.parent.GetComponentsInChildren<FoodIconChoose>()[gotFoodIndex].PlayDownAnim();
        }

        gotFoodIndex++;

        if (!mcChars[2].gameObject.activeInHierarchy)
            foodIndex = GetComponentInParent<McTable>().tempNeededFoodIndex;
    }

    public void StartEating()
    {
        if (mcChars[2].gameObject.activeInHierarchy)
            FindObjectOfType<VipTable>().DisableVipTable();

        Invoke("Eat", 1f);
    }

    private void Eat()
    {
        PlayerPrefs.SetInt("Eat", PlayerPrefs.GetInt("Eat", 0) + 1);

        if (PlayerPrefs.GetInt("Eat", 0) == 15)
        {

            PlayerPrefs.SetInt("Fries", 1);
            InfoCountText.fries.transform.parent.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Eat", 0) == 55)
        {

            PlayerPrefs.SetInt("Pizza", 1);
            InfoCountText.pizza.transform.parent.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Eat", 0) == 280)
        {

            PlayerPrefs.SetInt("IceCream", 1);
            InfoCountText.iceCream.transform.parent.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Eat", 0) == 84)
        {
            if (ArrowTutorialHolder.Instance != null)
                ArrowTutorialHolder.Instance.EnableNextTut(9); //OUTSIDE
        }

        if (PlayerPrefs.GetInt("Eat", 0) == 105)
        {
            if (ArrowTutorialHolder.Instance != null)
                ArrowTutorialHolder.Instance.EnableNextTut(10); //WHEEL
        }

        if (PlayerPrefs.GetInt("Eat", 0) == 190)  //DRINKSHOLDER
        {
            FindObjectOfType<DrinksHolder>().SetDrinksTrue();
            if (ArrowTutorialHolder.Instance != null)
                ArrowTutorialHolder.Instance.EnableNextTut(11);
        }

        if (PlayerPrefs.GetInt("Eat", 0) == 380)  //NEXT LEVEL
        {
            FindObjectOfType<NewLevel>().EnableNewLevel();
            if (ArrowTutorialHolder.Instance != null)
                ArrowTutorialHolder.Instance.EnableNextTut(14);
        }

        animator.SetBool("eat", true);
        Invoke("StopEating", Random.Range(3f, 8f));
    }

    public Avatar fatAvatar;

    bool doesWant2Food = false;

    private void StopEating()
    {
        if (mcChars[2].gameObject.activeInHierarchy)//VIP
        {
            GameObject obj2 = Instantiate(Resources.Load("PoofMc") as GameObject, transform.parent.GetChild(0).position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(obj2, 1.5f);
            transform.parent.GetChild(0).gameObject.SetActive(false);

            GameObject obj0 = Instantiate(Resources.Load("PoofMc") as GameObject, transform.parent.GetChild(1).position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(obj0, 1.5f);
            transform.parent.GetChild(1).gameObject.SetActive(false);

            GameObject obj1 = Instantiate(Resources.Load("PoofMc") as GameObject, transform.parent.GetChild(2).position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(obj1, 1.5f);
            transform.parent.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            GameObject obj2 = Instantiate(Resources.Load("PoofMc") as GameObject, transform.parent.GetChild(0).position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(obj2, 1.5f);

            if (doesWant2Food)
            {
                gotFoodIndex--;

                doesWant2Food = false;
                needFood = true;

                animator.SetBool("eat", false);

                transform.parent.GetChild(0).gameObject.SetActive(false);

                StartCoroutine(RotateToParent());
                return;
            }

            if (colaHolder != null)
                Destroy(colaHolder.gameObject);

            transform.parent.GetChild(0).gameObject.SetActive(false);
        }

        animator.transform.GetChild(0).gameObject.name = armatureNameAtStart;

        animator.Rebind();

        transform.GetChild(0).transform.localPosition = Vector3.zero;

        agent.enabled = true;

        MoveTo(startPosGuest);
    }

    #endregion

    #region Paying

    private bool paid = false;
    public void PayFood()
    {
        if (transform.parent == null || gameObject.GetComponentInParent<WaitingLine>() == null || paid)
        {

        }
        else
        {
            paid = true;
            StartCoroutine(PayForFood());
        }
    }

    IEnumerator PayForFood()
    {
        GetComponentInParent<PosFirstSensor>().WaitressPlayAnim();
        animator.Play("Pay");
        yield return new WaitForSeconds(Random.Range(0.7f, 0.7f));

        if (mcChars[2].gameObject.activeInHierarchy)
        {
            for (int i = 0; i < 6; i++)
            {
                transform.parent.parent.parent.GetComponentInChildren<SpawnMoneyPos>().SpawnMoneyOnce();
                yield return new WaitForEndOfFrame();
            }
        }
        else
            transform.parent.parent.parent.GetComponentInChildren<SpawnMoneyPos>().SpawnMoneyOnce();

        yield return new WaitForSeconds(Random.Range(1.4f, 1.9f));
        transform.GetComponentInParent<WaitingLine>().StepForward();
        transform.parent = null;


        if ((((PlayerPrefs.GetInt("Drinks", 0) == 1) && Random.Range(0, 4) == 0 && /*NOT_VIP*/!mcChars[2].gameObject.activeInHierarchy)) || (PlayerPrefs.GetInt("FirstDrink", 0) == 1))
        {
            PlayerPrefs.SetInt("FirstDrink", 0);
            GoToLineDrink();
        }
        else
            FindSeat();

    }

    #endregion



    private Transform parentAtStartMC = null;



    public void FindSeat()
    {
        if (transform.parent != null)
            return;

        //RANDOM SEAT FINDING
        do
        {
            Transform potentialSeat = GameObject.FindGameObjectsWithTag("Seat")[Random.Range(0, GameObject.FindGameObjectsWithTag("Seat").Length)].transform;

            if (mcChars[2].gameObject.activeInHierarchy)
            {
                foodIndex = 0;
                potentialSeat = VipTable.Instance.EnableVipTable();
                FindObjectOfType<McTable2>().delivered0 = false;
                FindObjectOfType<McTable2>().delivered1 = false;
                FindObjectOfType<McTable2>().delivered2 = false;
            }

            if (potentialSeat.GetComponentInChildren<Guest>() == null)
            {
                transform.parent = potentialSeat.transform;
                MoveTo(potentialSeat.transform.position);
                break;
            }
        } while (true);
        //-----------------
    }

    int parentInstanceID;

    public void MoveForward(Transform newParent)
    {
        transform.parent = newParent;
        Invoke("MoveForwardAfterRandom", Random.Range(0.2f, 0.4f) * (transform.parent.GetSiblingIndex() + 1));
    }

    private void MoveForwardAfterRandom()
    {
        agent.isStopped = false;
        StopAllCoroutines();
        canMove = true;
        animator.SetBool("walk", true);
        agent.SetDestination(transform.parent.position);
    }

    public void MoveTo(Vector3 targetPos)
    {
        agent.isStopped = false;
        StopAllCoroutines();
        canMove = true;
        animator.SetBool("walk", true);
        agent.SetDestination(targetPos);
    }

    private bool isOver = false;

    private void Update()
    {
        if (!isOver)
        {
            if (transform.parent != null)
            {
                if (Vector3.Distance(transform.position, new Vector3(transform.parent.position.x, transform.position.y, transform.parent.position.z)) <= 0.15f)
                {
                    if (animator.GetBool("walk"))
                    {
                        StartCoroutine(RotateToParent());
                        animator.SetBool("walk", false);
                        agent.isStopped = true;
                        agent.SetDestination(transform.position);


                        if (transform.parent.name.Contains("Food"))
                        {
                            isOver = true;

                            armatureNameAtStart = animator.transform.GetChild(0).gameObject.name;
                            animator.transform.GetChild(0).gameObject.name = "Armature.012";
                            if (animator.gameObject.name.Contains("Female"))
                            {
                                animator.Rebind();
                                animator.Play("Play");
                            }
                            //else
                            animator.SetBool("play", true);

                            transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);

                            Invoke("LocalPosChange", 1.4f);
                        }
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, startPosGuest) < 0.3f)
            {
                if (mcChars[2].gameObject.activeInHierarchy)//VIP
                {
                    FindObjectOfType<McTable2>().delivered0 = false;
                    FindObjectOfType<McTable2>().delivered1 = false;
                    FindObjectOfType<McTable2>().delivered2 = false;
                    InfoCountText.vip.transform.parent.gameObject.SetActive(false);
                    isThereVIP = false;
                }

                Destroy(gameObject);
            }
        }
    }

    private void LocalPosChange()
    {
        agent.enabled = false;
        transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);

        transform.localPosition = new Vector3(0f, transform.localPosition.y, -0.021f);


        transform.GetChild(0).transform.localPosition = new Vector3(0f, 0.137f, 0.127f);
    }

    [HideInInspector] public bool canMove = false;
    Transform colaHolder;

    IEnumerator RotateToParent()
    {
        if (transform.parent.name.Contains("FoodPos"))
        {
            if (GetComponentsInChildren<FoodBoxPrefab>().Length != 0)
                colaHolder = GetComponentInChildren<FoodBoxPrefab>().transform;
            if (colaHolder != null)
            {
                CupHolderDown();

                colaHolder.parent = transform.parent;
                if (GetComponentInParent<McTable>().gameObject.name.Contains("Loca"))
                    colaHolder.localPosition = new Vector3(-0.4f, -0.113f, 0.311f);
                else
                    colaHolder.localPosition = new Vector3(-0.34f, 0.082f, 0.311f);

                colaHolder.localRotation = Quaternion.identity;
                colaHolder.Rotate(Vector3.up, Random.Range(0, 360));
            }

            needFood = true;
            transform.parent.GetComponentInChildren<FoodIconChoose>().GetComponent<Animation>().Play();

            if (mcChars[2].gameObject.activeInHierarchy)
            {
                transform.parent.GetComponentsInChildren<FoodIconChoose>()[1].GetComponent<Animation>().Play();
                transform.parent.GetComponentsInChildren<FoodIconChoose>()[2].GetComponent<Animation>().Play();
            }
        }

        while (Mathf.Abs(transform.localRotation.eulerAngles.y) > 0.5f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, 3f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.localRotation = Quaternion.identity;
    }

    string armatureNameAtStart;
}

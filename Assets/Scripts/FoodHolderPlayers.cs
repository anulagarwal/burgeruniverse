using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class FoodHolderPlayers : MonoBehaviour
{
    #region AI Interactions

    int typeOfAI;
    public void ReachedMaxFood()
    {
        if (typeOfAI == 0)
            maxAnim.Play();
        else if (typeOfAI == 2)
        {
            if (!IsInvoking("FinishedAIWithGettingFood"))
                Invoke("FinishedAIWithGettingFood", timeForInvokingAI);
        }
        else if (typeOfAI == 1)
        {
            if (!IsInvoking("WaiterFinishedAIWithGettingFood"))
                Invoke("WaiterFinishedAIWithGettingFood", timeForInvokingAI);
        }
    }

    public void RigOff()
    {
        if (typeOfAI == 2)
        {
            GetComponentInParent<ChipMan>().RigOff_Chips();

            if (!IsInvoking("FinishedAIWithDroppingFood"))
                Invoke("FinishedAIWithDroppingFood", timeForInvokingAI);
        }
        else if (typeOfAI == 1)
        {
            if (GetComponentInParent<Waiter_Mc>() != null)
                GetComponentInParent<Waiter_Mc>().RigOff_Chips();
            else
                GetComponentInParent<Waiter_Drinks>().RigOff_Chips();

            if (!IsInvoking("WaiterFinishedAIWithDroppingFood"))
                Invoke("WaiterFinishedAIWithDroppingFood", timeForInvokingAI);
        }
        else
            GetComponentInParent<PlayerMcDonalds>().RigOff_Chips();
    }

    public void RigOn()
    {
        //RIG ON
        if (typeOfAI == 2)
            GetComponentInParent<ChipMan>().RigOn_Chips();
        else if (typeOfAI == 0)
            GetComponentInParent<PlayerMcDonalds>().RigOn_Chips();
        else if (typeOfAI == 1)
        {
            if (GetComponentInParent<Waiter_Mc>() != null)
                GetComponentInParent<Waiter_Mc>().RigOn_Chips();
            else
                GetComponentInParent<Waiter_Drinks>().RigOn_Chips();
        }
    }

    private bool canAIGetChips = true;
    private void CanGetChipsAgain()
    {
        canAIGetChips = true;
    }

    private void FinishedAIWithDroppingFood()
    {
        if (!GetComponentInParent<ChipMan>().gameObject.name.Contains("DELIVERY") && canAIGetChips)
        {
            canAIGetChips = false;
            Invoke("CanGetChipsAgain", 2.5f);

            GetComponentInParent<ChipMan>().GetChips();
        }
    }
    private void FinishedAIWithGettingFood()
    {
        GetComponentInParent<ChipMan>().DropChipsAtTable();
    }

    private void WaiterFinishedAIWithDroppingFood()
    {

        if (GetComponentInParent<Waiter_Mc>() != null)
            GetComponentInParent<Waiter_Mc>().GetRandomFoods();
        else
            GetComponentInParent<Waiter_Drinks>().GetDrink();
    }
    private void WaiterFinishedAIWithGettingFood()
    {

        if (GetComponentInParent<Waiter_Mc>() != null)
            GetComponentInParent<Waiter_Mc>().GoToRandomTable();
        else
            GetComponentInParent<Waiter_Drinks>().GoPutDownDrinks();
    }

    #endregion

    public GameObject foodHolderPref;

    public int capacity = 3;

    public Animation maxAnim;

    public UpgradeButtonNew upgradeManager;


    private void GetFoodInfinite(FoodUp foodUpHolder)
    {
        //capacity = 8 * 3;
        StartCoroutine(GettingFoodFromInfiniteSpawner(foodUpHolder));
    }

    private void GetFoodFromShelf(FoodUpShelf foodUpHolder)
    {
        if (typeOfAI == 2 && !GetComponentInParent<ChipMan>().name.Contains("DELIVERY"))
            return;
        StartCoroutine(GettingFoodFromShelfSpawner(foodUpHolder));
    }


    Collider coll;
    private void ResetCollider()
    {
        if (coll.enabled)
        {
            coll.enabled = false;
            ResetCollider();
        }
        else
        {
            coll.enabled = true;
            Invoke("ResetCollider", timeBetweenDrops);
        }
    }

    void Start()
    {
        if (upgradeManager)
            capacity = upgradeManager.GetUpgradeValue();

        #region Decide Player Type
        if (GetComponentInParent<PlayerMcDonalds>() != null)
            typeOfAI = 0;
        else if (GetComponentInParent<Waiter_Mc>() != null || GetComponentInParent<Waiter_Drinks>() != null)
            typeOfAI = 1;
        else if (GetComponentInParent<ChipMan>() != null)
            typeOfAI = 2;


        coll = GetComponent<Collider>();
        if (typeOfAI == 0)
            Invoke("ResetCollider", 1f);
        #endregion

        #region Spawn FoodBox Holders
        float pacing = 0.165f;
        for (int i = 0; i < 30; i++)
        {
            GameObject obj1 = Instantiate(foodHolderPref, transform);
            obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
            obj1.transform.localRotation = Quaternion.identity;

            //if (i > 0)
            //    obj1.AddComponent<StackFollow>();

            obj1.transform.localScale = new Vector3(0.7142858f, 0.7142858f, 0.7142858f);
        }
        #endregion
    }

    #region Food Decisions
    private float timeForInvokingAI = 1.2f;

    private bool CanGetMoreFood()
    {
        int countOfBoxesInChildren = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount >= 1)
                countOfBoxesInChildren++;

            if (countOfBoxesInChildren >= capacity)
            {
                ReachedMaxFood();

                return false;
            }
        }

        if (countOfBoxesInChildren < capacity)
            return true;
        else
            return false;
    }

    private bool HasMoreFoodToPutDown()
    {
        if (typeOfAI == 1)
        {
            if (!IsInvoking("WaiterFinishedAIWithDroppingFood"))
                Invoke("WaiterFinishedAIWithDroppingFood", timeForInvokingAI);
        }

        if (GetComponentsInChildren<FoodBoxPrefab>().Length != 0)
            return true;
        else
        {
            RigOff();

            return false;
        }
    }
    //------------------------------------------------
    private int GetFirstEmptyChildIndex()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount == 0)
                return i;
        }

        return -1;
    }

    private Transform FindLatestFood(int index)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).childCount == 1)
            {
                if (transform.GetChild(i).GetChild(0).gameObject.name.Contains(index.ToString()) && !transform.GetChild(i).GetChild(0).gameObject.name.Contains("Cylinder"))
                    return transform.GetChild(i).GetChild(0);
            }
        }
        return null;
    }

    private Transform GetLatestBoxForTrash()
    {
        for (int i = capacity + 1; i >= 0; i--)
        {
            if (transform.GetChild(i).GetComponentInChildren<FoodBoxPrefab>() != null)
                return transform.GetChild(i).GetChild(0);
        }

        return null;
    }

    private Transform GetLatestBox(bool lookingForDrink = false)
    {
        if (lookingForDrink)
        {
            for (int i = capacity + 1; i >= 0; i--)
            {
                Debug.Log("WAITER_DRINK_0");
                if (transform.GetChild(i).GetComponentInChildren<FoodBoxPrefab>() != null && transform.GetChild(i).GetComponentInChildren<FoodBoxPrefab>().gameObject.name.Contains("Cylinder"))
                    return transform.GetChild(i).GetChild(0);
            }
        }
        else
        {
            for (int i = capacity + 1; i >= 0; i--)
            {
                if (transform.GetChild(i).GetComponentInChildren<FoodBoxPrefab>() != null && !transform.GetChild(i).GetComponentInChildren<FoodBoxPrefab>().gameObject.name.Contains("Cylinder"))
                    return transform.GetChild(i).GetChild(0);
            }
        }
        return null;
    }
    //------------------------------------------------
    public bool DoesHave(int foodIndex)
    {
        foreach (FoodBoxPrefab food in GetComponentsInChildren<FoodBoxPrefab>())
        {
            if (food.gameObject.name.Contains(foodIndex.ToString()))
                return true;
        }
        return false;
    }
    #endregion

    #region Triggers
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("FoodUp") || other.GetComponent<McTable>() != null || other.GetComponent<McTable2>() != null || other.GetComponent<PutDownBag>() != null)
        {
            StopAllCoroutines();
            canGetFoodFromShelfSpawner = canGetFroodFromInfiniteSpawner = canPutDownFoodToBag = canPutDownFoodToTable = true;
        }

        if (other.GetComponent<PutDownBag>() != null && other.isTrigger)
        {
            StartCoroutine(PutDownFoodToBag(other.GetComponentInChildren<PutDownBag>()));
            //other.transform.parent.GetComponentInChildren<ExchangeIcon>().StopAllCoroutines();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("FoodUpInfinite"))
        {
            GetFoodInfinite(other.GetComponentInChildren<FoodUp>());
        }

        if (other.gameObject.name.Contains("FoodUpShelf"))
        {
            GetFoodFromShelf(other.GetComponentInChildren<FoodUpShelf>());
        }

        if (other.GetComponentInChildren<McTable>() != null)
        {
            StartCoroutine(PutDownFoodToTable(other.GetComponentInChildren<McTable>()));
        }

        if (other.GetComponentInChildren<McTable2>() != null && typeOfAI == 0)
        {
            StartCoroutine(PutDownFoodToTable(other.GetComponentInChildren<McTable2>()));
        }

        if (other.GetComponent<PutDownBag>() != null && other.isTrigger)
        {
            StartCoroutine(PutDownFoodToBag(other.GetComponentInChildren<PutDownBag>()));
            //other.transform.parent.GetComponentInChildren<ExchangeIcon>().StartChecking();
        }
    }
    #endregion


    #region Coroutines-Try To Put Down / Get Up

    private float timeBetweenDrops = 0.2f;

    #region Put Down Food

    bool canPutDownFoodToBag = true;
    IEnumerator PutDownFoodToBag(PutDownBag bag)
    {
        if (canPutDownFoodToBag)
        {
            canPutDownFoodToBag = false;
            while (HasMoreFoodToPutDown())
            {
                //RIGDOWN---------
                if ((GetComponentsInChildren<FoodBoxPrefab>().Length + GetComponentsInChildren<BoxPlaceHolder>().Length) <= 1)
                {
                    RigOff();
                }
                //RIGDOWN---------

                Transform transformToSpawn = bag.TryToDropDownBagToEmptySpace();

                if (transformToSpawn == null)
                    break;
                else
                {
                    if (typeOfAI == 0)
                    {
                        //foreach (FoodIconChoose icons in FindObjectsOfType<FoodIconChoose>())
                        //    icons.SetColor();

                        VibrationManager.Instance.Vibr_LightImpact();
                    }

                    if (bag.name.Contains("Trash"))
                    {
                        GetLatestBoxForTrash().GetComponent<FoodBoxPrefab>().MoveToParent(transformToSpawn);
                    }
                    else if (bag.name.Contains("Drink"))
                    {
                        int indexOfNeeded = -10;
                        if (GetLatestBox(true) != null)
                            indexOfNeeded = GetLatestBox(true).GetSiblingIndex();

                        //Debug.Log("DRINKING_WAITER__" + GetLatestBox(true).gameObject + "____" + transformToSpawn.name);

                        Debug.Log("WAITER_DRINK_-1       " + transformToSpawn.name);


                        GetLatestBox(true).GetComponent<FoodBoxPrefab>().MoveToParent(transformToSpawn);

                        Debug.Log("WAITER_DRINK_1");


                        if (indexOfNeeded != -10)
                            SortFoods(indexOfNeeded);
                    }
                    else
                    {
                        SortFoods(0);
                        GetLatestBox().GetComponent<FoodBoxPrefab>().MoveToParent(transformToSpawn);
                    }
                }

                yield return new WaitForSeconds(timeBetweenDrops);
            }

            canPutDownFoodToBag = true;
        }
    }

    //private void WaiterCanTryAgain()
    //{
    //    canPutDownFoodToTable = true;
    //}
    bool canPutDownFoodToTable = true;


    IEnumerator PutDownFoodToTable(McTable2 table)
    {
        if (canPutDownFoodToTable /*&& (typeOfAI == 1)*/)
        {
            //Invoke("WaiterCanTryAgain", 5f);
            canPutDownFoodToTable = false;

            if (typeOfAI == 1)
            {
                Debug.Log("WAITER_TRIES_TO_PUT_DOWN");

                while (HasMoreFoodToPutDown())
                {
                    if (typeOfAI == 1)
                        Debug.Log("WAITER_TRIES_TO_PUT_DOWN_X_TIMES");

                    //if (typeOfAI == 1)
                    //{
                    //    if (isFirstTy)
                    //        isFirstTy = false;
                    //    Transform targRealPosW = table.GetFreeSpaceForWaiter(GetComponentsInChildren<FoodBoxPrefab>().Length, isFirstTy);


                    //    if (targRealPosW == null)
                    //        break;

                    //    //RIGDOWN---------
                    //    if ((GetComponentsInChildren<FoodBoxPrefab>().Length + GetComponentsInChildren<BoxPlaceHolder>().Length) <= 1)
                    //    {
                    //        RigOff();
                    //    }
                    //    //RIGDOWN---------

                    //    GetLatestBox().GetComponent<FoodBoxPrefab>().MoveToParent(targRealPosW);
                    //}
                    //else
                    //{
                    Transform targRealPos = table.GetFreeSpace(DoesHave(0), DoesHave(1), DoesHave(2), DoesHave(3));

                    if (targRealPos == null)
                        break;

                    int indexOfFoodNeeded = targRealPos.parent.GetComponentInChildren<Guest>().foodIndex;
                    FoodBoxPrefab neededBox = GetComponentInChildren<FoodBoxPrefab>();

                    neededBox = FindLatestFood(indexOfFoodNeeded).GetComponent<FoodBoxPrefab>();
                    int indexOfNeededBoxInChildren = neededBox.transform.parent.GetSiblingIndex();

                    //RIGDOWN---------
                    if ((GetComponentsInChildren<FoodBoxPrefab>().Length + GetComponentsInChildren<BoxPlaceHolder>().Length) <= 1)
                    {
                        RigOff();
                    }
                    //RIGDOWN---------

                    neededBox.MoveToParent(targRealPos);

                    //SORTING
                    for (int i = indexOfNeededBoxInChildren; i < transform.childCount - 1; i++)
                    {
                        if (transform.GetChild(i + 1).childCount == 1)
                        {
                            Transform tempChild = transform.GetChild(i + 1).GetChild(0);
                            tempChild.parent = transform.GetChild(i);
                            tempChild.localPosition = Vector3.zero;
                        }
                        if (transform.GetChild(i).childCount == 0 && transform.GetChild(i + 1).childCount == 0)
                            break;
                    }
                    //}

                    yield return new WaitForSeconds(timeBetweenDrops);
                }
            }
            else if (typeOfAI == 0)
            {
                Transform targRealPos = table.GetFreeSpace(DoesHave(0), DoesHave(1), DoesHave(2), DoesHave(3));

                if (targRealPos != null)
                {

                    int indexOfFoodNeeded = targRealPos.parent.GetComponentInChildren<Guest>().foodIndex;

                    if (table.isVIP)
                        indexOfFoodNeeded = table.GetVipNeededIndex(DoesHave(0), DoesHave(1), DoesHave(2));

                    FoodBoxPrefab neededBox = GetComponentInChildren<FoodBoxPrefab>();

                    neededBox = FindLatestFood(indexOfFoodNeeded).GetComponent<FoodBoxPrefab>();
                    int indexOfNeededBoxInChildren = neededBox.transform.parent.GetSiblingIndex();

                    if (typeOfAI == 0)
                    {
                        //foreach (FoodIconChoose icons in FindObjectsOfType<FoodIconChoose>())
                        //    icons.SetColor();

                        VibrationManager.Instance.Vibr_LightImpact();
                    }

                    //RIGDOWN---------
                    if ((GetComponentsInChildren<FoodBoxPrefab>().Length/* + GetComponentsInChildren<BoxPlaceHolder>().Length*/) <= 1)
                    {
                        RigOff();
                    }
                    //RIGDOWN---------

                    neededBox.MoveToParent(targRealPos);

                    if (table.isVIP)
                    {
                        table.VipDelivered(indexOfFoodNeeded);

                        //if (indexOfFoodNeeded == 0)
                        //    table.delivered0 = true;
                        //else if (indexOfFoodNeeded == 1)
                        //    table.delivered1 = true;
                        //else
                        //    table.delivered2 = true;
                    }

                    //SORTING
                    SortFoods(indexOfFoodNeeded);
                    //}

                    yield return new WaitForSeconds(timeBetweenDrops);
                }
            }

            canPutDownFoodToTable = true;
        }
        //bool isFirstTy = true;
    }
    IEnumerator PutDownFoodToTable(McTable table)
    {
        if (canPutDownFoodToTable /*&& (typeOfAI == 1)*/)
        {
            //Invoke("WaiterCanTryAgain", 5f);
            canPutDownFoodToTable = false;

            if (typeOfAI == 1)
            {
                Debug.Log("WAITER_TRIES_TO_PUT_DOWN");

                while (HasMoreFoodToPutDown())
                {
                    if (typeOfAI == 1)
                        Debug.Log("WAITER_TRIES_TO_PUT_DOWN_X_TIMES");

                    //if (typeOfAI == 1)
                    //{
                    //    if (isFirstTy)
                    //        isFirstTy = false;
                    //    Transform targRealPosW = table.GetFreeSpaceForWaiter(GetComponentsInChildren<FoodBoxPrefab>().Length, isFirstTy);


                    //    if (targRealPosW == null)
                    //        break;

                    //    //RIGDOWN---------
                    //    if ((GetComponentsInChildren<FoodBoxPrefab>().Length + GetComponentsInChildren<BoxPlaceHolder>().Length) <= 1)
                    //    {
                    //        RigOff();
                    //    }
                    //    //RIGDOWN---------

                    //    GetLatestBox().GetComponent<FoodBoxPrefab>().MoveToParent(targRealPosW);
                    //}
                    //else
                    //{
                    Transform targRealPos = table.GetFreeSpace(DoesHave(0), DoesHave(1), DoesHave(2), DoesHave(3));

                    if (targRealPos == null)
                        break;

                    int indexOfFoodNeeded = targRealPos.parent.GetComponentInChildren<Guest>().foodIndex;
                    FoodBoxPrefab neededBox = GetComponentInChildren<FoodBoxPrefab>();

                    neededBox = FindLatestFood(indexOfFoodNeeded).GetComponent<FoodBoxPrefab>();
                    int indexOfNeededBoxInChildren = neededBox.transform.parent.GetSiblingIndex();

                    //RIGDOWN---------
                    if ((GetComponentsInChildren<FoodBoxPrefab>().Length + GetComponentsInChildren<BoxPlaceHolder>().Length) <= 1)
                    {
                        RigOff();
                    }
                    //RIGDOWN---------

                    neededBox.MoveToParent(targRealPos);

                    //SORTING
                    for (int i = indexOfNeededBoxInChildren; i < transform.childCount - 1; i++)
                    {
                        if (transform.GetChild(i + 1).childCount == 1)
                        {
                            Transform tempChild = transform.GetChild(i + 1).GetChild(0);
                            tempChild.parent = transform.GetChild(i);
                            tempChild.localPosition = Vector3.zero;
                        }
                        if (transform.GetChild(i).childCount == 0 && transform.GetChild(i + 1).childCount == 0)
                            break;
                    }
                    //}

                    yield return new WaitForSeconds(timeBetweenDrops);
                }
            }
            else if (typeOfAI == 0)
            {
                Transform targRealPos = table.GetFreeSpace(DoesHave(0), DoesHave(1), DoesHave(2), DoesHave(3));

                if (targRealPos != null)
                {

                    int indexOfFoodNeeded = targRealPos.parent.GetComponentInChildren<Guest>().foodIndex;

                    if (table.isVIP)
                        indexOfFoodNeeded = table.GetVipNeededIndex(DoesHave(0), DoesHave(1), DoesHave(2));

                    FoodBoxPrefab neededBox = GetComponentInChildren<FoodBoxPrefab>();

                    neededBox = FindLatestFood(indexOfFoodNeeded).GetComponent<FoodBoxPrefab>();
                    int indexOfNeededBoxInChildren = neededBox.transform.parent.GetSiblingIndex();

                    if (typeOfAI == 0)
                    {
                        //foreach (FoodIconChoose icons in FindObjectsOfType<FoodIconChoose>())
                        //    icons.SetColor();

                        VibrationManager.Instance.Vibr_LightImpact();
                    }

                    //RIGDOWN---------
                    if ((GetComponentsInChildren<FoodBoxPrefab>().Length/* + GetComponentsInChildren<BoxPlaceHolder>().Length*/) <= 1)
                    {
                        RigOff();
                    }
                    //RIGDOWN---------

                    neededBox.MoveToParent(targRealPos);

                    if (table.isVIP)
                    {
                        table.VipDelivered(indexOfFoodNeeded);

                        //if (indexOfFoodNeeded == 0)
                        //    table.delivered0 = true;
                        //else if (indexOfFoodNeeded == 1)
                        //    table.delivered1 = true;
                        //else
                        //    table.delivered2 = true;
                    }

                    //SORTING
                    SortFoods(indexOfFoodNeeded);
                    //}

                    yield return new WaitForSeconds(timeBetweenDrops);
                }
            }

            canPutDownFoodToTable = true;
        }
        //bool isFirstTy = true;
    }

    #endregion

    private void SortFoods(int indexOfSelected)
    {
        StartCoroutine(Sorting());
    }

    IEnumerator Sorting()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                if ((transform.GetChild(i + 1).childCount >= 1) && (transform.GetChild(i).childCount == 0))
                {
                    Transform tempChild = transform.GetChild(i + 1).GetChild(0);
                    tempChild.parent = transform.GetChild(i);
                    tempChild.localPosition = Vector3.zero;
                }
                //if (transform.GetChild(i).childCount == 0 && transform.GetChild(i + 1).childCount == 0)
                //    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    #region Get Up Food

    bool canGetFoodFromShelfSpawner = true;
    IEnumerator GettingFoodFromShelfSpawner(FoodUpShelf foodUpHold)
    {
        if (canGetFoodFromShelfSpawner)
        {
            canGetFoodFromShelfSpawner = false;

            bool isFirst = true;

            while (CanGetMoreFood())
            {
                //SPAWN TO THIS CHILD
                Transform boxToGetFromShelfTr = foodUpHold.GetLastBox();
                if (boxToGetFromShelfTr == null)
                    break;

                if (isFirst)
                {
                    isFirst = false;
                    RigOn();
                }

                boxToGetFromShelfTr.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(GetFirstEmptyChildIndex()));

                if (typeOfAI == 0)
                {
                    //foreach (FoodIconChoose icons in FindObjectsOfType<FoodIconChoose>())
                    //    icons.SetColor();

                    VibrationManager.Instance.Vibr_LightImpact();
                }


                yield return new WaitForSeconds(timeBetweenDrops);
            }

            canGetFoodFromShelfSpawner = true;
        }
    }

    bool canGetFroodFromInfiniteSpawner = true;
    IEnumerator GettingFoodFromInfiniteSpawner(FoodUp foodUpHold)
    {
        if (canGetFroodFromInfiniteSpawner)
        {
            canGetFroodFromInfiniteSpawner = false;

            bool isFirst = true;

            while (CanGetMoreFood())
            {
                if (typeOfAI == 0)
                {
                    //foreach (FoodIconChoose icons in FindObjectsOfType<FoodIconChoose>())
                    //    icons.SetColor();

                    VibrationManager.Instance.Vibr_LightImpact();
                }

                //SPAWN TO THIS CHILD
                foodUpHold.SpawnFood(transform.GetChild(GetFirstEmptyChildIndex()));

                if (isFirst)
                {
                    isFirst = false;
                    RigOn();
                }

                yield return new WaitForSeconds(timeBetweenDrops);
            }

            canGetFroodFromInfiniteSpawner = true;
        }
    }

    #endregion

    #endregion
}

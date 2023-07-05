//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerChipsHolder : MonoBehaviour
//{
//    public int[] childCounts;

//    public GameObject chipHolder;

//    public static PlayerChipsHolder Instance;

//    public int newCapacity = 2;
//    public bool usingNewCapacity = false;

//    public int capacity = 20;

//    public int tempCapacity = 0;

//    public Animation maxChipsAnim;

//    //MEKI

//    private Transform firstChipHolder;


//    public void TryToPutDownFood(Transform targetPosToPut, bool isTable = false)
//    {
//        if (firstChipHolder.childCount > 1)
//            Destroy(firstChipHolder.GetChild(0).gameObject);

//        StartCoroutine(PutDownFoods(targetPosToPut, isTable));
//    }

//    IEnumerator PutDownFoods(Transform targetPosToPut, bool isTable = false)
//    {
//        while (true)
//        {
//            //if (targetPosToPut == null)
//            //    break;

//            if (childCounts[0] >= 0)
//            {
//                Transform targRealPos = null;
//                if (isTable)
//                {
//                    bool has0 = false, has1 = false, has2 = false;

//                    //Mibol van nekunk
//                    //foreach (BoxPrefab box in transform.GetComponentsInChildren<BoxPrefab>())
//                    //{
//                    //    if (box.gameObject.name.Contains("0"))
//                    //        has0 = true;
//                    //    if (box.gameObject.name.Contains("1"))
//                    //        has1 = true;
//                    //    if (box.gameObject.name.Contains("2"))
//                    //        has2 = true;
//                    //}

//                    if (targetPosToPut.GetComponentInChildren<McTable>() != null)
//                        targRealPos = targetPosToPut.GetComponentInChildren<McTable>().GetFreeSpace(has0, has1, has2);

//                    if (targRealPos == null)
//                        break;

//                    int indexOfFoodNeeded = targRealPos.parent.GetComponentInChildren<Guest>().foodIndex;
//                    BoxPrefab neededBox = GetComponentInChildren<BoxPrefab>();

//                    foreach (BoxPrefab box in transform.GetComponentsInChildren<BoxPrefab>())
//                    {
//                        if (box.gameObject.name.Contains(indexOfFoodNeeded.ToString()))
//                            neededBox = box;
//                    }
//                    bool canSort = false;
//                    foreach (BoxPrefab box in transform.GetComponentsInChildren<BoxPrefab>())
//                    {
//                        if (canSort)
//                        {
//                            box.transform.parent = box.transform.parent.parent.GetChild(box.transform.parent.GetSiblingIndex() - 1);
//                            box.transform.localPosition = Vector3.zero;
//                        }

//                        if (neededBox == box)
//                            canSort = true;
//                    }


//                    //transform.GetChild(0).GetChild(childCounts[0]).GetComponentInChildren<BoxPrefab>().MoveToTarget(targRealPos);
//                    neededBox.MoveToTarget(targRealPos);
//                    childCounts[0]--;

//                    tempCapacity--;


//                    //int tempNeededBoxParentSiblingIndex = neededBox.transform.parent.GetSiblingIndex();
//                    //for (int i = tempNeededBoxParentSiblingIndex + 1; i < transform.GetChild(0).childCount; i++)
//                    //{
//                    //    if (transform.GetChild(0).GetChild(i).childCount == 0)
//                    //        break;

//                    //    transform.GetChild(0).GetChild(i).GetComponentInChildren<BoxPrefab>().MoveUpByOne(transform.GetChild(0).GetChild(i - 1));
//                    //}

//                }
//                else
//                {
//                    if (targetPosToPut.GetComponent<FoodShelf>() != null)
//                        targRealPos = targetPosToPut.GetComponent<FoodShelf>().GetFreeSpace();
//                    else
//                        targRealPos = targetPosToPut.GetComponent<FoodShelfBag>().GetFreeSpace();



//                    if (targRealPos == null)
//                        break;

//                    if (transform.GetChild(0).GetChild(childCounts[0]).GetComponentInChildren<BoxPrefab>() != null)
//                        transform.GetChild(0).GetChild(childCounts[0]).GetComponentInChildren<BoxPrefab>().MoveToTarget(targRealPos);
//                    childCounts[0]--;

//                    tempCapacity--;
//                }

//            }
//            else
//            {
//                if (GetComponentInParent<PlayerMcDonalds>() != null)
//                    GetComponentInParent<PlayerMcDonalds>().RigOff_Chips();
//                break;
//            }
//            yield return new WaitForSeconds(0.1f);
//        }
//    }

//    IEnumerator PutDownFoodsOldWay(Transform targetPosToPut, bool isTable = false)
//    {
//        while (true)
//        {
//            //if (targetPosToPut == null)
//            //    break;

//            if (childCounts[0] >= 0)
//            {
//                Transform targRealPos = null;
//                if (isTable)
//                    targRealPos = targetPosToPut.GetComponentInChildren<McTable>().GetFreeSpace(false, false, false);
//                else
//                {
//                    if (targetPosToPut.GetComponent<FoodShelf>() != null)
//                        targRealPos = targetPosToPut.GetComponent<FoodShelf>().GetFreeSpace();
//                    else
//                        targRealPos = targetPosToPut.GetComponent<FoodShelfBag>().GetFreeSpace();
//                }

//                if (targRealPos == null)
//                    break;

//                transform.GetChild(0).GetChild(childCounts[0]).GetComponentInChildren<BoxPrefab>().MoveToTarget(targRealPos);
//                childCounts[0]--;

//                tempCapacity--;
//            }
//            else
//            {
//                GetComponentInParent<PlayerMcDonalds>().RigOff_Chips();
//                break;
//            }
//            yield return new WaitForSeconds(0.1f);
//        }
//    }

//    //MEKI

//    public bool renametoBox0123 = false;

//    public void CapacityCheck()
//    {
//        tempCapacity = GetComponentsInChildren<BoxPrefab>().Length;
//        childCounts[0] = tempCapacity - 1;
//    }

//    public void ReachedCapacity()
//    {
//        CapacityCheck();

//        if (renametoBox0123)
//        {
//            foreach (BoxPrefab box in GetComponentsInChildren<BoxPrefab>())
//            {
//                box.gameObject.name = "Box0123";
//            }
//        }

//        maxChipsAnim.Play();
//        if (GetComponentInParent<ChipMan>() != null)
//            GetComponentInParent<ChipMan>().InvokeDropChipsAtTable(0.75f);
//    }

//    public void ResetChildCounts()
//    {
//        childCounts[0] = childCounts[1] = childCounts[2] = -1;
//    }

//    public void FlyChipsTo(PokerTableChips chipsHolder)
//    {
//        StartCoroutine(FlyingChipsToTable(chipsHolder));
//    }

//    public List<Transform> GetChipsOfType(int t)
//    {
//        List<Transform> list = new List<Transform>();

//        foreach (ChipPrefab chip in transform.GetChild(t).GetComponentsInChildren<ChipPrefab>())
//        {
//            list.Add(chip.transform);
//        }

//        return list;
//    }


//    IEnumerator FlyingChipsToTable(PokerTableChips chipHolderTable)
//    {
//        while (childCounts[0] >= 0 || childCounts[1] >= 0 || childCounts[2] >= 0)
//        {
//            int randomIndex;

//            do
//            {
//                randomIndex = Random.Range(0, 3);
//            } while (childCounts[randomIndex] < 0);



//            transform.GetChild(randomIndex).GetChild(childCounts[randomIndex]).GetComponentInChildren<ChipPrefab>().MoveToTarget(chipHolderTable.GetNewFreeChildAt(randomIndex));

//            childCounts[randomIndex]--;

//            yield return new WaitForSeconds(0.1f);
//        }
//    }


//    private void Awake()
//    {
//        Instance = this;
//    }

//    public Transform GetNextTransformOf(int index)
//    {
//        childCounts[index]++;
//        return transform.GetChild(index).GetChild(childCounts[index]);
//    }

//    private bool isMC_Donalds = false;

//    private void SetNewCapacity()
//    {
//        capacity = newCapacity;
//        Invoke("SetNewCapacity", 0.5f);
//    }

//    private void TempCapacityCheck()
//    {
//        if (transform.GetChild(0).GetChild(0).childCount == 0)
//        {
//            tempCapacity = 0;
//            childCounts[0] = -1;
//        }

//        Invoke("TempCapacityCheck", 3f);
//    }

//    void Start()
//    {
//        Invoke("TempCapacityCheck", 1f);

//        if (usingNewCapacity)
//            SetNewCapacity();

//        if (gameObject.name.Contains("MC"))
//            isMC_Donalds = true;

//        float pacing = 0.017f;

//        if (isMC_Donalds)
//            pacing = 0.165f;

//        if (isMC_Donalds)
//        {
//            for (int i = 0; i < 250; i++)
//            {
//                GameObject obj1 = Instantiate(chipHolder, transform.GetChild(0));
//                obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
//                obj1.transform.localRotation = Quaternion.identity;

//                obj1.transform.localScale = new Vector3(0.7142858f, 0.7142858f, 0.7142858f);

//                //obj1.transform.Rotate(Vector3.right, 90f);
//            }

//            firstChipHolder = transform.GetChild(0).GetChild(0);

//            return;
//        }

//        for (int i = 0; i < 250; i++)
//        {
//            GameObject obj1 = Instantiate(chipHolder, transform.GetChild(0));
//            obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
//            obj1.transform.localRotation = Quaternion.identity;
//            obj1.transform.Rotate(Vector3.right, 90f);
//        }
//        for (int i = 0; i < 250; i++)
//        {
//            GameObject obj1 = Instantiate(chipHolder, transform.GetChild(1));
//            obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
//            obj1.transform.localRotation = Quaternion.identity;
//            obj1.transform.Rotate(Vector3.right, 90f);
//        }
//        for (int i = 0; i < 250; i++)
//        {
//            GameObject obj1 = Instantiate(chipHolder, transform.GetChild(2));
//            obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
//            obj1.transform.localRotation = Quaternion.identity;
//            obj1.transform.Rotate(Vector3.right, 90f);
//        }
//    }

//    void Update()
//    {

//    }
//}

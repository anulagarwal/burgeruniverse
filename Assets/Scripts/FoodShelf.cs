//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FoodShelf : MonoBehaviour
//{
//    public int tempIndex = 0;

//    Transform boxes;

//    void Start()
//    {
//        boxes = transform.parent.GetChild(0);

//        CheckIfNegative();
//    }

//    private void CheckIfNegative()
//    {
//        if (tempIndex < 0)
//            tempIndex = 0;

//        Invoke("CheckIfNegative", 1f);
//    }

//    //private void LateUpdate()
//    //{
//    //    tempIndex = boxes.GetComponentsInChildren<BoxPrefab>().Length;
//    //}

//    public Transform GetFreeSpace()
//    {
//        //transform.parent.GetComponentInChildren<CheckIChildIsMissing>().Check();

//        Debug.Log("GIVING BACK FREE SPACE");

//        int boxesChildCount = boxes.childCount;
//        //int boxCount = boxes.GetComponentsInChildren<BoxPrefab>().Length;

//        if (boxesChildCount != boxCount)
//        {
//            //List<Transform> emptyBoxHolder = new List<Transform>();

//            //for (int i = 0; i < boxesChildCount; i++)
//            //{
//            //    if (boxes.GetChild(i).childCount == 0)
//            //        emptyBoxHolder.Add(boxes.GetChild(i));
//            //}

//            //return emptyBoxHolder[Random.Range(0, emptyBoxHolder.Count)];
//            tempIndex++;

//            if (tempIndex >= boxesChildCount)
//                tempIndex = boxCount;

//            return boxes.GetChild(tempIndex - 1);
//        }
//        else
//            return null;

//        //Debug.Log("GIVING BACK FREE SPACE");

//        //foreach (Guest guest in GetComponentsInChildren<Guest>())
//        //{
//        //    if (guest.needFood)
//        //    {
//        //        guest.needFood = false;
//        //        return guest.transform.parent.GetChild(0);
//        //    }
//        //}
//        //return null;
//    }
//}
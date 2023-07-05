//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FoodShelfBag : MonoBehaviour
//{
//    public int tempIndex = 0;

//    Transform boxes;

//    private Collider coll;

//    private void ColliderReset()
//    {
//        coll.enabled = !coll.enabled;
//        if (coll.enabled)
//            Invoke("ColliderReset", 0.5f);
//        else
//            Invoke("ColliderReset", 0.05f);
//    }

//    void Start()
//    {
//        boxes = transform.parent.GetChild(0);

//        coll = GetComponent<Collider>();
//        ColliderReset();
//    }

//    //private void LateUpdate()
//    //{
//    //    tempIndex = boxes.GetComponentsInChildren<BoxPrefab>().Length;
//    //}

//    public int DestroyFoods()
//    {
//        tempIndex = 0;
//        int moneyToSpawn = boxes.GetComponentsInChildren<BoxPrefab>().Length;
//        for (int i = moneyToSpawn - 1; i >= 0; i--)
//        {
//            Destroy(boxes.GetComponentsInChildren<BoxPrefab>()[i].gameObject);
//        }

//        return moneyToSpawn;
//    }

//    public Transform GetFreeSpace()
//    {
//        //transform.parent.GetComponentInChildren<CheckIChildIsMissing>().Check();

//        Debug.Log("GIVING BACK FREE SPACE");

//        int boxesChildCount = boxes.childCount;
//        int boxCount = boxes.GetComponentsInChildren<BoxPrefab>().Length;

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

//            Transform returnTransform;
//            int ind = 1;
//            do
//            {
//                returnTransform = boxes.GetChild(tempIndex - ind);
//                ind++;
//            } while (returnTransform != null);

//            return boxes.GetChild(tempIndex - 1);
//        }
//        else
//            return null;
//    }
//}
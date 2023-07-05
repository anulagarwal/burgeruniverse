//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BoxesCheckOnSelf : MonoBehaviour
//{
//    private FoodShelf shelf;

//    void Start()
//    {
//        shelf = transform.parent.GetComponentInChildren<FoodShelf>();
//        StartCoroutine(CheckBoxes());
//    }

//    IEnumerator CheckBoxes()
//    {
//        Transform emptyChild = null;
//        while (true)
//        {
//            for (int i = 0; i < shelf.tempIndex; i++)
//            {
//                if (transform.GetChild(i).childCount == 0)
//                {
//                    Transform childToSwitch = null;
//                    for (int j = transform.childCount - 1; j >= 0; j--)
//                    {
//                        if (transform.GetChild(j).GetComponentInChildren<BoxPrefab>() != null)
//                        {
//                            childToSwitch = transform.GetChild(j).GetChild(0);
//                            childToSwitch.parent = transform.GetChild(i);
//                            childToSwitch.localPosition = Vector3.zero;
//                            childToSwitch.localRotation = Quaternion.identity;
//                            break;
//                        }
//                    }
//                }
//            }

//            //for (int i = 0; i < shelf.tempIndex; i++)
//            //{
//            //    if (shelf.tempIndex < 2)
//            //        break;

//            //    if (transform.GetChild(i).GetComponentInChildren<BoxPrefab>() == null)
//            //    {
//            //        if (emptyChild != transform.GetChild(i))
//            //        {
//            //            emptyChild = transform.GetChild(i);
//            //            break;
//            //        }
//            //        else
//            //        {
//            //            Transform childToSwitch = null;
//            //            for (int j = transform.childCount - 1; j >= 0; j--)
//            //            {
//            //                if (transform.GetChild(j).GetComponentInChildren<BoxPrefab>() != null)
//            //                {
//            //                    childToSwitch = transform.GetChild(j).GetChild(0);
//            //                    break;
//            //                }
//            //            }

//            //            childToSwitch.parent = transform.GetChild(i);
//            //            childToSwitch.localPosition = Vector3.zero;
//            //            childToSwitch.localPosition = Vector3.zero + Vector3.up * 0.3f;
//            //            childToSwitch.localRotation = Quaternion.identity;
//            //            //shelf.tempIndex--;
//            //            emptyChild = null;
//            //        }
//            //    }
//            //}

//            yield return new WaitForSeconds(1f);
//        }
//    }

//    void Update()
//    {

//    }
//}

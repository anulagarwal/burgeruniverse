//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CheckIChildIsMissing : MonoBehaviour
//{
//    private bool canCheck = true;

//    public bool canStart = false;

//    private void Start()
//    {
//        if (canStart || gameObject.name.Contains("Chip1"))
//            TryContinously();
//    }

//    private void TryContinously()
//    {
//        Check();
//        Invoke("TryContinously", 0.2f);
//    }

//    public void Check()
//    {
//        if (canCheck)
//        {
//            canCheck = false;
//            StartCoroutine(Checking());
//        }
//        else if (!IsInvoking("CanCheckAgain"))
//            Invoke("CanCheckAgain", 3f);
//    }

//    public void CheckLater()
//    {
//        if (!IsInvoking("CheckAgain"))
//            Invoke("CheckAgain", 0.01f);
//    }

//    private void CheckAgain()
//    {
//        StartCoroutine(Checking());
//    }

//    private void CanCheckAgain()
//    {
//        canCheck = true;
//    }

//    IEnumerator Checking()
//    {
//        Debug.Log("CHECKING");
//        for (int j = 0; j < 7; j++)
//        {
//            bool isMissing = false;
//            List<Transform> missingList = new List<Transform>();

//            for (int i = 0; i < transform.childCount; i++)
//            {
//                if (isMissing)
//                {
//                    if (transform.GetChild(i).childCount > 0)
//                    {
//                        Transform tempObj = transform.GetChild(i).GetChild(0);
//                        if (tempObj.GetComponentInChildren<BoxPrefab>() != null)
//                        {
//                            if (!tempObj.GetComponentInChildren<BoxPrefab>().movingParabola)
//                            {
//                                tempObj.parent = missingList[0];
//                                tempObj.localPosition = Vector3.zero;

//                                missingList.RemoveAt(0);
//                            }
//                        }
//                    }
//                }
//                if (transform.GetChild(i).childCount == 0)
//                {

//                    isMissing = true;
//                    missingList.Add(transform.GetChild(i));
//                }
//            }
//            yield return new WaitForSeconds(0.05f);
//        }
//    }
//}

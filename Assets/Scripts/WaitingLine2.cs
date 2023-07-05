using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingLine2 : MonoBehaviour
{
    public void StepForward()
    {
        //StartCoroutine(SteppingForward());

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Transform nextChild = transform.GetChild(i + 1);

            if (!nextChild.gameObject.name.Contains("Pos_Seat"))
                break;

            if (nextChild.childCount > 0)
            {
                if (nextChild.GetChild(0).GetComponent<Guest>() != null)
                    nextChild.GetChild(0).GetComponent<Guest>().MoveForward(transform.GetChild(i));
            }
        }
    }

    //IEnumerator SteppingForward()
    //{
    //    for (int i = 0; i < transform.childCount - 1; i++)
    //    {
    //        Transform nextChild = transform.GetChild(i + 1);

    //        if (!nextChild.gameObject.name.Contains("Pos_Seat"))
    //            break;

    //        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));


    //        if (nextChild.childCount > 0)
    //        {
    //            //if (nextChild.GetChild(0).GetComponent<Guest>() != null)
    //            //    nextChild.GetChild(0).GetComponent<Guest>().MoveForward(transform.GetChild(i));
    //        }
    //    }

    //}

    public Transform GetEmptyTransform()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            if (transform.GetChild(i).childCount == 0)
            {
                return transform.GetChild(i);
            }
        }

        return null;
    }
}

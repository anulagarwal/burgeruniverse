using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArrow : MonoBehaviour
{
    public GameObject arrow;

    void OnEnable()
    {
        if (Time.timeSinceLevelLoad > 2f)
        {
            Invoke("CheckThisArrow", 3.6f);
        }
    }

    void CheckThisArrow()
    {
        if (!arrow.activeInHierarchy)
            ArrowTutorialHolder.Instance.EnableNextTut(12);
    }
}

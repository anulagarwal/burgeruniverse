using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodUpShelf : MonoBehaviour
{
    public Transform boxesHolder;

    public Transform GetLastBox(bool isThisRegular = true)
    {
        for (int i = boxesHolder.childCount - 1; i >= 0; i--)
        {
            if (boxesHolder.GetChild(i).GetComponentInChildren<FoodBoxPrefab>() != null)
                return boxesHolder.GetChild(i).GetComponentInChildren<FoodBoxPrefab>().transform;
        }
        return null;
    }
}

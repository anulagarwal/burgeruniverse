using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodButton : MonoBehaviour
{
    public void SetChoice(int index)
    {
        ProgressBar2.Instance.ButtonPressed(index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandYesOrNo : MonoBehaviour
{
    private int tempIndex = 0;

    public string[] yesAnimNames, noAnimNames;

    public void DoChoice(bool boolean)
    {
        if (boolean)
        {
            GetComponentInChildren<Animation>().Play(yesAnimNames[tempIndex]);
        }
        else
        {
            GetComponentInChildren<Animation>().Play(noAnimNames[tempIndex]);
        }

        tempIndex++;
    }
}

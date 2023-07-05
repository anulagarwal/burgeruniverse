using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOutsideCanvasVisibilityAfterThisEnabled : MonoBehaviour
{
    public GameObject[] enableThis;

    private void OnEnable()
    {
        if (Time.timeSinceLevelLoad > 2f)
        {
            PlayerPrefs.SetInt("OutsideCanvas", 1);

            for (int i = 0; i < enableThis.Length; i++)
                enableThis[i].SetActive(true);
        }

        //FindObjectOfType<OutsideFaszomCanvasCapsuleLocker>().EnableThis();
    }
}

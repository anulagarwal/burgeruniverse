using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableShits : MonoBehaviour
{
    private bool canDo = true;
    void Start()
    {
        if (PlayerPrefs.GetInt("DisabledEverything", 0) == 0)
            DisableEverything();
        else
        {
            for (int i = 0; i < stuffs2.Length; i++)
            {
                stuffs2[i].gameObject.SetActive(false);
            }
            canDo = false;
        }
    }
    public GameObject[] stuffs, stuffs1, stuffs2, stuffs3, stuffs4, stuffs5;

    private void DisableEverything()
    {
        for (int i = 0; i < stuffs.Length; i++)
        {
            stuffs[i].gameObject.SetActive(false);
        }

        PlayerPrefs.SetInt("DisabledEverything", 1);
    }

    private void EnableEverything()
    {
        for (int i = 0; i < stuffs.Length; i++)
        {
            stuffs[i].gameObject.SetActive(true);
        }
    }

    int tempIndex = 0;

    public void DoCheck()
    {
        tempIndex = PlayerPrefs.GetInt("Enable", 0) - 1;

        if (tempIndex >= 0)
        {
            for (int i = 0; i < stuffs1.Length; i++)
            {
                stuffs1[i].gameObject.SetActive(true);
            }
        }
        if (tempIndex >= 1)
        {
            //DIDABLE BOUNDARIES
            for (int i = 0; i < stuffs2.Length; i++)
            {
                stuffs2[i].gameObject.SetActive(false);
            }
        }
        if (tempIndex >= 2)
        {
            for (int i = 0; i < stuffs3.Length; i++)
            {
                stuffs3[i].gameObject.SetActive(true);
            }
        }
        if (tempIndex >= 3)
        {
            for (int i = 0; i < stuffs4.Length; i++)
            {
                stuffs4[i].gameObject.SetActive(true);
            }
        }
        if (tempIndex >= 4)
        {
            for (int i = 0; i < stuffs5.Length; i++)
            {
                stuffs5[i].gameObject.SetActive(true);
            }
        }

        tempIndex++;
    }

    public void EnableNextThings(int ind = 0)
    {
        if (!canDo)
            return;

        if (ind > 0)
            tempIndex = ind;

        if (tempIndex == 0)
        {
            for (int i = 0; i < stuffs1.Length; i++)
            {
                stuffs1[i].gameObject.SetActive(true);
            }
        }
        else if (tempIndex == 1)
        {
            //DIDABLE BOUNDARIES
            for (int i = 0; i < stuffs2.Length; i++)
            {
                stuffs2[i].gameObject.SetActive(false);
            }
        }
        else if (tempIndex == 2)
        {
            for (int i = 0; i < stuffs3.Length; i++)
            {
                stuffs3[i].gameObject.SetActive(true);
            }
        }
        else if (tempIndex == 3)
        {
            for (int i = 0; i < stuffs4.Length; i++)
            {
                stuffs4[i].gameObject.SetActive(true);
            }
        }
        else if (tempIndex == 4)
        {
            for (int i = 0; i < stuffs5.Length; i++)
            {
                stuffs5[i].gameObject.SetActive(true);
            }
        }

        tempIndex++;

        PlayerPrefs.SetInt("Enable", tempIndex);
    }
}

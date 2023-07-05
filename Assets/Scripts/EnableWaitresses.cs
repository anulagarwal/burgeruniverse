using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWaitresses : MonoBehaviour
{
    private void OnEnable()
    {
        int countToActivate = 0;
        countToActivate = PlayerPrefs.GetInt("SecuritySpeed", 0);


        countToActivate = Mathf.Abs(countToActivate);

        if (countToActivate == 0)
            return;

        if (countToActivate > transform.childCount)
            countToActivate = transform.childCount;

        for (int i = 0; i < countToActivate; i++)
            transform.GetChild(i).gameObject.SetActive(true);
    }

    void Update()
    {

    }
}

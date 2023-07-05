using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAI : MonoBehaviour
{
    private void OnEnable()
    {
        int countToActivate = 0;
        if (gameObject.name.Contains("ChipMan"))
            countToActivate = PlayerPrefs.GetInt("ChipManCount", 0);
        else if (gameObject.name.Contains("Security"))
            countToActivate = PlayerPrefs.GetInt("SecurityCount", 0);
        else
            countToActivate = PlayerPrefs.GetInt("ChefCount", 0);

        if (countToActivate == 0)
            return;

        for (int i = 0; i < countToActivate; i++)
            transform.GetChild(i).gameObject.SetActive(true);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

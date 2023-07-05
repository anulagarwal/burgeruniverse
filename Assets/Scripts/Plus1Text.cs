using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plus1Text : MonoBehaviour
{
    public static Plus1Text Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayText(int count)
    {
        string plusString = "";
        if (count > 0)
        {
            plusString = "+ ";
            GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
            GetComponent<TextMeshProUGUI>().color = Color.green;

        GetComponent<TextMeshProUGUI>().text = plusString + count.ToString();
        GetComponent<Animation>().Play();
    }
}

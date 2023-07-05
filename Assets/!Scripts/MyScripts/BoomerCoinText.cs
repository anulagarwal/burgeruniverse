using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoomerCoinText : MonoBehaviour
{
    TextMeshProUGUI thisText;
    Animation anim;

    int tempCount = 0;

    void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animation>();
        thisText.text = "0";
    }

    public void AddCount()
    {
        anim.Play();
        tempCount++;
        thisText.text = tempCount.ToString();
    }
}

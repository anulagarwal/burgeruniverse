using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCounter : MonoBehaviour
{
    public void CheckCount(string str)
    {
        GetComponent<TextMeshProUGUI>().text = str.Length.ToString();

        PressedKey(str);
    }

    private void PressedKey(string key)
    {
        Keyboard.Instance.PressKey(key);
    }
}

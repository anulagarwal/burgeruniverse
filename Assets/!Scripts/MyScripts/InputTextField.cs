using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputTextField : MonoBehaviour
{
    public Crate changeText;

    private TMP_InputField field;


    public void DO_SEND_TEXT()
    {
        if (field.text == "")
            return;

        string tempChar = field.text;
        changeText.SetTexts(tempChar);
        Debug.Log(tempChar);
        field.text = "";
    }

    void Start()
    {
        field = GetComponent<TMP_InputField>();
    }

    void Update()
    {

    }
}

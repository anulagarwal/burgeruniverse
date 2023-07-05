using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crate : MonoBehaviour
{
    public bool isActiveCrate;

    private TextMeshPro[] texts;

    void Start()
    {
        texts = GetComponentsInChildren<TextMeshPro>();

        if (!isActiveCrate)
            transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Activate(bool canActivate, bool hideLetters = true)
    {
        isActiveCrate = canActivate;
        if (hideLetters)
            transform.GetChild(0).gameObject.SetActive(canActivate);

        if (canActivate)
            SetTexts("?");
    }

    public void SetColor(Color col)
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].color = col;
    }

    public void SetTexts(string character = "")
    {
        for (int i = 0; i < texts.Length; i++)
        {
            //if (character != "")
            if (character == texts[i].text)
                return;

            texts[i].text = character;

            if (character != "?")
                Activate(false, false);
            else
                texts[i].color = Color.white;

            //FindObjectOfType<CratesManager>().Next();
        }
    }

    void Update()
    {
        if (isActiveCrate && (Input.inputString != ""))
        {
            isActiveCrate = false;
            SetTexts(Input.inputString);
        }
    }
}

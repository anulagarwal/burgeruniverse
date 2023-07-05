using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoCountText : MonoBehaviour
{
    TextMeshProUGUI tmp;
    private Animation anim;

    public static InfoCountText burger, fries, pizza, iceCream, vip;

    private void Awake()
    {
        //switch (transform.parent.name)
        //{
        //    case "Burg":
        //        burger = this;
        //        break;
        //    case "Fries":
        //        fries = this;
        //        break;
        //    case "Pizza":
        //        pizza = this;
        //        break;
        //    case "Icecr":
        //        iceCream = this;
        //        break;
        //}

        if (transform.parent.name.Contains("Burg"))
            burger = this;
        else if (transform.parent.name.Contains("Fries"))
            fries = this;
        else if (transform.parent.name.Contains("Pizza"))
            pizza = this;
        else if (transform.parent.name.Contains("Icecr"))
            iceCream = this;
        else
            vip = this;
    }

    private void Start()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        anim = GetComponentInChildren<Animation>();

        if (PlayerPrefs.GetInt("Fries", 0) == 0 && this == fries)
            transform.parent.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("Pizza", 0) == 0 && this == pizza)
            transform.parent.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("IceCream", 0) == 0 && this == iceCream)
            transform.parent.gameObject.SetActive(false);

        if (vip == this)
            transform.parent.gameObject.SetActive(false);
    }

    int tempValue = 0;

    bool isFirstFood = true;

    public void UpdateText(int value)
    {
        if (isFirstFood)
        {
            isFirstFood = false;

            if (ArrowTutorialHolder.Instance != null)
            {
                if (this == fries)
                    ArrowTutorialHolder.Instance.EnableNextTut(7);
                else if (this == pizza)
                    ArrowTutorialHolder.Instance.EnableNextTut(8);
                else if (this == iceCream)
                    ArrowTutorialHolder.Instance.EnableNextTut(13);
                //If you want to give a tutorial for enabling a new food pawner, you can do it here..
            }
        }

        tempValue += value;

        tmp.text = tempValue.ToString();
        anim.Play();
    }
}

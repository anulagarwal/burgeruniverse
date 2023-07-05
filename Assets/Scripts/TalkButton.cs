using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public void ClickedTalkButton()
    {
        GetComponent<Animation>().Play("ChoseTalk");
        if (transform.GetSiblingIndex() == 0)
            transform.parent.GetChild(1).gameObject.SetActive(false);
        else
            transform.parent.GetChild(0).gameObject.SetActive(false);
    }

    public void ClickedChooseButton()
    {
        GetComponent<Animation>().Play("ChoseButton");
        if (transform.GetSiblingIndex() == 0)
            transform.parent.GetChild(1).gameObject.SetActive(false);
        else
            transform.parent.GetChild(0).gameObject.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

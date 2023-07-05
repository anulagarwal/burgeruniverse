using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamActivity : MonoBehaviour
{
    public GameObject tapToPickText, firstBubble;

    private void PickCardText()
    {
        tapToPickText.SetActive(true);
    }

    public void BackToStartPos()
    {
        GetComponent<Animation>().Play("CamToStartPos");
        tapToPickText.SetActive(false);
    }

    public void HideCards()
    {
        transform.parent.GetComponent<Animation>().Play();
        transform.parent = transform.parent.parent.Find("CamHolder");
        GetComponent<Animation>().Play("CamPos2");
    }

    private void ActivateFirstBubble()
    {
        firstBubble.SetActive(true);
    }

    public void CamToGolf()
    {
        GetComponent<Animation>().Play("CamPosGolf");
    }

    public void CamToGolfBack()
    {
        GetComponent<Animation>().Play("CamPosGolfBack");
    }
}

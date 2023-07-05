using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDollarManager : MonoBehaviour
{
    public static Animation tempAnim;

    public void SetTempAnim()
    {
        tempAnim = GetComponentInParent<Animation>();
        DisableButton();
    }

    private void DisableButton()
    {
        GetComponent<Button>().enabled = false;
        Invoke("EnableButton", 0.55f);
    }

    private void EnableButton()
    {
        GetComponent<Button>().enabled = true;
    }
}

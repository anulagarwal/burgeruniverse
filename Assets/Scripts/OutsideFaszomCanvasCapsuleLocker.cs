using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideFaszomCanvasCapsuleLocker : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("OutsideCanvas", 0) == 0)
            gameObject.SetActive(false);
        //GetComponent<Canvas>().sortingLayerID = 0;
    }

    public void EnableThis()
    {
        //gameObject.SetActive(false);
        //GetComponent<Canvas>().sortingLayerID = 0;
    }
}

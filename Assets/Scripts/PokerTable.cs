using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerTable : MonoBehaviour
{
    void Start()
    {
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).childCount > 0)
            {
                if (transform.GetChild(0).GetChild(0).gameObject.name.Contains("Shelf_MC") && Time.timeSinceLevelLoad < 2f)
                    Invoke("ChangeSize", 1f);
            }
        }
        //transform.localScale = Vector3.one;
    }

    private void ChangeSize()
    {
        transform.localScale = Vector3.one;
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUpperRightCorner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("UpCornerEnabled", 0) == 0)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

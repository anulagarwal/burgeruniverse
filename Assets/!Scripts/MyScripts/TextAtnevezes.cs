using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TextAtnevezes : MonoBehaviour
{

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.name = "Letter" + i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

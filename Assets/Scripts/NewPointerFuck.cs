using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPointerFuck : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeInHierarchy);
        }
    }
}

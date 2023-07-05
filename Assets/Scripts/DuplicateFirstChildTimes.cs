using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DuplicateFirstChildTimes : MonoBehaviour
{
    public int count = 20;

    void Start()
    {
        GameObject newObj;
        for (int i = 0; i < count; i++)
        {
            newObj = Instantiate(transform.GetChild(0), transform).gameObject;
            newObj.transform.localPosition = new Vector3(newObj.transform.localPosition.x, newObj.transform.localPosition.y, newObj.transform.localPosition.z * (i + 2));
        }
        DestroyImmediate(this);
    }

    void Update()
    {

    }
}

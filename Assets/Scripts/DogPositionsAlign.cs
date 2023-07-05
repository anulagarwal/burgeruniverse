using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DogPositionsAlign : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        Transform[] allChildren = transform.GetComponentsInChildren<Transform>(true);
        Transform[] allChildrenOfTarg = target.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < allChildren.Length; i++)
        {
            allChildren[i].localPosition = allChildrenOfTarg[i].localPosition;
            allChildren[i].localRotation = allChildrenOfTarg[i].localRotation;
        }
    }

    void Update()
    {

    }
}

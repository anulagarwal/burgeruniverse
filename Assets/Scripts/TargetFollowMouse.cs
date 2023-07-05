using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowMouse : MonoBehaviour
{
    void Start()
    {

    }

    public Transform targetTransform;

    void LateUpdate()
    {
        transform.position = targetTransform.position;
    }

    
}

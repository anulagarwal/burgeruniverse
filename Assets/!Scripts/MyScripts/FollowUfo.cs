using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUfo : MonoBehaviour
{
    public Transform ufoTransform;

    void Update()
    {
        transform.position = ufoTransform.position;
    }
}

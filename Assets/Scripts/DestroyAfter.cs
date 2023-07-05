using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float time;

    void Start()
    {
        Invoke("DestroyThis", time);
    }

    private void DestroyThis()
    {
        Transform shootButton;
        if (transform.Find("ShootButton"))
        {
            shootButton = transform.Find("ShootButton");
            shootButton.parent = transform.parent;
            shootButton.SetSiblingIndex(transform.GetSiblingIndex());
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHolderForEmoji : MonoBehaviour
{
    public static HandHolderForEmoji Instance;
    public Transform followThis;

    private Camera cam;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = cam.WorldToScreenPoint(followThis.position - Vector3.forward * 3f);
    }
}

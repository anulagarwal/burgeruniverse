using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryChoose : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(Random.Range(1, transform.childCount)).gameObject.SetActive(true);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

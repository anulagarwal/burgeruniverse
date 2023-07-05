using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemInsideThis : MonoBehaviour
{
    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojisHolder : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(Random.Range(0, 2)).gameObject.SetActive(true);
    }
}

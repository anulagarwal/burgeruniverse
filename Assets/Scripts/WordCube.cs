using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordCube : MonoBehaviour
{
    public void ChangeTexts(string newText)
    {
        foreach (TextMeshPro textMesh in transform.GetComponentsInChildren<TextMeshPro>())
        {
            textMesh.text = newText;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Ground"))
        {
            transform.parent = null;
            GetComponent<Collider>().isTrigger = false;
        }

    }
}

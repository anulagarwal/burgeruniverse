using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorAddParent : MonoBehaviour
{
    public Transform parent;
    public bool addAsChild = false;

    void Update()
    {
        if (addAsChild)
        {
            addAsChild = false;
            transform.parent = parent;
            DestroyImmediate(this);
        }
    }
}

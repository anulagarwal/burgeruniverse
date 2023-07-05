using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetTransform.position);
        transform.Rotate(Vector3.right, 90f);
        transform.Rotate(Vector3.up, -90f);
    }
}

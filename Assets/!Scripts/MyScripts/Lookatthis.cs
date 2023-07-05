using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookatthis : MonoBehaviour
{
    public Transform target;
    public Vector2 clamp, clampX;
    public float offsetY = 1f, offsetX = 1f;

    void LateUpdate()
    {
        if (!Input.GetMouseButton(0))
            return;

        Vector3 newPos = new Vector3(Mathf.Clamp(target.position.x + offsetX, clampX.x, clampX.y), Mathf.Clamp(target.position.y + offsetY, clamp.x, clamp.y), transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 2f * Time.deltaTime);
        //transform.position = new Vector3(Mathf.Clamp(target.position.x - transform.position.x, clampX.x, clampX.y), Mathf.Clamp(target.position.y + offsetY, clamp.x, clamp.y), transform.position.z);
    }
}

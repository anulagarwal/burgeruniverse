using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight2 : MonoBehaviour
{
    public GameObject shootButton;
    public Transform boyHead;

    void Start()
    {

    }

    void Update()
    {
        if (!GetComponent<MeshRenderer>().enabled)
            return;

        Ray ray = new Ray(transform.parent.position, boyHead.position - transform.parent.position);
        RaycastHit hitInfo;

        float rayLength = 10f;

        if (Physics.Raycast(ray, out hitInfo, rayLength /*, mask, QueryTriggerInteraction.Ignore*/))
        {
            //Debug.Log("COLLIDED WITH: " + hitInfo.collider.gameObject.name);

            if (hitInfo.collider.gameObject.name.Contains("Fluid"))
            {
                shootButton.SetActive(false);
                EmojiMoveUp.Instance.Increase();
            }

            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.Log("NO COLLISION");

            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.green);
        }

    }
}

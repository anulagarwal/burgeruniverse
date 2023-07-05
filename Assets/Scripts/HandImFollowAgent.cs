using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandImFollowAgent : MonoBehaviour
{
    public Transform agentTransform;
    public float speed = 100f;
    private Camera cam;

    void Start()
    {
        //agentTransform = FindObjectOfType<AgentRun>().transform;
        cam = Camera.main;
    }

    void Update()
    {
        //transform.position = new Vector2(cam.WorldToScreenPoint(agentTransform.position + Vector3.up * 1f).x, transform.position.y);
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
        }
    }
}

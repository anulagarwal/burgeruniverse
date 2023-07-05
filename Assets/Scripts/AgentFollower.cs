using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFollower : MonoBehaviour
{
    private Transform hand;
    private Vector3 offsetPos;

    private void Start()
    {
        if (gameObject.name.Contains("PlayerRun"))
            hand = FindObjectOfType<AgentRun>().transform;
        else
            hand = FindObjectOfType<HandRun>().transform;
        transform.parent = null;
        offsetPos = transform.position - hand.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + offsetPos.x, transform.position.y, hand.position.z + offsetPos.z);
    }
}

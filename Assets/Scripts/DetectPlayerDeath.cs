using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerDeath : MonoBehaviour
{
    private Transform followChar;

    void Start()
    {
        followChar = transform.parent.GetChild(0);
    }

    void Update()
    {
        transform.position = followChar.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Ground"))
        {
            if (!other.gameObject.name.Contains("STEP"))
            {
                transform.parent.GetComponentInChildren<TypeRunCharacter>().KillCharacter();
                transform.GetChild(0).parent = null;
                Destroy(gameObject);
            }
            else
            {
                transform.GetComponentInParent<CubesHolder>().canMove = false;
                transform.parent.GetComponentInChildren<TypeRunCharacter>().Win();
                transform.GetChild(0).parent = null;
                Destroy(gameObject);
            }
        }
    }
}

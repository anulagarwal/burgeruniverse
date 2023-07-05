using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hip : MonoBehaviour
{
    private bool canMove = false;

    public Transform attractPos;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attractPos = FindObjectOfType<Attract>().transform;
    }

    public void StartLevitating()
    {
        attractPos = Attract.Instance.AddToAttract(this);
        canMove = true;
    }

    public void StartMoveTowardEnemy(Transform enemyPos)
    {
        attractPos = enemyPos;
        canMove = true;
    }

    public void StopLevitating()
    {
        Attract.Instance.DeleteAttraction(attractPos);
    }

    void LateUpdate()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, attractPos.position, 11f * Time.deltaTime);
            rb.Sleep();
            rb.AddTorque(new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2)));
            //GetComponent<Rigidbody>().MovePosition(attractPos.position);
        }
    }
}

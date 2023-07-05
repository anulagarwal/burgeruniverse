using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMoving : MonoBehaviour
{
    public float rotSpeed = 10f, moveSpeed = 10f;

    public GameObject handTutCanv;

    [HideInInspector] public bool canMove = true;

    private void Start()
    {
        handTutCanv.SetActive(true);
        canMove = false;
        Invoke("StartMove", 1.2f);
        Invoke("DisableHandTut", 3.7f);
    }

    private void StartMove()
    {
        canMove = true;
    }

    private void DisableHandTut()
    {
        handTutCanv.SetActive(false);
    }

    void Update()
    {
        if (!canMove)
            return;

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotSpeed * Time.deltaTime, Space.World);
            //transform.Rotate(Vector3.right, -rotSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime, Space.World);
            //transform.Rotate(Vector3.right, rotSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.forward, -rotSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void MoveBack()
    {
        StartCoroutine(MovingBack());
    }


    public float moveBackBy = 1.15f, speedOfMovingBack = 1.2f;

    IEnumerator MovingBack()
    {
        Vector3 target = transform.position - transform.up * moveBackBy;
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speedOfMovingBack * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        transform.position = target;
        canMove = true;
    }

}

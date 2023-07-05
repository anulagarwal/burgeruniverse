using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCoin : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        targetPos = transform.position;
        tempMoveX = transform.position.x;

    }

    public void StartMoving()
    {
        if (gameObject.name.Contains("Face"))
        {
            return;
        }

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (Vector2.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 1600f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        //
        target.GetComponent<MovingCoin>().Move(transform.parent.name.Contains("Happy"));
        //

        GetComponent<Animation>().Play();
    }



    public float moveBy = 2f, tempMoveX = 0f, speed = 50f;

    public void Move(bool isRight)
    {
        Debug.Log(isRight);

        if (isRight)
            tempMoveX += moveBy;
        else
            tempMoveX -= moveBy;

        targetPos = new Vector3(tempMoveX, transform.position.y, transform.position.z);
    }

    Vector3 targetPos;

    void LateUpdate()
    {
        if (!gameObject.name.Contains("Face"))
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position.x > (512 + 25))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (transform.position.x < (512 - 25))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        Debug.Log(transform.position);
    }
}

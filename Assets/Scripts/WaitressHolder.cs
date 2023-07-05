using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitressHolder : MonoBehaviour
{
    public Transform[] targets;
    public int[] posIndexes;

    private Transform tempTarget;

    public int tempPosIndex = 0;

    public float rotSpeed = 3.5f, moveSpeed = 3f;

    void Start()
    {
        Invoke("StartMoving", Random.Range(3f, 5f));
    }

    void StartMoving()
    {
        tempTarget = targets[posIndexes[tempPosIndex]];
        tempPosIndex++;
        StartCoroutine(RotateToward());

        Debug.Log("START MOVING ->" + tempPosIndex);
    }


    IEnumerator RotateToward(bool isLastRotation = false)
    {

        yield return new WaitForSeconds(0.1f);

        Debug.Log("ROTATING TO " + isLastRotation);

        Quaternion targetRot = tempTarget.rotation;

        if (!isLastRotation)
        {
            Quaternion tempRotation = transform.rotation;
            transform.LookAt(tempTarget.position);
            targetRot = transform.rotation;
            transform.rotation = tempRotation;
        }

        while (Quaternion.Angle(transform.rotation, targetRot) > 2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        transform.rotation = targetRot;

        Debug.Log("ROTATING TO " + isLastRotation + " STOPPED--------");

        if (!isLastRotation)
            StartCoroutine(MoveToward());
        else
        {
            Invoke("StartMoving", Random.Range(3f, 5f));
            StopAllCoroutines();
        }
    }


    IEnumerator MoveToward()
    {
        Debug.Log("MOVING");

        GetComponent<Animator>().SetBool("walk", true);
        while ((Vector3.Distance(transform.position, tempTarget.position) > 0.05f))
        {
            transform.position = Vector3.MoveTowards(transform.position, tempTarget.position, moveSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        transform.position = tempTarget.position;
        GetComponent<Animator>().SetBool("walk", false);

        StartCoroutine(RotateToward(true));

        Debug.Log("MOVING STOPPED-----");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MoveTarget : MonoBehaviour
{
    public bool canFollowPart = true;
    private bool isFirst = true;

    private Transform childTransform;

    public Transform lookAtThis, followThis;

    private Vector3 startLocalPosOfChild;

    private float weightAtStart, weightAtStart2;

    public MultiAimConstraint[] constraints;

    public void SetWeightDown()
    {
        //if (constraints.Length > 0)
        //{
        //    weightAtStart = constraints[0].weight;
        //    weightAtStart2 = constraints[1].weight;
        //    constraints[0].weight = 0f;
        //    constraints[1].weight = 0f;
        //}

        //if (GetComponentInParent<TwoBoneIKConstraint>() != null)
        //{
        //    weightAtStart = GetComponentInParent<TwoBoneIKConstraint>().weight;
        //    GetComponentInParent<TwoBoneIKConstraint>().weight = 0f;
        //}
    }

    void Start()
    {
        //SetWeightDown();

        childTransform = transform.GetChild(0);
        startLocalPosOfChild = childTransform.localPosition;

        if (transform.GetComponentInParent<TwoBoneIKConstraint>() != null)
        {
            lookAtThis = transform.GetComponentInParent<TwoBoneIKConstraint>().data.mid.transform;
            followThis = transform.GetComponentInParent<TwoBoneIKConstraint>().data.tip.transform;
        }

        childTransform.parent = followThis;
        childTransform.localPosition = Vector3.zero;
    }

    public LayerMask wallMask;

    private bool isTouching = false;

    public float degree;
    public bool x, y, z;

    public void CheckIfMovedAway()
    {
        //if (transform.GetComponentInParent<TwoBoneIKConstraint>() == null)
        //    return;

        //if (transform.position != followThis.position)
        //    StartCoroutine(MoveToTarget());
    }


    IEnumerator MoveToTarget()
    {
        Vector3 targetPos = followThis.position;

        while ((Vector3.Distance(childTransform.position, targetPos) > 0.01f))
        {
            childTransform.position = Vector3.MoveTowards(childTransform.position, targetPos, 3.5f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        transform.position = followThis.position;
        childTransform.localPosition = startLocalPosOfChild;
    }


    void Update()
    {
        childTransform.localPosition = Vector3.zero;

        if (canFollowPart)
        {
            transform.position = followThis.position;
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, wallMask))
        {
            Debug.Log("POINTER IS AT: " + hitInfo.collider.gameObject.name);

            //hitInfo.collider.gameObject

            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }


        if ((Input.GetMouseButtonDown(0)) && ((Vector3.Distance(transform.position, hitInfo.point) < 0.3f)))
        {
            if (isFirst)
            {
                isFirst = false;

                if (constraints.Length > 0)
                {
                    constraints[0].GetComponent<Animation>().Play();
                    constraints[1].GetComponent<Animation>().Play();
                }

                if (GetComponentInParent<TwoBoneIKConstraint>() != null)
                    GetComponentInParent<TwoBoneIKConstraint>().GetComponent<Animation>().Play();
            }

            isTouching = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isTouching)
                CheckIfMovedAway();

            isTouching = false;
        }

        if (Input.GetMouseButton(0) && isTouching)
        {
            transform.position = Vector3.MoveTowards(transform.position, hitInfo.point, 4.5f * Time.deltaTime);
        }



        if (transform.GetComponentInParent<TwoBoneIKConstraint>() != null)
            transform.rotation = lookAtThis.rotation;
        //transform.LookAt(lookAtThis);
        //if (z)
        //    transform.Rotate(transform.forward, degree);
        //if (y)
        //    transform.Rotate(transform.up, degree);
        //if (x)
        //    transform.Rotate(transform.right, degree);
    }
}

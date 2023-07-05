using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TargetYesOrNo : MonoBehaviour
{
    public TwoBoneIKConstraint hand;
    public MultiAimConstraint chest, head;

    public void WeightToZero()
    {
        //transform.parent.GetComponent<Animation>().Play("ZeroWeight");
        StartCoroutine(WeightDown());
    }

    public Transform pepperTarget, pepperGameobject;

    public void GetPepper()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPepper(pepperTarget));
    }

    private void NoShakingHand()
    {
        hand.GetComponent<Animation>().Stop();
        hand.data.targetPositionWeight = 1f;
    }

    public Transform intoMouthTransform;

    public void DropPepper()
    {
        transform.GetChild(0).GetChild(0).gameObject.AddComponent<Rigidbody>();

        Invoke("ButtonChoosingIdle", 2f);
    }

    IEnumerator MoveToPepper(Transform targetTransform)
    {
        NoShakingHand();
        transform.position = hand.data.tip.transform.position;
        PepperMoveWeightBack();
        GetComponent<Animation>().Stop();

        Vector3 targetPos = targetTransform.position;
        Quaternion targetRot = targetTransform.rotation;

        while ((Vector3.Distance(transform.position, targetPos) > 0.02f) && (transform.rotation != targetRot))
        {
            //PepperMoveWeightBack();

            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.6f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 1.3f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        transform.position = targetPos;
        transform.rotation = targetRot;

        pepperGameobject.parent = transform.GetChild(0);
        transform.GetChild(0).GetChild(0).localPosition = Vector3.zero;
        transform.GetChild(0).GetChild(0).localRotation = Quaternion.identity;

        //MOVING TO MOUTH----------------------------------------------------------------------
        targetPos = intoMouthTransform.position;
        targetRot = intoMouthTransform.rotation;

        while ((Vector3.Distance(transform.position, targetPos) > 0.001f) && (transform.rotation != targetRot))
        {
            //PepperMoveWeightBack();

            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.2f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 2f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        transform.position = targetPos;
        transform.rotation = targetRot;

        FindObjectOfType<HandGirlManager>().Eat();
        //FindObjectOfType<HandGirlManager>().PlayHot();
    }


    IEnumerator WeightDown(bool isUp = true)
    {
        if (!isUp)
        {
            while (hand.weight != 1f && chest.weight != 1f && head.weight != 1f)
            {
                hand.weight += 1f * Time.deltaTime;
                chest.weight += 1f * Time.deltaTime;
                head.weight += 1f * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (hand.weight != 0f && chest.weight != 0f && head.weight != 0f)
            {
                hand.weight -= 1f * Time.deltaTime;
                chest.weight -= 1f * Time.deltaTime;
                head.weight -= 1f * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void ButtonChooseIdleInvoke()
    {
        Invoke("ButtonChoosingIdle", 0.5f);
    }

    private void ButtonChoosingIdle()
    {
        GetComponent<Animation>().Play();

        hand.GetComponent<Animation>().Play();

        StartCoroutine(WeightDown(false));
    }

    private void PepperMoveWeightBack()
    {
        StartCoroutine(WeightDown(false));
    }

    public void StartPushingButton(Vector3 targetPos, Animation buttonAnim)
    {
        GetComponent<Animation>().Stop();
        StartCoroutine(MoveToTarget(targetPos, buttonAnim));
    }


    IEnumerator MoveDown()
    {
        Vector3 targetPos = transform.position - Vector3.down * 10f;
        Transform transformToMove = transform;

        while ((Vector3.Distance(transformToMove.position, targetPos) > 0.01f))
        {
            transformToMove.position = Vector3.MoveTowards(transformToMove.position, targetPos, 0.1f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        Vector3 targetPos = transform.position - Vector3.up * 0.9f;
        Transform transformToMove = transform;

        while ((Vector3.Distance(transformToMove.position, targetPos) > 0.01f))
        {
            transformToMove.position = Vector3.MoveTowards(transformToMove.position, targetPos, 0.6f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MoveToTarget(Vector3 target, Animation anim)
    {
        Vector3 targetPos = target;
        Transform transformToMove = transform;

        while (Vector3.Distance(transformToMove.position, targetPos) > 0.01f)
        {
            transformToMove.position = Vector3.MoveTowards(transformToMove.position, targetPos, 0.4f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        anim.Play();

        StartCoroutine(MoveDown());
    }
}

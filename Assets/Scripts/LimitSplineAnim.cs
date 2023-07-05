using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LimitSplineAnim : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform sourceTransform;
    public float addWeightAfterLessThan, maxWieghtAt = 0.1f;

    private MultiAimConstraint constraint;

    void Start()
    {
        constraint = GetComponent<MultiAimConstraint>();
    }

    void Update()
    {
        if (sourceTransform.localPosition.y < addWeightAfterLessThan)
        {
            float tempWeight = curve.Evaluate(sourceTransform.localPosition.y);
            constraint.weight = tempWeight;
        }
        else
        {
            constraint.weight = 0f;
        }
    }
}

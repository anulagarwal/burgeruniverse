using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MultiPositionPlayer : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform sourceTransform;
    public float addWeightAfterLessThan, maxWieghtAt = 0.1f;

    private MultiPositionConstraint constraint;

    void Start()
    {
        constraint = GetComponent<MultiPositionConstraint>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyBar : MonoBehaviour
{
    Image barIm;

    public AnimationCurve curve;

    public Vector3 targetPos;

    public float startDistance, tempDistance;

    void Start()
    {
        barIm = Bar.barImage;
        startDistance = Vector3.Distance(transform.position, targetPos);
    }

    void Update()
    {
        tempDistance = Vector3.Distance(transform.position, targetPos);
        barIm.fillAmount = curve.Evaluate(tempDistance);
    }
}

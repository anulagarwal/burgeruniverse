using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamSphere : MonoBehaviour
{
    private Vector3 scaleGoal;

    private bool canMelt = false, canGrow = true;

    void Start()
    {
        scaleGoal = transform.localScale;
        transform.localScale = Vector3.zero;

        Invoke("StopGrowing", 2f);
    }

    private void StopGrowing()
    {
        canGrow = false;
    }

    public void Melt()
    {
        if (Random.Range(0, 9) < 8)
            canMelt = true;
    }

    void Update()
    {
        if (canGrow)
            transform.localScale = Vector3.LerpUnclamped(transform.localScale, scaleGoal, 5f * Time.deltaTime);

        if (canMelt)
            transform.localScale = Vector3.LerpUnclamped(transform.localScale, Vector3.zero, 5f * Time.deltaTime);
    }
}

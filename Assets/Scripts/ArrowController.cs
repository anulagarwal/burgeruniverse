using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Vector3 target;

    public static ArrowController vip, tut;

    private void Awake()
    {
        if (gameObject.name.Contains("VIP"))
            vip = this;
        else
            tut = this;
    }

    public void LookAtThis(Transform targ)
    {
        target = new Vector3(targ.position.x, transform.position.y, targ.position.z);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void StopLookingAt()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        transform.LookAt(target);
    }
}

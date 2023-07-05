using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNextThing : MonoBehaviour
{
    public int index = 0;
    public float afterSecs = 0f;
    void OnEnable()
    {
        if (Time.timeSinceLevelLoad > 1f)
            Invoke("EnableThings", afterSecs);
    }

    private void EnableThings()
    {
        FindObjectOfType<EnableDisableShits>().EnableNextThings(index);
    }

    void Update()
    {

    }
}

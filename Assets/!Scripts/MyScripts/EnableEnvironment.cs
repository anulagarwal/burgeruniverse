using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnvironment : MonoBehaviour
{
    public void EnableEnv()
    {
        e1.SetActive(true);
        e2.SetActive(true);
    }
    public GameObject e1, e2;
}

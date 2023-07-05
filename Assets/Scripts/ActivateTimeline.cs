using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ActivateTimeline : MonoBehaviour
{
    public GameObject timeline;
    void Start()
    {
        timeline.SetActive(true);
    }

    void Update()
    {

    }
}

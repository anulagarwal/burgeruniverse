using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutImage : MonoBehaviour
{
    private void StopGameObject()
    {
        GetComponent<Animation>().Play("TutImageEnd");
    }

    private void StartCountingDown()
    {
        Invoke("StopGameObject", 1.3f);
    }

    private void DisableThis()
    {
        gameObject.SetActive(false);
    }
}

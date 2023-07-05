using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        foreach (MoveForward moveForwardScript in FindObjectsOfType<MoveForward>())
        {
            moveForwardScript.enabled = false;
        }


        FindObjectOfType<MobileJoystick>().axisValue = new Vector2(0f, 0f);
        FindObjectOfType<AnimalChange>().gameObject.SetActive(false);

        if (other.transform.position.x > -1.2f)
        {
            GameObject.FindGameObjectWithTag("Confettis").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ClearedPanel").transform.GetChild(0).gameObject.SetActive(true);
        }
        else
            GameObject.FindGameObjectWithTag("FailedPanel").transform.GetChild(0).gameObject.SetActive(true);

        FindObjectOfType<ProgressBar>().gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMeUp : MonoBehaviour
{

    void Start()
    {

    }

    public GameObject raycastToDisable, raycastToEnable, disableBarChanger, enableBarChanger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Hand"))
        {
            FindObjectOfType<HitmanTaskManager>().ActivateNextText();
            disableBarChanger.GetComponent<ModifyBar>().enabled = false;
            enableBarChanger.GetComponent<HandSensorBar>().enabled = true;
            raycastToDisable.SetActive(false);
            raycastToEnable.SetActive(true);
            other.transform.Find(gameObject.name).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSensor : MonoBehaviour
{
    //[HideInInspector] public Joystick joystick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Destroy(Instantiate(Resources.Load("+1Text") as GameObject, other.gameObject.transform.position + Vector3.up * 2.2f, Quaternion.identity), 2f);
            other.GetComponent<Player>().EnableRagdoll(true);
            other.gameObject.tag = "Player";
        }
        else if (other.gameObject.name.Contains("Hip") && gameObject.name.Contains("Enemy"))
        {
            other.GetComponent<Hip>().StartMoveTowardEnemy(transform);
            Destroy(Instantiate(Resources.Load("-1Text") as GameObject, new Vector3(other.gameObject.transform.position.x, 2.2f, other.gameObject.transform.position.z), Quaternion.identity), 2f);
            other.GetComponentInParent<Player>().gameObject.tag = "Untagged";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject.FindGameObjectWithTag("Panels").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Confettis").transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject.FindGameObjectWithTag("Panels").transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitersSensors : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    int tempIndex = 0;
    public GameObject spawner, failedCam;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            if (tempIndex == 0)
            {
                spawner.SetActive(true);
                tempIndex++;
            }
            else if (tempIndex == 1)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                tempIndex++;
            }
            else if (tempIndex == 2)
            {
                failedCam.SetActive(true);
                FindObjectOfType<PlayerMcDonalds>().GetComponentInChildren<Animator>().SetBool("failed", true);
                tempIndex++;
            }
        }
    }
}

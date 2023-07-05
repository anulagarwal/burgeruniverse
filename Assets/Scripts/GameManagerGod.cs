using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGod : MonoBehaviour
{


    void Start()
    {

    }

    public GameObject memory, happy;
    private int index = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {

            if (index == 0)
            {
                memory.SetActive(true);
            }
            else if (index == 1)
            {
                memory.SetActive(false);
            }
            else if (index == 2)
            {
                happy.SetActive(true);
            }
            index++;

        }

    }
}

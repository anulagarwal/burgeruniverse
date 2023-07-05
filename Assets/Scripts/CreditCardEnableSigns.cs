using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCardEnableSigns : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject[] objectsToEnable;
    private int i = 0;

    public GameObject tutDown, tutUp;

    private void Tut2()
    {
        tutUp.SetActive(true);
    }

    private void Cleared()
    {
        tutUp.SetActive(false);
    }

    private void EnableFasz()
    {
        if (i == 0)
            tutDown.SetActive(false);

        objectsToEnable[i].SetActive(false);
        i++;
    }
}

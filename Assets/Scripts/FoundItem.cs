using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundItem : MonoBehaviour
{
    public bool isHappy = true;

    private void GirlReaction()
    {
        FindObjectOfType<GirlStuff>().React(isHappy);
    }

    public GameObject table;

    private void EnableTable()
    {
        table.SetActive(true);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

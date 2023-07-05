using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerCapacity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void SetPlayer()
    {
        PlayerPrefs.SetInt("PlayerCapacityNew", PlayerPrefs.GetInt("PlayerCapacityNew", 3) + 1);

    }

    // Update is called once per frame
    void Update()
    {

    }
}

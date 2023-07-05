using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class McPlayerUpgrade : MonoBehaviour
{
    public string playerPrefStringCapacity, playerPrefStringSpeed;

    public bool doesSpeedChange, doesCapacityChange;

    void Start()
    {
        Invoke("SetCapacity", 2f);
    }

    private void SetCapacity()
    {
        GetComponentInChildren<FoodHolderPlayers>().capacity = PlayerPrefs.GetInt("PlayerCapacityNew", 3);
    }
}

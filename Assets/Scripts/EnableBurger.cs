using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBurger : MonoBehaviour
{
    public GameObject burgers;

    public bool isEnable = true;
    public bool canSetShelfPlayerPrefs = false;
    public int shelfIndex;

    public string playerp = "Outdoor";

    private void OnEnable()
    {
        if (Time.timeSinceLevelLoad > 5f)
        {
            PlayerPrefs.SetInt(playerp, 1);
            burgers.SetActive(isEnable);


            if (canSetShelfPlayerPrefs && (FindObjectOfType<ChipMan>() != null))
            {
                PlayerPrefs.SetInt("Shelf" + shelfIndex.ToString(), 1);
                FindObjectOfType<EnableShelfs>().CheckWhatToEnable();
            }
        }
    }
}

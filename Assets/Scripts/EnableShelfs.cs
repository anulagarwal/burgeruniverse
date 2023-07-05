using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShelfs : MonoBehaviour
{
    public GameObject[] shelfs;

    private void OnEnable()
    {
        CheckWhatToEnable();
    }

    public void CheckWhatToEnable()
    {
        if (PlayerPrefs.GetInt("Shelf0", 0) == 1)
            shelfs[0].SetActive(true);
        if (PlayerPrefs.GetInt("Shelf1", 0) == 1)
        {
            shelfs[0].SetActive(true);
            shelfs[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Shelf2", 0) == 1)
        {
            shelfs[0].SetActive(true);
            shelfs[1].SetActive(true);
            shelfs[2].SetActive(true);
        }
    }
}

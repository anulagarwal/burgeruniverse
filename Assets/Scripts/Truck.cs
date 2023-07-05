using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    private bool isOut = false;

    public Transform boxes;
    public GameObject[] flasks;

    private void OnEnable()
    {
        if (Time.timeSinceLevelLoad > 6f)
            PlayerPrefs.SetInt("FirstDrink", 1);
    }

    IEnumerator Start()
    {
        FillUp();

        yield return new WaitForSeconds(10f);

        while (true)
        {
            yield return new WaitForSeconds(0.75f);
            if ((GetComponentsInChildren<FoodBoxPrefab>().Length == 0) && !isOut)
            {
                isOut = true;
                GetComponent<Animation>().Play("TruckOut");
            }
        }
    }

    private void FillUp()
    {
        int flaskIndex = 0;
        for (int i = 0; i < boxes.childCount; i++)
        {
            GameObject flask = Instantiate(flasks[flaskIndex], boxes.GetChild(i));
            flask.transform.localPosition = Vector3.zero;
            flask.transform.localRotation = Quaternion.identity;
            flaskIndex++;
            if (flaskIndex >= 3)
                flaskIndex = 0;
        }


        GetComponent<Animation>().Play();
        isOut = false;
    }
}

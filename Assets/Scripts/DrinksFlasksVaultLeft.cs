using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksFlasksVaultLeft : MonoBehaviour
{
    public float timeBetweenConverts = 2f;

    void OnEnable()
    {
        StartCoroutine(Convert());
    }

    public FoodUpShelf foodShelf;

    IEnumerator Convert()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenConverts);

            //if (boxesBag.GetComponentInChildren<BoxPrefab>() != null)
            //{
            Transform boxToGet = foodShelf.GetLastBox();

            if (boxToGet != null)
            {
                //yield return new WaitForSeconds(1.1f);

                //boxToGet = foodShelf.GetLastBox();

                if (boxToGet != null)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).GetComponentInChildren<FoodBoxPrefab>() == null)
                            boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(i));
                    }
                }
            }
        }
    }
}
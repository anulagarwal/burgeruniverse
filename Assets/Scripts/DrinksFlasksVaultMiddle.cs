using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class DrinksFlasksVaultMiddle : MonoBehaviour
{
    public Transform[] liquidsHolder;
    float[] liquidValues;

    public float timeBetweenConverts = 2f;

    void OnEnable()
    {
        liquidValues = new float[3];

        //int i = 0;
        //foreach (LiquidVolume liquid in GetComponentsInChildren<LiquidVolume>())
        //{
        //    liquidsHolder[i] = liquid;
        //    i++;
        //}

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
                    if (boxToGet.name.Contains("BoxCylinder1"))
                    {
                        if (transform.GetChild(2).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                        {
                            if (transform.GetChild(0).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                            {
                                if (transform.GetChild(1).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                                    boxToGet = null;
                                else
                                    boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(1));
                            }
                            else
                                boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(0));
                        }
                        else
                            boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(2));
                    }
                    else if (boxToGet.name.Contains("BoxCylinder2"))
                    {
                        if (transform.GetChild(0).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                        {
                            if (transform.GetChild(1).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                            {
                                if (transform.GetChild(2).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                                    boxToGet = null;
                                else
                                    boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(2));
                            }
                            else
                                boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(1));
                        }
                        else
                            boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(0));
                    }
                    else if (boxToGet.name.Contains("BoxCylinder3"))
                    {
                        if (transform.GetChild(1).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                        {
                            if (transform.GetChild(0).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                            {
                                if (transform.GetChild(2).GetComponentsInChildren<FoodBoxPrefab>().Length >= 3)
                                    boxToGet = null;
                                else
                                    boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(2));
                            }
                            else
                                boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(0));
                        }
                        else
                            boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform.GetChild(1));
                    }
                }
            }
        }
    }

    public void IncreaseLiquid(int index)
    {
        liquidValues[index] += 0.333f;
    }

    public void DecreaseLiquid(int index)
    {
        liquidValues[index] -= 0.333f;
    }


    private void LateUpdate()
    {
        for (int i = 0; i < liquidsHolder.Length; i++)
        {
            liquidValues[i] = Mathf.Clamp(liquidValues[i], 0.02f, 0.965f);

            liquidsHolder[i].localScale = new Vector3(liquidsHolder[i].localScale.x, Mathf.Lerp(liquidsHolder[i].localScale.y, liquidValues[i], 2f * Time.deltaTime), liquidsHolder[i].localScale.z);
        }
    }
}

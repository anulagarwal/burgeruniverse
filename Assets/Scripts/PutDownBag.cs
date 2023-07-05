using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutDownBag : MonoBehaviour
{
    public Transform boxesHolder;

    public int maxBoxesCount = 0;

    private ExchangeIcon exchange;

    public string saveName;
    public UpgradeButtonNew upgradeManager;

    IEnumerator Start()
    {
        if (saveName != "")
            maxBoxesCount = upgradeManager.GetUpgradeValue();

        exchange = transform.parent.GetComponentInChildren<ExchangeIcon>();
        if (exchange == null)
            StopAllCoroutines();
        else
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                exchange.CheckIfFullOrFree(maxBoxesCount, boxesHolder.GetComponentsInChildren<FoodBoxPrefab>().Length);
            }
        }
    }


    public Transform TryToDropDownBagToEmptySpace()
    {
        if (exchange != null)
        {
            int countOfBoxes = 0;
            for (int i = 0; i < boxesHolder.childCount; i++)
            {
                if (boxesHolder.GetChild(i).childCount >= 1)
                    countOfBoxes++;
            }

            if (countOfBoxes >= maxBoxesCount)
            {
                exchange.CheckIfFullOrFree(maxBoxesCount, boxesHolder.GetComponentsInChildren<FoodBoxPrefab>().Length);
                return null;
            }
            else
            {
                for (int i = 0; i < boxesHolder.childCount; i++)
                {
                    if (boxesHolder.GetChild(i).childCount == 0)
                        return boxesHolder.GetChild(i);
                }
            }
        }

        for (int i = 0; i < boxesHolder.childCount; i++)
        {
            if (boxesHolder.GetChild(i).childCount == 0)
                return boxesHolder.GetChild(i);
        }
        return null;
    }

    public int DestroyFoods()
    {
        int foodCount = boxesHolder.GetComponentsInChildren<FoodBoxPrefab>().Length;

        foreach (FoodBoxPrefab box in boxesHolder.GetComponentsInChildren<FoodBoxPrefab>())
        {
            Destroy(box.gameObject);
        }

        return foodCount;
    }
}

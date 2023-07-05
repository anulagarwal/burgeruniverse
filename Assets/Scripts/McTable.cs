using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class McTable : MonoBehaviour
{
    List<bool> isEmpty = new List<bool>();

    private bool animIsUp = false;
    public bool isVIP = false;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name.Contains("Seat"))
                isEmpty.Add(true);
        }

        if (isVIP)
        {
            delivered0 = delivered1 = delivered2 = false;
        }

        StartCoroutine(CheckIfNeedsFood());
    }

    IEnumerator CheckIfNeedsFood()
    {
        while (true)
        {
            if (animIsUp && !DoesNeedFood())
            {
                animIsUp = false;
            }
            else if (!animIsUp && DoesNeedFood())
            {
                animIsUp = true;
            }

            yield return new WaitForSeconds(0.4f);
        }
    }

    public bool DoesNeedFood()
    {
        bool needFood = false;
        foreach (Guest guest in GetComponentsInChildren<Guest>())
        {
            if (guest.needFood)
            {
                needFood = true;
                break;
            }
        }

        if (isVIP)
            needFood = !(delivered0 && delivered1 && delivered2);

        return needFood;
    }

    public void VipDelivered(int index)
    {
        if (index == 0)
            delivered0 = true;
        else if (index == 1)
            delivered1 = true;
        else if (index == 2)
            delivered2 = true;

        if (delivered0 && delivered1 && delivered2)
            transform.GetComponentInChildren<Guest>().StartEating();
    }

    bool canWaiter = true;
    int waiterC;

    public bool delivered0 = false, delivered1 = false, delivered2 = false;
    public int tempNeededFoodIndex;

    public Transform GetFreeSpaceForWaiter(int countOfBoxes, bool isFirstTry)
    {
        if (isFirstTry)
            canWaiter = true;

        if (canWaiter)
        {
            canWaiter = false;
            waiterC = countOfBoxes;
        }

        if (waiterC <= 0)
            return null;

        foreach (Guest guest in GetComponentsInChildren<Guest>())
        {
            if (guest.needFood)
            {
                guest.StartEating();
                guest.needFood = false;
                waiterC--;
                return guest.transform.parent.GetChild(0);
            }
        }
        return null;
    }

    public int GetVipNeededIndex(bool has0, bool has1, bool has2)
    {
        if (has0 && !delivered0)
            tempNeededFoodIndex = 0;
        else if (has1 && !delivered1)
            tempNeededFoodIndex = 1;
        else
            tempNeededFoodIndex = 2;

        return tempNeededFoodIndex;
    }

    public Transform GetFreeSpace(bool has0, bool has1, bool has2, bool has3)
    {

        if (isVIP)
        {
            if (has0 && !delivered0)
                tempNeededFoodIndex = 0;
            else if (has1 && !delivered1)
                tempNeededFoodIndex = 1;
            else
                tempNeededFoodIndex = 2;

            foreach (Guest guest in GetComponentsInChildren<Guest>())
            {
                if (!(delivered0 && delivered1 && delivered2))//NEEDS FOOD
                {
                    if (tempNeededFoodIndex == 0 && has0 && !delivered0)
                    {
                        return guest.transform.parent.GetChild(1);
                    }
                    else if (tempNeededFoodIndex == 1 && has1 && !delivered1)
                    {
                        return guest.transform.parent.GetChild(2);
                    }
                    else if (tempNeededFoodIndex == 2 && has2 && !delivered2)
                    {
                        return guest.transform.parent.GetChild(0);
                    }
                }
            }

        }
        else
        {
            foreach (Guest guest in GetComponentsInChildren<Guest>())
            {
                if (guest.needFood)
                {
                    if (guest.foodIndex == 0 && has0)
                    {
                        guest.StartEating();
                        guest.needFood = false;
                        return guest.transform.parent.GetChild(0);
                    }
                    else if (guest.foodIndex == 1 && has1)
                    {
                        guest.StartEating();
                        guest.needFood = false;
                        return guest.transform.parent.GetChild(0);
                    }
                    else if (guest.foodIndex == 2 && has2)
                    {
                        guest.StartEating();
                        guest.needFood = false;
                        return guest.transform.parent.GetChild(0);
                    }
                    else if (guest.foodIndex == 3 && has3)
                    {
                        guest.StartEating();
                        guest.needFood = false;
                        return guest.transform.parent.GetChild(0);
                    }
                }
            }
        }
        return null;
    }
}

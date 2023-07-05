using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosFirstSensor : MonoBehaviour
{
    List<int> addedIDs = new List<int>();

    public Animator waitressAnimator;
    public void WaitressPlayAnim()
    {
        waitressAnimator.Play("Talk");
    }

    private bool canSpawnMoney = false;

    public void CanSpawne(bool can = true)
    {
        if (canSpawnMoney != can)
        {
            canSpawnMoney = can;
            GetComponent<Collider>().enabled = false;
            Invoke("EnableColl", 0.1f);
        }
    }

    private void EnableColl()
    {
        GetComponent<Collider>().enabled = true;
    }

    public FoodUpShelf shelf;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains("Drink") && other.gameObject.name.Contains("Guest_MC") && (other.transform.parent == transform) && (other./*transform.Find("CupPos").*/GetComponentsInChildren<FoodBoxPrefab>().Length == 0))
        {
            Debug.Log("TRIES_TO_PAY_FOR_DRINK");

            //TRY TO GET CUP
            Transform tempCup = shelf.GetLastBox(false);
            if (tempCup != null)
            {
                if (addedIDs.Contains(other.gameObject.GetInstanceID()))
                    return;
                addedIDs.Add(other.gameObject.GetInstanceID());

                other.GetComponent<Guest>().CupHolderUp();
                tempCup.GetComponentInChildren<FoodBoxPrefab>().MoveToParent(other.GetComponent<Guest>().tempCupHolder);

                transform.parent.parent.GetComponentInChildren<SpawnMoneyPos>().SpawnMoneyOnce(0.5f);
            }

            return;
        }

        if (!canSpawnMoney)
        {

        }
        else
        {
            if (other.gameObject.name.Contains("Guest_MC") /*&& other.transform.parent.gameObject.name.Contains("Pos_Seat")*/)
            {
                other.GetComponent<Guest>().PayFood();
            }
        }

    }
}

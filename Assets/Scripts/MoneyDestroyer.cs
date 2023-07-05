using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDestroyer : MonoBehaviour
{
    private List<Money> moneys = new List<Money>();

    private float timeBetweenMoneys = 0.019f;
    private int tempMoneyCount = 0;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Money"))
        {
            if (other.GetComponent<Money>() != null /*&& !moneys.Contains(other.GetComponent<Money>())*/)
            {
                other.GetComponent<Money>().ReceivedMoney(timeBetweenMoneys * tempMoneyCount);
                tempMoneyCount++;
            }
            //moneys.Add(other.GetComponent<Money>());
        }

    }

    private void OnEnable()
    {
        tempMoneyCount = 0;

        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        CancelInvoke("CheckChild");
        Invoke("CheckChild", 0.1f);
    }

    private void CheckChild()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        Invoke("CheckChild", 0.2f);
    }

    private void DisableGameobject()
    {
        gameObject.SetActive(false);
    }
}

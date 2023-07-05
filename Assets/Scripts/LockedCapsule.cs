using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockedCapsule : MonoBehaviour
{
    public int placeIndex;

    public GameObject poofParticle;

    private TextMeshProUGUI moneyText;
    private Image fillImage;

    public int price = 50;
    private int priceAtStart;

    public GameObject activateAfterThis, activateAfterThis2, deactivateAfterThis;
    public bool enableNextTut = false;

    public Collider enableColliderAtEnd;

    void Start()
    {
        if (PlayerPrefs.GetInt("Place" + placeIndex.ToString(), 0) == 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);

            priceAtStart = price;
            moneyText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
            fillImage = transform.parent.GetChild(0).GetChild(0).GetComponent<Image>();

            price = PlayerPrefs.GetInt("PlacePrice" + placeIndex + ToString(), price);

            moneyText.text = price.ToString();
            fillImage.fillAmount = 1f - (float)price / priceAtStart;
        }
        else
        {
            Destroy(transform.GetComponentInChildren<Animation>());
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.parent = null;

            if (activateAfterThis != null)
            {
                activateAfterThis.SetActive(true);
            }
            if (activateAfterThis2 != null)
                activateAfterThis2.SetActive(true);
            if (deactivateAfterThis != null)
                deactivateAfterThis.SetActive(false);

            if (canSpawnCheaterAfterThis)
                FindObjectOfType<AiSpawner>().canSpawnCheater = true;

            Destroy(transform.parent.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            StartCoroutine(CountDown());
        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            StopAllCoroutines();
        }

    }


    IEnumerator CountDown()
    {
        int shouldBeThisMuchMoney = MoneySpawner.Instance.tempMoney - price;

        yield return new WaitForSeconds(0.6f);

        int helper = 0;
        int something = 30;
        int xyz = 1;

        while (MoneySpawner.Instance.tempMoney > 0)
        {
            for (int i = 0; i < xyz; i++)
            {
                helper++;
                if (helper < 1 * something)
                    xyz = 1;
                else if (helper < 2 * something)
                    xyz = 2;
                else /*if (helper < 3 * sthing)*/
                    xyz = 3;
                /*else *//*if (helper < 4 * sthing)*/
                //xyz = 4;
                //else if (helper < 5 * sthing)
                //    xyz = 5;


                MoneySpawner.Instance.UpdateMoney(-1);

                if (MoneySpawner.Instance.tempMoney <= 0)
                    StopAllCoroutines();

                price--;
                moneyText.text = price.ToString();

                PlayerPrefs.SetInt("PlacePrice" + placeIndex + ToString(), price);

                fillImage.fillAmount = 1f - (float)price / priceAtStart;

                if (price <= 0)
                {
                    CapsuleBuy(shouldBeThisMuchMoney);
                }

                //yield return new WaitForSeconds(0.01f);
                //yield return new WaitForEndOfFrame();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void CapsuleBuy(int shouldBeThisMuchMoney)
    {
        StopAllCoroutines();

        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).gameObject.name.Contains("_MC_") || transform.GetChild(0).gameObject.name.Contains("_OUT"))
            {
                PlayerPrefs.SetInt("TableUnlocked", PlayerPrefs.GetInt("TableUnlocked", 0) + 1);
            }

            if (transform.GetChild(0).GetChild(0).gameObject.name.Contains("VespaHolder"))
            {
                PlayerPrefs.SetInt("DeliveryUnlocked", PlayerPrefs.GetInt("DeliveryUnlocked", 0) + 1);
            }
        }

        Instantiate(poofParticle, transform.position + Vector3.up, Quaternion.identity);

        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.parent = null;
        }

        if (enableNextTut)
            ArrowTutorialHolder.Instance.EnableNextTut();
        if (activateAfterThis != null)
        {
            if (!gameObject.name.Contains("Vespa"))
                activateAfterThis.SetActive(true);
        }
        if (deactivateAfterThis != null)
            deactivateAfterThis.SetActive(false);

        PlayerPrefs.SetInt("Place" + placeIndex.ToString(), 1);

        Instantiate(Resources.Load("InvisibleCylinder") as GameObject, transform.position, Quaternion.identity);

        if (canPushPlayer)
            FindObjectOfType<PlayerMcDonalds>().MoveAway(transform.position);

        if (enableColliderAtEnd != null)
            enableColliderAtEnd.enabled = true;

        if (MoneySpawner.Instance.tempMoney != shouldBeThisMuchMoney)
        {
            MoneySpawner.Instance.tempMoney = shouldBeThisMuchMoney;
            MoneySpawner.Instance.UpdateMoney(0);
        }

        if (!gameObject.name.Contains("Vespa"))
            Destroy(transform.parent.gameObject);
        else
            Invoke("VespaDestroy", 0.3f);
    }

    private void VespaDestroy()
    {
        activateAfterThis.SetActive(true);
        Destroy(transform.parent.gameObject);
    }

    public bool canPushPlayer = true;

    public bool canSpawnCheaterAfterThis = false;

}

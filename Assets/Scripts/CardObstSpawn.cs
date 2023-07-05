using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObstSpawn : MonoBehaviour
{
    void Start()
    {
        AddCard();
    }

    public void Add3CardsToEnemy(Transform targetTransform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<Card>().MoveTo(targetTransform);
        }
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //}
    }

    public void ChangeCardsColors(char color)
    {
        for (int i = 3 - 1; i >= 0; i--)
        {
            int tempDeckIndex = 0;
            //if (color == 'r')
            //    tempDeckIndex = 0;
            if (color == 'y')
                tempDeckIndex = 1;
            if (color == 'b')
                tempDeckIndex = 2;
            if (color == 'g')
                tempDeckIndex = 3;

            Transform tempChild = transform.GetChild(i);
            tempChild.GetComponent<Card>().enabled = false;
            tempChild.GetComponent<Collider>().enabled = false;
            tempChild.parent = null;
            tempChild.GetComponent<Animation>().Play("RotateAround");
            GameObject newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(Random.Range(0, cardsDeck[tempDeckIndex].childCount)).gameObject, tempChild);
            newCard.transform.localRotation = Quaternion.identity;
            newCard.transform.localPosition = new Vector3(0f, 0f, -5f);
            newCard.transform.GetComponent<Animation>().Stop();
            newCard.transform.localScale = Vector3.one;
            //if (i == 0)
            //    newCard.transform.localPosition = new Vector3(1.438573f, 0f, 0f);
            //if (i == 1)
            //    newCard.transform.localPosition = new Vector3(0f, 0f, 0f);
            //if (i == 2)
            //    newCard.transform.localPosition = new Vector3(-1.438573f, 0f, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("ENEMY_HAND"))
        {
            other.GetComponentInChildren<CardHolder>().EnemyDecide();

            Invoke("CardsAnim", 0.2f);
        }
    }

    public CardHolder playerCardHolder;

    private void CardsAnim()
    {
        if (Random.Range(0, 2) == 0)
            AddCardByColor(playerCardHolder.GetTempCard('c'));
        else
            AddCardByNumber(playerCardHolder.GetTempCard('n'));

        Invoke("AddCardBAD", 0.1f);
    }

    public Transform[] cardsDeck;

    public void AddCardByNumber(char numberName)
    {
        int randomChildIndex = Random.Range(0, transform.childCount);
        Destroy(transform.GetChild(randomChildIndex).gameObject);

        int tempDeckIndex = Random.Range(0, cardsDeck.Length);

        int goodNumberIndex = 0;
        for (int i = 0; i < cardsDeck[tempDeckIndex].childCount; i++)
        {
            if (cardsDeck[tempDeckIndex].GetChild(i).GetComponent<Card>().number == numberName)
            {
                goodNumberIndex = i;
                break;
            }
        }

        GameObject newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(goodNumberIndex).gameObject, transform);
        newCard.transform.localRotation = Quaternion.identity;
        newCard.transform.localScale = Vector3.zero;
        if (randomChildIndex == 0)
            newCard.transform.localPosition = new Vector3(1.438573f, 0f, 0f);
        if (randomChildIndex == 1)
            newCard.transform.localPosition = new Vector3(0f, 0f, 0f);
        if (randomChildIndex == 2)
            newCard.transform.localPosition = new Vector3(-1.438573f, 0f, 0f);

        newCard.name = "Card_GOOD";
    }

    public void AddCardByColor(char colorName)
    {
        int randomChildIndex = Random.Range(0, transform.childCount);
        Destroy(transform.GetChild(randomChildIndex).gameObject);

        int tempDeckIndex;
        if (colorName == 'r')
            tempDeckIndex = 0;
        else if (colorName == 'y')
            tempDeckIndex = 1;
        else if (colorName == 'b')
            tempDeckIndex = 2;
        else
            tempDeckIndex = 3;

        GameObject newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(Random.Range(0, cardsDeck[tempDeckIndex].childCount)).gameObject, transform);
        newCard.transform.localRotation = Quaternion.identity;
        newCard.transform.localScale = Vector3.zero;
        if (randomChildIndex == 0)
            newCard.transform.localPosition = new Vector3(1.438573f, 0f, 0f);
        if (randomChildIndex == 1)
            newCard.transform.localPosition = new Vector3(0f, 0f, 0f);
        if (randomChildIndex == 2)
            newCard.transform.localPosition = new Vector3(-1.438573f, 0f, 0f);

        newCard.name = "Card_GOOD";
    }

    private void AddCardBAD()
    {
        GameObject tempChildDestroyable0 = transform.GetChild(0).gameObject, tempChildDestroyable1 = transform.GetChild(1).gameObject, tempChildDestroyable2 = transform.GetChild(2).gameObject;

        if (!tempChildDestroyable0.name.Contains("GOOD"))
        {
            GameObject newCard;
            Vector3 tempChildLocalPos = tempChildDestroyable0.transform.localPosition;
            do
            {
                int tempDeckIndex = Random.Range(0, cardsDeck.Length);
                newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(Random.Range(0, cardsDeck[tempDeckIndex].childCount)).gameObject, transform);
                newCard.transform.localRotation = Quaternion.identity;
                newCard.transform.localScale = Vector3.zero;
                newCard.transform.localPosition = tempChildLocalPos;
                Destroy(tempChildDestroyable0);
                tempChildDestroyable0 = newCard;
            } while (playerCardHolder.GetTempCard('c') == newCard.GetComponent<Card>().color || playerCardHolder.GetTempCard('n') == newCard.GetComponent<Card>().number);
        }
        if (!tempChildDestroyable1.name.Contains("GOOD"))
        {
            GameObject newCard;
            Vector3 tempChildLocalPos = tempChildDestroyable1.transform.localPosition;
            do
            {
                int tempDeckIndex = Random.Range(0, cardsDeck.Length);
                newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(Random.Range(0, cardsDeck[tempDeckIndex].childCount)).gameObject, transform);
                newCard.transform.localRotation = Quaternion.identity;
                newCard.transform.localScale = Vector3.zero;
                newCard.transform.localPosition = tempChildLocalPos;
                Destroy(tempChildDestroyable1);
                tempChildDestroyable1 = newCard;
            } while (playerCardHolder.GetTempCard('c') == newCard.GetComponent<Card>().color || playerCardHolder.GetTempCard('n') == newCard.GetComponent<Card>().number);
        }
        if (!tempChildDestroyable2.name.Contains("GOOD"))
        {
            GameObject newCard;
            Vector3 tempChildLocalPos = tempChildDestroyable2.transform.localPosition;
            do
            {
                int tempDeckIndex = Random.Range(0, cardsDeck.Length);
                newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(Random.Range(0, cardsDeck[tempDeckIndex].childCount)).gameObject, transform);
                newCard.transform.localRotation = Quaternion.identity;
                newCard.transform.localScale = Vector3.zero;
                newCard.transform.localPosition = tempChildLocalPos;
                Destroy(tempChildDestroyable2);
                tempChildDestroyable2 = newCard;
            } while (playerCardHolder.GetTempCard('c') == newCard.GetComponent<Card>().color || playerCardHolder.GetTempCard('n') == newCard.GetComponent<Card>().number);
        }

        foreach (Animation anim in transform.GetComponentsInChildren<Animation>())
        {
            anim.Play();
        }
    }

    public void AddCard(char colorName = 'n', char numberName = 'n', char actionName = 'n')
    {
        if ((colorName == 'n') && (numberName == 'n') && (actionName == 'n'))
        {
            //RANDOM CARD
            for (int i = 0; i < 3; i++)
            {
                int tempDeckIndex = Random.Range(0, cardsDeck.Length);
                GameObject newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(Random.Range(0, cardsDeck[tempDeckIndex].childCount)).gameObject, transform);
                newCard.transform.localRotation = Quaternion.identity;
                newCard.transform.localScale = Vector3.zero;
                if (i == 0)
                    newCard.transform.localPosition = new Vector3(1.438573f, 0f, 0f);
                if (i == 1)
                    newCard.transform.localPosition = new Vector3(0f, 0f, 0f);
                if (i == 2)
                    newCard.transform.localPosition = new Vector3(-1.438573f, 0f, 0f);
            }
        }
    }
}

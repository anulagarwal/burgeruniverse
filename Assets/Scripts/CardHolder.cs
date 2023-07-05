using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHolder : MonoBehaviour
{
    public static CardHolder CardHolderPlayer, cardHolderEnemy;

    private Vector3 localPos, localScale;
    private Quaternion localRot;

    public void RemoveCardsButOne()
    {
        for (int i = transform.childCount - 1; i >= 1; i--)
        {
            if (transform.GetChild(i).childCount > 0)
                RemoveCard();
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void Awake()
    {
        if (gameObject.name == "CardHolders")
            CardHolderPlayer = this;
        else
            cardHolderEnemy = this;
    }

    public TextMeshProUGUI cardCounterText;
    public TextMeshPro cardCounterTextSimple;

    public int startWith = 7;

    public Transform[] cardsDeck;

    private float offsetY = 0.01698818f, offsetZ = 227.1065f;

    //ENEMY-------------------------------------------------
    private List<GameObject> enemyCards = new List<GameObject>();

    public void EnemyDecide()
    {
        if (Random.Range(0, 9) < 3)
            EnemyAddCard();
        else
            EnemyRemoveCard();
    }

    public void EnemyAddCard(int count = 1)
    {
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < enemyCards.Count; i++)
            {
                if (enemyCards[i].activeInHierarchy)
                {
                    enemyCards[i - 1].SetActive(true);
                    break;
                }
            }
        }

        CountEnemyCards();
    }

    private void EnemyRemoveCard(int count = 1)
    {
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < enemyCards.Count; i++)
            {
                if (enemyCards[i].activeInHierarchy)
                {
                    enemyCards[i].SetActive(false);
                    break;
                }
            }
        }

        CountEnemyCards();
    }

    private void CountEnemyCards()
    {
        int countOfActiveCards = 0;
        for (int i = 0; i < enemyCards.Count; i++)
        {
            if (enemyCards[i].activeInHierarchy)
                countOfActiveCards++;
        }
        cardCounterTextSimple.text = cardCounterText.text = countOfActiveCards.ToString();
    }

    //-------------------------------------------------

    public char GetTempCard(char c)
    {
        //if (c == 'n')
        //    return transform.GetChild(transform.childCount - 1).GetComponent<Card>().number;
        //else
        //    return transform.GetChild(transform.childCount - 1).GetComponent<Card>().color;

        if (c == 'n')
            return transform.GetChild(tempCardCount - 1).GetComponentInChildren<Card>().number;
        else
            return transform.GetChild(tempCardCount - 1).GetComponentInChildren<Card>().color;
    }

    public void AddCard(char colorName = 'n', char numberName = 'n', char actionName = 'n')
    {
        //tempCardCount++;
        //cardCounterText.text = tempCardCount.ToString();
        transform.Rotate(Vector3.forward * -8.5f);

        if ((colorName == 'n') && (numberName == 'n') && (actionName == 'n'))
        {
            //RANDOM CARD
            int tempDeckIndex = Random.Range(0, cardsDeck.Length);
            GameObject newCard = Instantiate(cardsDeck[tempDeckIndex].GetChild(Random.Range(0, cardsDeck[tempDeckIndex].childCount)).gameObject, transform.parent);
            //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
            newCard.transform.parent = transform.GetChild(tempCardCount);
            newCard.transform.localPosition = localPos;
            newCard.transform.localScale = localScale;
            newCard.transform.localRotation = localRot;
            return;
        }
        else if (actionName != 'n')
        {
            int tempDeckIndex;
            GameObject newCard;
            if (colorName != 'n')
            {
                //ACTION CARD
                tempDeckIndex = 4;
                foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
                {
                    if (card.IsSameCard(colorName, numberName, actionName))
                    {
                        newCard = Instantiate(card.gameObject, transform.parent);
                        //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                        newCard.transform.parent = transform.GetChild(tempCardCount);
                        newCard.transform.localPosition = localPos;
                        newCard.transform.localScale = localScale;
                        newCard.transform.localRotation = localRot;
                        return;
                    }
                }
                tempDeckIndex = 5;
                foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
                {
                    if (card.IsSameCard(colorName, numberName, actionName))
                    {
                        newCard = Instantiate(card.gameObject, transform.parent);
                        //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                        newCard.transform.parent = transform.GetChild(tempCardCount);
                        newCard.transform.localPosition = localPos;
                        newCard.transform.localScale = localScale;
                        newCard.transform.localRotation = localRot;
                        return;
                    }
                }
                tempDeckIndex = 6;
                foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
                {
                    if (card.IsSameCard(colorName, numberName, actionName))
                    {
                        newCard = Instantiate(card.gameObject, transform.parent);
                        //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                        newCard.transform.parent = transform.GetChild(tempCardCount);
                        newCard.transform.localPosition = localPos;
                        newCard.transform.localScale = localScale;
                        newCard.transform.localRotation = localRot;
                        return;
                    }
                }

            }

            if (colorName == 'n')
            {
                //SUPERACTION CARD
                tempDeckIndex = 4;
                foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
                {
                    if (card.IsSameCard(colorName, numberName, actionName))
                    {
                        newCard = Instantiate(card.gameObject, transform.parent);
                        //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                        newCard.transform.parent = transform.GetChild(tempCardCount);
                        newCard.transform.localPosition = localPos;
                        newCard.transform.localScale = localScale;
                        newCard.transform.localRotation = localRot;
                        return;
                    }
                }
            }
        }
        if (char.IsDigit(numberName))
        {
            //NUMBER CARD
            int tempDeckIndex;
            GameObject newCard;

            tempDeckIndex = 0;
            foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
            {
                if (card.IsSameCard(colorName, numberName, actionName))
                {
                    newCard = Instantiate(card.gameObject, transform.parent);
                    //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                    newCard.transform.parent = transform.GetChild(tempCardCount);
                    newCard.transform.localPosition = localPos;
                    newCard.transform.localScale = localScale;
                    newCard.transform.localRotation = localRot;
                    return;
                }
            }
            tempDeckIndex = 1;
            foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
            {
                if (card.IsSameCard(colorName, numberName, actionName))
                {
                    newCard = Instantiate(card.gameObject, transform.parent);
                    //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                    newCard.transform.parent = transform.GetChild(tempCardCount);
                    newCard.transform.localPosition = localPos;
                    newCard.transform.localScale = localScale;
                    newCard.transform.localRotation = localRot;
                    return;
                }
            }
            tempDeckIndex = 2;
            foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
            {
                if (card.IsSameCard(colorName, numberName, actionName))
                {
                    newCard = Instantiate(card.gameObject, transform.parent);
                    //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                    newCard.transform.parent = transform.GetChild(tempCardCount);
                    newCard.transform.localPosition = localPos;
                    newCard.transform.localScale = localScale;
                    newCard.transform.localRotation = localRot;
                    return;
                }
            }
            tempDeckIndex = 3;
            foreach (Card card in cardsDeck[tempDeckIndex].GetComponentsInChildren<Card>())
            {
                if (card.IsSameCard(colorName, numberName, actionName))
                {
                    newCard = Instantiate(card.gameObject, transform.parent);
                    //newCard.transform.localPosition = new Vector3(0f, offsetY * transform.childCount, offsetZ * transform.childCount);
                    newCard.transform.parent = transform.GetChild(tempCardCount);
                    newCard.transform.localPosition = localPos;
                    newCard.transform.localScale = localScale;
                    newCard.transform.localRotation = localRot;
                    return;
                }
            }
        }

        cardCounterText.text = tempCardCount.ToString();
    }

    [HideInInspector] public int tempCardCount = 0;
    public int tempCardCountDISPLAY;

    public void RemoveCard()
    {
        //tempCardCount--;
        //cardCounterText.text = tempCardCount.ToString();

        transform.Rotate(Vector3.forward * 8.5f);
    }

    private bool isEnemy = false;

    void Start()
    {
        //Destroy(transform.GetChild(3).gameObject);
        //Destroy(transform.GetChild(2).gameObject);
        //Destroy(transform.GetChild(1).gameObject);
        //Destroy(transform.GetChild(0).gameObject);

        if (transform.GetComponentsInChildren<Card>() != null)
            tempCardCount = transform.GetComponentsInChildren<Card>().Length;
        else
            tempCardCount = 0;

        localPos = transform.GetChild(0).GetChild(0).localPosition;
        localScale = transform.GetChild(0).GetChild(0).localScale;
        localRot = transform.GetChild(0).GetChild(0).localRotation;


        if (gameObject.name.Contains("EnemyHold"))
        {
            isEnemy = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name.Contains("Card"))
                    enemyCards.Add(transform.GetChild(i).gameObject);
            }

            CountEnemyCards();

            return;
        }

        Destroy(transform.GetChild(0).GetChild(0).gameObject);

        StartCoroutine(AddCardsToPlayer());

        //AddCard('b', '2');

    }

    public Animation cardChooseUIAnim;
    public void CardSelectionOver()
    {
        cardChooseUIAnim.Play("PopBackAnim");
        GetComponentInParent<HandRun>().forwardSpeed = GetComponentInParent<HandRun>().forwardDefSpeed;

        for (int i = 0; i < tempCardCount; i++)
        {
            if (transform.GetChild(i).childCount == 0)
            {
                transform.GetChild(i + 1).GetChild(0).parent = transform.GetChild(i);
                GameObject newCard = transform.GetChild(i).GetChild(0).gameObject;
                newCard.transform.localPosition = localPos;
                newCard.transform.localScale = localScale;
                newCard.transform.localRotation = localRot;
            }
        }
    }

    public void PlayCardSelectionAnim()
    {
        GetComponent<Animation>().Play();
    }

    public void MoveOneCardToEnemy(int index)
    {
        transform.GetChild(index).GetComponentInChildren<Card>().MoveTo(cardHolderEnemy.transform);
    }

    IEnumerator AddCardsToPlayer()
    {
        for (int i = 0; i < startWith; i++)
        {
            yield return new WaitForEndOfFrame();
            AddCard();
        }
    }


    void Update()
    {
        if (isEnemy)
            return;

        if (transform.GetComponentsInChildren<Card>() != null)
            tempCardCount = transform.GetComponentsInChildren<Card>().Length;
        else
            tempCardCount = 0;

        if (tempCardCount == 0 && Time.timeSinceLevelLoad > 1f)
        {
            //GetComponentInParent<HandRun>().forwardSpeed = 0f;
        }

        tempCardCountDISPLAY = tempCardCount;
        cardCounterTextSimple.text = cardCounterText.text = tempCardCount.ToString();
    }
}

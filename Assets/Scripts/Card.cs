using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class Card : MonoBehaviour
{
    public char color, number, action;

    [HideInInspector] public bool isNumber = false;

    public void MoveTo(Transform target)
    {
        StartCoroutine(MoveToTarget(target));
    }


    IEnumerator MoveToTarget(Transform targetTransform)
    {
        GetComponent<Collider>().enabled = false;

        Vector3 targetPos = targetTransform.position;
        Quaternion targetRot = targetTransform.Find("Rot").rotation;

        while ((Vector3.Distance(transform.position, targetPos) > 0.01f) /*&& (transform.rotation != targetRot)*/)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 3f * Time.deltaTime);

            transform.parent = null;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 5.5f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        CardHolder.cardHolderEnemy.EnemyAddCard();

        targetTransform.Find("Rot").GetComponentInChildren<ParticleSystem>().Play();

        Destroy(gameObject);
    }



    void Start()
    {
        isNumber = char.IsDigit(number);
        if (transform.parent != null)
        {
            if (transform.parent.gameObject.name.Contains("CardHolder"))
                isHolding = true;
        }
    }

    public bool freshColor, freshNumber, freshAction, spawnTakiBack;
    [HideInInspector] public bool isHolding = false;

    public bool IsSameCard(char c, char n, char a)
    {
        return (color == c) && (number == n) & (action == a);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("HAND") && !isHolding)
        {
            Transform cardHolderTransform = other.GetComponentInChildren<CardHolder>().transform;
            cardHolderTransform.GetChild(cardHolderTransform.GetComponent<CardHolder>().tempCardCount - 1).GetComponentInChildren<Card>().Decide(color, number, action);
            if (!transform.parent.name.Contains("Obst"))
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }

    public void Decide(char c, char n, char a)
    {
        //IF HAND CARD IS NUMBER
        if (isNumber)
        {
            //SAME COLOR OR NUMBER -> GOOD
            if (c == color || n == number)
                GoodCard();
            else
                BadCard(c, n, a);
        }
        //HAND CARD IS SUPER_ACTION CARD
        else if (char.IsDigit(action))
        {
            GoodCard();
        }
    }
    private void GoodCard()
    {
        GameObject.FindGameObjectWithTag("-1").GetComponent<ParticleSystem>().Play();
        //Plus1Text.Instance.PlayText(-1);

        GetComponentInParent<CardHolder>().RemoveCard();
        //if (!transform.parent.name.Contains("Obst"))
        //    Destroy(transform.parent.gameObject);
        //else
        Destroy(gameObject);
    }

    private void BadCard(char col = 'n', char num = 'n', char ac = 'n')
    {
        GameObject.FindGameObjectWithTag("+1").GetComponent<ParticleSystem>().Play();
        //Plus1Text.Instance.PlayText(+1);

        GetComponentInParent<CardHolder>().AddCard(col, num, ac);
        FindObjectOfType<CamImpulseSource>().Shake();
    }


    void Update()
    {
        if (freshColor)
        {
            freshColor = false;
            if (transform.parent.name.Contains("red"))
                color = 'r';
            else if (transform.parent.name.Contains("green"))
                color = 'g';
            else if (transform.parent.name.Contains("blue"))
                color = 'b';
            else
                color = 'y';
        }
        if (freshNumber)
        {
            freshNumber = false;
            number = GetComponent<MeshRenderer>().material.name.ToCharArray()[0];
        }

        if (spawnTakiBack)
        {
            spawnTakiBack = false;
            Instantiate(new GameObject("TakiBack"), transform);
            transform.GetChild(0).localPosition = new Vector3(0f, 0f, -10f);
        }
    }
}

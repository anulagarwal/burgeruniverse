using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHolderHolder : MonoBehaviour
{
    public Transform helpPos;
    public GameObject enemy_hand2, cardHolderReverse, cardObstReverse, cardHolderTextSimple, handEnableDisable, handEnemy1st;

    private void StopRoad()
    {
        FindObjectOfType<HandRun>().forwardSpeed = 0f;
    }

    private void SwitchHands()
    {
        CardHolder.CardHolderPlayer.transform.parent.localPosition = new Vector3(0f, 0f, 6.947f);

        enemyHandAtStart.SetActive(true);
        handEnemy1st.SetActive(false);
        handEnableDisable.SetActive(true);
    }

    public GameObject enemyHandAtStart;
    private void DisableEnemyHandAtStart()
    {
        enemyHandAtStart.SetActive(false);
    }

    private void ReverseRoad()
    {
        //transform.parent = null;
        handEnableDisable.SetActive(false);

        foreach (CardObstSpawn item in FindObjectsOfType<CardObstSpawn>())
        {
            if (!item.gameObject.name.Contains("Reverse"))
            {
                Destroy(item.gameObject);
            }
        }

        cardHolderTextSimple.SetActive(false);
        enemy_hand2.SetActive(true);
        CardHolder.CardHolderPlayer.RemoveCardsButOne();
        cardHolderReverse.SetActive(true);
        cardObstReverse.transform.parent = null;
        cardObstReverse.SetActive(true);

        Transform hand = FindObjectOfType<HandRun>().transform;


        FindObjectOfType<HandRun>().forwardSpeed = 1.16f;

        CardHolder.CardHolderPlayer.transform.parent.localPosition = Vector3.zero; ;



        //hand.Rotate(hand.up, 180f);
        hand.position = new Vector3(helpPos.position.x, hand.position.y, helpPos.position.z);
        hand.rotation = helpPos.rotation;
        //hand.position = new Vector3(hand.position.x, hand.position.y, hand.position.z - 3f);


        FindObjectOfType<CamImpulseSource>().enabled = false;
        //FindObjectOfType<CamImpulseSource>().gameObject.SetActive(false);
        //transform.parent = hand;
    }


    void Start()
    {

    }

    void Update()
    {

    }
}

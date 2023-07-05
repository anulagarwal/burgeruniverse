using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityGirlController : MonoBehaviour
{
    private Vector3 posOfCard;
    private Quaternion rotOfCard;
    private Transform tempCard;

    public Transform hand;

    private void PullCard()
    {
        tempCard = transform.Find("Cards").Find("Right");
        posOfCard = tempCard.position;
        rotOfCard = tempCard.rotation;
        tempCard.parent = hand;
        tempCard.localPosition = Vector3.zero;
        tempCard.localRotation = Quaternion.identity;
    }

    private void DropCard()
    {
        tempCard.parent = transform.Find("Cards");
        tempCard.position = posOfCard;
        tempCard.rotation = rotOfCard;
    }

    int drawingIndex = 0;
    public GameObject[] drawings;

    private void EnableDrawing()
    {
        drawings[drawingIndex].SetActive(true);
        drawingIndex++;
    }
}

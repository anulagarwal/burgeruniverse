using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CoinsHolder : MonoBehaviour
{
    public static CoinsHolder HappyEmojiInstance, OkayEmojiInstance, SadEmojiInstance;

    private List<Vector3> localPositionsOfChildren = new List<Vector3>();

    private void Start()
    {
        foreach (Transform child in transform)
            localPositionsOfChildren.Add(child.localPosition);
    }

    private void Awake()
    {
        if (gameObject.name.Contains("Happy"))
            HappyEmojiInstance = this;
        if (gameObject.name.Contains("Okay"))
            OkayEmojiInstance = this;
        if (gameObject.name.Contains("Sad"))
            SadEmojiInstance = this;
    }

    public void PlayThisAt(Transform startHere)
    {
        int index = 0;
        foreach (Transform child in transform)
        {
            child.localPosition = localPositionsOfChildren[index];
            child.localScale = new Vector3(0.3f, 0.3f, 1f);
            index++;
        }

        transform.position = startHere.position;
        GetComponent<Animation>().Play();
    }

    IEnumerator MoveChildren()
    {
        int i = 0;
        while (i < transform.childCount)
        {
            yield return new WaitForSeconds(0.05f);
            transform.GetChild(i).GetComponent<MovingCoin>().StartMoving();
            i++;
        }
    }
}

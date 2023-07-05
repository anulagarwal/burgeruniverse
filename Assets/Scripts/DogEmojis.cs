using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEmojis : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void ResetEmojis()
    {
        transform.Find("SadEmojis").gameObject.SetActive(false);
        transform.Find("HappyEmojis").gameObject.SetActive(false);
    }

    public void Sad()
    {
        transform.Find("SadEmojis").gameObject.SetActive(true);
        Invoke("ResetEmojis", 2.5f);
    }

    public void Happy()
    {
        transform.Find("HappyEmojis").gameObject.SetActive(true);
        Invoke("ResetEmojis", 2.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkin : MonoBehaviour
{
    public GameObject[] beards, caps, eyes, eyebases, glasses, gloves, hairs, mouths, pants, shirts, shoes, watches;

    void Start()
    {
        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        //eyes[Random.Range(0, eyes.Length)].gameObject.SetActive(true);
        //mouths[Random.Range(0, mouths.Length)].gameObject.SetActive(true);
        //eyebases[Random.Range(0, eyebases.Length)].gameObject.SetActive(true);

        if (Random.Range(0, 2) == 0)
            watches[Random.Range(0, watches.Length)].gameObject.SetActive(true);
        if (Random.Range(0, 2) == 0)
            beards[Random.Range(0, beards.Length)].gameObject.SetActive(true);
        if (Random.Range(0, 4) == 0)
            caps[Random.Range(0, caps.Length)].gameObject.SetActive(true);
        else
            hairs[Random.Range(0, hairs.Length)].gameObject.SetActive(true);

        //glasses[Random.Range(0, glasses.Length)].gameObject.SetActive(true);
        //gloves[Random.Range(0, gloves.Length)].gameObject.SetActive(true);
        pants[Random.Range(0, pants.Length)].gameObject.SetActive(true);
        shirts[Random.Range(0, shirts.Length)].gameObject.SetActive(true);
        shoes[Random.Range(0, shoes.Length)].gameObject.SetActive(true);
    }

    void Update()
    {

    }
}

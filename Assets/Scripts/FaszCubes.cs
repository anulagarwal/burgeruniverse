using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaszCubes : MonoBehaviour
{
    public int count = 10;

    private TextMeshPro textmeshpro;

    private void Start()
    {
        textmeshpro = transform.GetChild(0).GetComponentInChildren<TextMeshPro>();
    }


    IEnumerator CoundDown()
    {
        while (count > 0)
        {
            count--;
            textmeshpro.text = count.ToString();


            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player"))
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().isTrigger = false;
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<Rigidbody>());
            GetComponentInChildren<Animation>().Play();
            other.GetComponent<AgentRun>().StopPlayer();

            StartCoroutine(CoundDown());
        }

    }
}

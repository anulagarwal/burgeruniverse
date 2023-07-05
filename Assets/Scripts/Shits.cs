using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shits : MonoBehaviour
{
    public float speed = 100f, increaseBy = 0.2f;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Character"))
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().isTrigger = false;

            GameObject obj2 = Instantiate(Resources.Load("Poof") as GameObject, transform.position + Vector3.up * 1.3f, Quaternion.identity);
            if (transform.parent.gameObject.name.Contains("Bad"))
                Instantiate(Resources.Load("Bad") as GameObject, transform.position, Quaternion.identity);
            else
                Instantiate(Resources.Load("Good") as GameObject, transform.position, Quaternion.identity);

            other.GetComponentInChildren<CharacterBar>().IncreaseBar(increaseBy);

            Destroy(gameObject);
        }

    }


    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }
}

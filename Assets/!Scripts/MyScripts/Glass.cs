using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public string colliderNameToFragment;

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name.Contains(colliderNameToFragment))
        {
            if (collision.transform.Find("SM_Item_Hammer_01").gameObject.activeInHierarchy)
            {
                GetComponent<Collider>().enabled = false;
                transform.GetComponentInChildren<ParticleSystem>().transform.position = collision.GetContact(0).point;
                transform.GetComponentInChildren<ParticleSystem>().Play();
                transform.GetComponentInChildren<ParticleSystem>().transform.parent = null;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                Destroy(this);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

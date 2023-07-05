using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private int tempGrabbedChildIndex = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.parent.name.Contains("House") && !collision.transform.parent.name.Contains("Box"))
            return;

        if (transform.Find(collision.gameObject.name) != null)
        {
            GetComponent<Animation>().Play();
            transform.Find(collision.gameObject.name).gameObject.SetActive(true);
            collision.gameObject.SetActive(false);

            tempGrabbedChildIndex = transform.Find(collision.gameObject.name).GetSiblingIndex();
        }
    }

    public void Release()
    {
        if (tempGrabbedChildIndex != 0)
        {

            if (transform.GetChild(tempGrabbedChildIndex).gameObject.name.Contains("Pistol"))
            {
                transform.GetComponentInChildren<ParticleSystem>().Play();
                GetComponentInChildren<LaserRobber>().Shoot();
                return;
            }


            if (transform.gameObject.name.Contains("LEFT"))
                transform.GetComponentInChildren<Animation>().Play("HandGrabOut");
            else
                transform.GetComponentInChildren<Animation>().Play("HandGrabOut2");

            transform.GetChild(tempGrabbedChildIndex).gameObject.AddComponent<Rigidbody>();
            transform.GetChild(tempGrabbedChildIndex).gameObject.GetComponent<MeshCollider>().enabled = true;
            transform.GetChild(tempGrabbedChildIndex).parent = null;

            tempGrabbedChildIndex = 0;
        }
    }
}

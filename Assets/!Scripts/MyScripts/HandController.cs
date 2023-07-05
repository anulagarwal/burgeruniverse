using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Transform handTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, handTarget.position, 10f * Time.deltaTime);

    }

    public float force;

    // Update is called once per frame
    void FixedUpdate()
    {
        //GetComponent<Rigidbody>().MovePosition(handTarget.position);
        if (!Input.GetMouseButton(0))
            return;

        if (Vector3.Distance(handTarget.position, transform.position) > 0.1f)
        {
            Vector3 f = handTarget.position - transform.position;
            f = f.normalized;
            f = f * force;
            GetComponent<Rigidbody>().AddForce(f);
        }


        //transform.position = handTarget.position;
    }
}

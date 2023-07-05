using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Obi;

public class GoToPosition : MonoBehaviour
{
    public Transform target;
    //public ObiFixedUpdater updater;

    IEnumerator GoToPlace()
    {
        Transform tempParent = transform.parent;
        tempParent.parent = target.transform.GetChild(0);
        tempParent.GetComponent<Rigidbody>().isKinematic = true;
        tempParent.localPosition = Vector3.zero;

        transform.parent = null;

        yield return new WaitForSeconds(0.1f);
        //updater.enabled = false;

        while (Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 2f * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, 5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(GoToPlace());
        }
    }
}

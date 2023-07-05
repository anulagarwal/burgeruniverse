using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPrefab : MonoBehaviour
{
    int type = 0;

    public Material[] mats;

    //public PlayerChipsHolder chipHolder;

    void Start()
    {
        type = Random.Range(0, 3);
        GetComponent<MeshRenderer>().material = mats[type];
        //MoveToTarget(chipHolder.GetNextTransformOf(type));
    }

    void Update()
    {

    }

    protected float animation;

    public void MoveToTarget(Transform targ)
    {
        //transform.parent = null;
        StartCoroutine(MoveParabola(targ));
    }

    IEnumerator MoveParabola(Transform target)
    {
        //float rotSpeed = 540f;
        ////if (Random.Range(0, 2) == 0)
        ////    rotSpeed = -540f;

        //rotSpeed = Random.Range(-510, 510);

        //GetComponent<Rigidbody>().AddTorque(Vector3.up * rotSpeed);

        Vector3 parabolaStart;

        parabolaStart = transform.position;

        while (Vector3.Distance(transform.position, target.position) > 0.3f)
        {
            animation += Time.deltaTime;
            animation = animation % 5f;
            transform.position = MathParabola.Parabola(parabolaStart, target.position, 1.3f, animation % 5f);

            yield return new WaitForEndOfFrame();
        }

        //Destroy(GetComponent<Rigidbody>());
        transform.parent = target;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //transform.Rotate(Vector3.forward, Random.Range(-15f, 15f));

        //GetComponent<Animation>().Play();

        StopAllCoroutines();
    }
}

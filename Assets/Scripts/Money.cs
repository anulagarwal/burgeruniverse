using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    protected float animation;

    public void MoveToTarget(Vector3 targ)
    {
        StartCoroutine(MoveParabola(targ));
    }

    private bool stillMoving = false;
    private Vector3 targ;

    private void CheckMoving()
    {
        if (stillMoving)
        {
            StopAllCoroutines();

            Destroy(GetComponent<Rigidbody>());
            transform.position = targ;
            transform.localRotation = Quaternion.identity;
            transform.Rotate(Vector3.forward, Random.Range(-15f, 15f));

            GetComponent<Animation>().Play();
        }
    }

    IEnumerator MoveParabola(Vector3 target)
    {
        targ = target;
        stillMoving = true;
        Invoke("CheckMoving", 1.5f);

        float rotSpeed = 540f;
        //if (Random.Range(0, 2) == 0)
        //    rotSpeed = -540f;

        rotSpeed = Random.Range(-510, 510);

        GetComponent<Rigidbody>().AddTorque(Vector3.up * rotSpeed);

        Vector3 parabolaStart;

        parabolaStart = transform.position;

        while (Vector3.Distance(transform.position, target) > 0.3f)
        {
            animation += Time.deltaTime;
            animation = animation % 5f;
            transform.position = MathParabola.Parabola(parabolaStart, target, 2f, animation % 5f);

            yield return new WaitForEndOfFrame();
        }

        stillMoving = false;

        Destroy(GetComponent<Rigidbody>());
        transform.position = target;
        transform.localRotation = Quaternion.identity;
        transform.Rotate(Vector3.forward, Random.Range(-15f, 15f));

        GetComponent<Animation>().Play();
    }

    public void ReceivedMoney(float timeAfterDoingThis)
    {
        Invoke("ReceiveDo", timeAfterDoingThis);
    }

    private void ReceiveDo()
    {
        if (transform.parent.name.Contains("5"))
            MoneySpawner.Instance.SpawnMoneyAt(transform.position, 5);
        else if (transform.parent.name.Contains("10"))
            MoneySpawner.Instance.SpawnMoneyAt(transform.position, 10);

        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {

        //if (other.gameObject.name.Contains("Player"))
        //{
        //    ReceivedMoney();
        //}

    }

}

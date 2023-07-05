using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrates : MonoBehaviour
{
    public Animation rightLeg, leftLeg;

    private Transform[] crates;
    private int tempCratesIndex = 0;
    private int failAt = 0;

    void Start()
    {
        EnableRagdoll(false);

        failAt = Random.Range(3, 7);

        crates = new Transform[transform.parent.Find("Crates").childCount];
        for (int i = 0; i < crates.Length; i++)
            crates[i] = transform.parent.Find("Crates").GetChild(i);


        Invoke("LegDown", Random.Range(1f, 4f));
    }

    public void LegDown()
    {
        rightLeg.Play("RightLegDown");
        //rightLeg.Play("RightLegMove");
        //rightLeg.Play("RightLegStraight");
    }

    public void Fail()
    {
        transform.parent = null;
        EnableRagdoll(true);

        for (int i = 0; i < crates.Length; i++)
        {
            for (int j = 0; j < crates[i].childCount; j++)
            {
                crates[i].GetChild(j).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public void SetPlayerToCrate(bool isUp = true)
    {
        rightLeg.Play("RightLegStraight");
        leftLeg.Play();


        float tempHeight;

        if (tempCratesIndex >= crates.Length)
        {
            transform.parent = GameObject.FindGameObjectWithTag("LastCrate").transform;
            tempHeight = -0.25f;
        }
        else
        {
            transform.parent = crates[tempCratesIndex];

            if (crates[tempCratesIndex].childCount == 2)
                tempHeight = 0.2341f;
            else if (crates[tempCratesIndex].childCount == 3)
                tempHeight = 0.682f;
            else if (crates[tempCratesIndex].childCount == 4)
                tempHeight = 1.1284f;
            else if (crates[tempCratesIndex].childCount == 5)
                tempHeight = 1.577f;
            else
                tempHeight = -0.25f;
        }

        Vector3 targetPos = new Vector3(0f, tempHeight, -0.09f);

        StopAllCoroutines();
        StartCoroutine(MovePlayerUpToNextCrate(targetPos, isUp));
    }

    IEnumerator MovePlayerUpToNextCrate(Vector3 targetPos, bool isUpgo = true)
    {
        if (isUpgo)
        {
            rightLeg.GetComponent<Animation>().Play("RightLegStraight");
            leftLeg.GetComponent<Animation>().Play();
        }
        else
        {
            Debug.Log("STARTED STANKY LEG");
            leftLeg.GetComponent<Animation>().Play("LeftLegAnim2");
        }

        while (Vector3.Distance(transform.localPosition, targetPos) > 0.001f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, 1f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        if (tempCratesIndex >= 1)
        {
            crates[tempCratesIndex - 1].GetComponent<Animation>().Stop();
            crates[tempCratesIndex - 1].rotation = Quaternion.identity;
            crates[tempCratesIndex].GetComponent<Animation>().Play();
        }

        rightLeg.Play();
        tempCratesIndex++;
        Invoke("LegDown", Random.Range(1f, 6f));


        if (tempCratesIndex == failAt)
        {
            crates[tempCratesIndex].GetComponent<Animation>().Stop();
            CancelInvoke("LegDown");
            Invoke("Fail", Random.Range(1f, 6f));
        }
    }



    public void EnableRagdoll(bool canEnable = true)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = canEnable;
        }
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = !canEnable;
        }

        if (canEnable)
        {
            foreach (Animation anim in transform.GetComponentsInChildren<Animation>())
            {
                anim.Stop();
            }
        }
    }
}

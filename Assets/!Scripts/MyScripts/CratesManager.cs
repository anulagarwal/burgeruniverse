using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratesManager : MonoBehaviour
{
    public GameObject endCam;
    public Transform player, questionsHolder;
    public Color goodColor, badColor;
    public Transform[] crates;
    public int tempCratesIndex = 0, index = 0;

    public void Next()
    {
        index++;

        if (crates[tempCratesIndex].childCount > index)
        {
            crates[tempCratesIndex].GetChild(index).GetComponent<Crate>().Activate(true);
        }
    }

    public GameObject cleared, failed;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Next();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            endCam.SetActive(true);
            GameObject.FindGameObjectWithTag("Keyboard").SetActive(false);
            questionsHolder.gameObject.SetActive(false);

            SetPlayerToCrate(false);
            FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegMove2");

            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            cleared.SetActive(true);

            FindObjectOfType<PlayerCrates>().StartDance();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            questionsHolder.gameObject.SetActive(false);
            failed.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (tempCratesIndex >= crates.Length)
            {
                FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegDown2");
                return;
            }

            for (int i = 0; i < crates[tempCratesIndex].childCount; i++)
                crates[tempCratesIndex].GetChild(i).GetComponent<Crate>().SetColor(goodColor);

            questionsHolder.GetChild(tempCratesIndex).GetChild(0).gameObject.SetActive(true);

            if (tempCratesIndex == 0)
                FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegDown");
            else if (crates[tempCratesIndex].childCount > (crates[tempCratesIndex - 1].childCount - 1))
                FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegDown");
            else
            {
                SetPlayerToCrate(false);
                FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegMove2");
            }

            Invoke("GoodAnswer", 1f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < crates[tempCratesIndex].childCount; i++)
                crates[tempCratesIndex].GetChild(i).GetComponent<Crate>().SetColor(badColor);

            Invoke("BadAnswer", 1f);
        }
    }

    private void GoodAnswer()
    {
        for (int i = 0; i < crates[tempCratesIndex].childCount; i++)
        {
            if (!crates[tempCratesIndex].GetChild(i).gameObject.name.Contains("Player"))
                crates[tempCratesIndex].GetChild(i).GetComponentInChildren<Animation>().Play();
        }

        questionsHolder.GetChild(tempCratesIndex).gameObject.SetActive(false);

        index = 0;
        tempCratesIndex++;
        crates[tempCratesIndex].GetChild(index).GetComponent<Crate>().Activate(true);

        questionsHolder.GetChild(tempCratesIndex).gameObject.SetActive(true);


        if (tempCratesIndex > 1)
        {
            crates[tempCratesIndex - 2].GetComponent<Animation>().Stop();
            crates[tempCratesIndex - 2].transform.localRotation = Quaternion.identity;
            crates[tempCratesIndex - 1].GetComponent<Animation>().Play();
        }

        Debug.Log(crates[tempCratesIndex].childCount.ToString() + " -VS- " + (crates[(tempCratesIndex - 1)].childCount - 1).ToString());

        if (tempCratesIndex == 1)
            FindObjectOfType<RightLeg>().GetComponent<Animation>().Play();
        else if (crates[tempCratesIndex].childCount > (crates[tempCratesIndex - 1].childCount - 1))
            FindObjectOfType<RightLeg>().GetComponent<Animation>().Play();
        else
        {
            //SetPlayerToCrate(false);

            FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegDown2");
        }
    }

    private void BadAnswer()
    {

        index = 0;
        for (int i = 0; i < crates[tempCratesIndex].childCount; i++)
            crates[tempCratesIndex].GetChild(i).GetComponent<Crate>().Activate(false);

        questionsHolder.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("Keyboard").SetActive(false);

        player.parent = null;
        player.GetComponent<PlayerCrates>().EnableRagdoll(true);

        for (int i = 0; i < FindObjectsOfType<Crate>().Length; i++)
        {
            FindObjectsOfType<Crate>()[i].transform.parent.GetComponent<Animation>().Stop();
            FindObjectsOfType<Crate>()[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }

    public void SetPlayerToCrate(bool isUp = true)
    {
        float tempHeight;

        if (tempCratesIndex >= crates.Length)
        {
            player.parent = GameObject.FindGameObjectWithTag("LastCrate").transform;
            tempHeight = -0.25f;
        }
        else
        {
            player.parent = crates[tempCratesIndex];

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
            FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegStraight");
            GameObject.FindGameObjectWithTag("LeftLeg").GetComponent<Animation>().Play();
        }
        else
        {
            Debug.Log("STARTED STANKY LEG");
            GameObject.FindGameObjectWithTag("LeftLeg").GetComponent<Animation>().Play("LeftLegAnim2");
        }

        while (Vector3.Distance(player.localPosition, targetPos) > 0.001f)
        {
            player.localPosition = Vector3.MoveTowards(player.localPosition, targetPos, 1f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        //if (!isUpgo)
        //    FindObjectOfType<RightLeg>().GetComponent<Animation>().Play("RightLegDown2");
    }
}

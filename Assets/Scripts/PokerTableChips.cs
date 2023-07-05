using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerTableChips : MonoBehaviour
{
    [HideInInspector] public bool hasChips = false;

    public int[] childCounts;
    public GameObject chipHolder;

    public Transform GetNewFreeChildAt(int index)
    {
        childCounts[index]++;
        return transform.GetChild(index).GetChild(childCounts[index]);
    }

    IEnumerator DecreaseChips()
    {
        while (true)
        {
            if (tableSpawnMoney.playersAtTable >= 1)
            {
                if ((childCounts[0] < 0) && (childCounts[1] < 0) && (childCounts[2] < 0))
                {
                    if (hasChips)
                    {
                        hasChips = false;
                        //foreach (Guest guest in GetComponentInParent<PokerTable>().GetComponentsInChildren<Guest>())
                        //{
                        //    guest.GetAngry();
                        //}

                        tableSpawnMoney.canSpawn = false;
                        GetComponentInParent<PokerTable>().transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                        GetComponentInParent<PokerTable>().transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                        GetComponentInParent<PokerTable>().transform.GetChild(0).GetChild(3).Find("Error").gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (!hasChips)
                    {
                        hasChips = true;
                        //foreach (Guest guest in GetComponentInParent<PokerTable>().GetComponentsInChildren<Guest>())
                        //{
                        //    guest.StopAngry();
                        //}

                        tableSpawnMoney.canSpawn = true;
                        tableSpawnMoney.SpawnMoney();
                        GetComponentInParent<PokerTable>().transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                        GetComponentInParent<PokerTable>().transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                        GetComponentInParent<PokerTable>().transform.GetChild(0).GetChild(3).Find("Error").gameObject.SetActive(false);
                    }

                    int randomIndex;

                    do
                    {
                        randomIndex = Random.Range(0, 3);
                    } while (childCounts[randomIndex] < 0);

                    Destroy(transform.GetChild(randomIndex).GetChild(childCounts[randomIndex]).GetChild(0).gameObject);
                    childCounts[randomIndex]--;
                }

            }

            yield return new WaitForSeconds(0.3f);
        }
    }


    public void MoveAllChipsToTable(Transform chipsHolderT)
    {
        //PlayerChipsHolder chipsHolder = chipsHolderT.GetComponentInChildren<PlayerChipsHolder>();

        //List<Transform> chips0 = chipsHolder.GetChipsOfType(0);
        //for (int i = 0; i < chips0.Count; i++)
        //{
        //    childCounts[0]++;
        //    chips0[i].parent = transform.GetChild(0).GetChild(childCounts[0]);
        //    chips0[i].localPosition = Vector3.zero;
        //    chips0[i].localRotation = Quaternion.identity;
        //    //chips0[i].Rotate(Vector3.right, 90f);
        //}

        //List<Transform> chips1 = chipsHolder.GetChipsOfType(1);
        //for (int i = 0; i < chips1.Count; i++)
        //{
        //    childCounts[1]++;
        //    chips1[i].parent = transform.GetChild(1).GetChild(childCounts[1]);
        //    chips1[i].localPosition = Vector3.zero;
        //    chips1[i].localRotation = Quaternion.identity;
        //    //chips1[i].Rotate(Vector3.right, 90f);
        //}

        //List<Transform> chips2 = chipsHolder.GetChipsOfType(2);
        //for (int i = 0; i < chips2.Count; i++)
        //{
        //    childCounts[2]++;
        //    chips2[i].parent = transform.GetChild(2).GetChild(childCounts[2]);
        //    chips2[i].localPosition = Vector3.zero;
        //    chips2[i].localRotation = Quaternion.identity;
        //    //chips2[i].Rotate(Vector3.right, 90f);
        //}

        //chipsHolder.ResetChildCounts();
    }

    private SpawnMoneyPos tableSpawnMoney;

    void Start()
    {
        tableSpawnMoney = GetComponentInParent<PokerTable>().GetComponentInChildren<SpawnMoneyPos>();


        float pacing = 0.017f;

        for (int i = 0; i < 250; i++)
        {
            GameObject obj1 = Instantiate(chipHolder, transform.GetChild(0));
            obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
            obj1.transform.localRotation = Quaternion.identity;
            obj1.transform.Rotate(Vector3.right, 90f);
        }
        for (int i = 0; i < 250; i++)
        {
            GameObject obj1 = Instantiate(chipHolder, transform.GetChild(1));
            obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
            obj1.transform.localRotation = Quaternion.identity;
            obj1.transform.Rotate(Vector3.right, 90f);
        }
        for (int i = 0; i < 250; i++)
        {
            GameObject obj1 = Instantiate(chipHolder, transform.GetChild(2));
            obj1.transform.localPosition = Vector3.up * obj1.transform.GetSiblingIndex() * pacing;
            obj1.transform.localRotation = Quaternion.identity;
            obj1.transform.Rotate(Vector3.right, 90f);
        }


        StartCoroutine(SpawnFirstChips());
    }


    IEnumerator SpawnFirstChips()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 20; i++)
        {
            childCounts[0]++;
            GameObject tempChip = Instantiate(chipPref1, transform.GetChild(0).GetChild(childCounts[0]));
            tempChip.transform.localPosition = Vector3.zero;
            tempChip.transform.localRotation = Quaternion.identity;
            tempChip.transform.localScale = new Vector3(5.868542f, 5.868542f, 0.6425489f);
        }
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 15; i++)
        {
            childCounts[1]++;
            GameObject tempChip = Instantiate(chipPref2, transform.GetChild(1).GetChild(childCounts[1]));
            tempChip.transform.localPosition = Vector3.zero;
            tempChip.transform.localRotation = Quaternion.identity;
            tempChip.transform.localScale = new Vector3(5.868542f, 5.868542f, 0.6425489f);
        }
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 10; i++)
        {
            childCounts[2]++;
            GameObject tempChip = Instantiate(chipPref0, transform.GetChild(2).GetChild(childCounts[2]));
            tempChip.transform.localPosition = Vector3.zero;
            tempChip.transform.localRotation = Quaternion.identity;
            tempChip.transform.localScale = new Vector3(5.868542f, 5.868542f, 0.6425489f);
        }


        yield return new WaitForSeconds(0.1f);

        StartCoroutine(DecreaseChips());


    }


    public GameObject chipPref0, chipPref1, chipPref2;

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipTable : MonoBehaviour
{
    public static VipTable Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if ((Time.timeSinceLevelLoad > 4f) && (PlayerPrefs.GetInt("VIP", 0) == 0)/* && (GetComponentInParent<PokerTable>().transform.parent == null)*/)
        {
            PlayerPrefs.SetInt("VIP", 1);
            PlayerPrefs.SetInt("FirstVIP", 1);
        }
    }

    //private void Start()
    //{
    //    if (GetComponentInChildren<FaszomPincerJohet>())
    //    {
    //        Debug.Log("VOOOOTPINCERGECC");
    //        Destroy(GetComponentInChildren<FaszomPincerJohet>());
    //    }
    //}

    public Transform EnableVipTable()
    {
        ArrowController.vip.LookAtThis(transform);
        if (!transform.GetChild(0).gameObject.activeInHierarchy)
            transform.GetChild(0).gameObject.SetActive(true);
        return transform.GetChild(0).GetChild(0).Find("Seat_FoodPosVIP");
    }
    public void DisableVipTable()
    {
        ArrowController.vip.StopLookingAt();

        //GetComponentInChildren<Animation>().Play("ScaleDownAnim");
        //Invoke("DisableThis", 0.6f);
    }

    private void DisableThis()
    {
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void StartDisablingTable()
    {
        Invoke("DisableVipTable", 0.5f);
    }
}

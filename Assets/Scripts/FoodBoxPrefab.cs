using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBoxPrefab : MonoBehaviour
{
    public Transform placeHolder;
    private bool moving = false;

    public void MoveToParent(Transform targetParent)
    {
        if (placeHolder.parent != transform)
            return;
        animation = 0f;
        placeHolder.parent = targetParent;
        StartCoroutine(MoveParabola(targetParent));
    }

    protected float animation;
    IEnumerator MoveParabola(Transform target)
    {
        if (gameObject.name.Contains("Cylinder") && target.name.Contains("Cup") && target.name != "CupPos")
        {
            GetComponentInParent<DrinksFlasksVaultMiddle>().DecreaseLiquid(transform.parent.GetSiblingIndex());

            Transform parent = transform.parent;
            transform.parent = null;
            transform.localScale = new Vector3(2.533543f, 2.533543f, 2.533543f);
            transform.parent = parent;
            transform.GetChild(1).gameObject.SetActive(true);
        }

        moving = true;

        yield return new WaitForSeconds(0.05f);

        transform.parent = null;

        Vector3 parabolaStart;

        parabolaStart = transform.position;

        while ((Vector3.Distance(transform.position, target.position) > 0.25f) && transform.position.y >= 0.7f)
        {
            animation += Time.deltaTime * 1.4f;
            animation = animation % 5f;
            transform.position = MathParabola.Parabola(parabolaStart, target.position, 1.3f, animation % 5f);

            yield return new WaitForEndOfFrame();
        }

        GetComponent<Animation>().Play();

        if (!gameObject.name.Contains("Cylinder"))
        {
            if (target.name.Contains("Tray"))
            {
                target.gameObject.SetActive(true);
                if (target.GetComponentInParent<VipTable>() != null)
                    target.transform.parent.GetComponentInChildren<Guest>().GotFood(target.GetComponent<TrayEnable>().foodIcon.gameObject);
                else
                    target.transform.parent.GetComponentInChildren<Guest>().GotFood();
                Destroy(gameObject);
            }
            else if (target.name.Contains("bag"))
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                GetComponent<MeshRenderer>().enabled = false;
                transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (target.name.Contains("Box") || target.name.Contains("box"))
            {
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else
        {
            if (target.name.Contains("Flask"))
            {
                target.GetComponentInParent<DrinksFlasksVaultMiddle>().IncreaseLiquid(target.GetSiblingIndex());
                transform.GetChild(0).gameObject.SetActive(false);
            }
            if (target.name.Contains("CupPos"))
            {
                //targetT = target;
                //Invoke("GuestFindTableAfterGettingCup", 1.2f);
                target.GetComponentInParent<Guest>().transform.parent = null;
                target.GetComponentInParent<Guest>().FindSeat();

                FindObjectOfType<WaitingLine2>().StepForward();
            }

        }

        transform.parent = target;

        placeHolder.parent = transform;
        placeHolder.localPosition = Vector3.zero;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        //if (target.GetComponentInParent<PlayerMcDonalds>() != null)
        //{
        //    foreach (FoodIconChoose icons in FindObjectsOfType<FoodIconChoose>())
        //        icons.SetColor();
        //}

        moving = false;
    }

    Transform targetT;

    private void GuestFindTableAfterGettingCup()
    {
        targetT.GetComponentInParent<Guest>().transform.parent = null;
        targetT.GetComponentInParent<Guest>().FindSeat();

        FindObjectOfType<WaitingLine2>().StepForward();
    }

    private void Start()
    {
        if (gameObject.name.Contains("Cylinder"))
            placeHolder = transform.Find("PlaceHolder");
    }
}

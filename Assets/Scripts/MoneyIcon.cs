using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIcon : MonoBehaviour
{
    Vector3 parentPos;

    public float movSpeed = 20f;

    private bool canMove = false;

    void OnEnable()
    {
        VibrationManager.Instance.Vibr_LightImpact();

        parentPos = transform.parent.position;
        //Invoke("InvokeCanMove", 0.05f);
        canMove = true;
    }

    private void InvokeCanMove()
    {
        canMove = true;
    }

    [HideInInspector] public int countOfMoney;

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, parentPos, movSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, parentPos) <= 0.1f)
            {
                GetComponentInParent<MoneySpawner>().UpdateMoney(countOfMoney);
                canMove = false;
                gameObject.SetActive(false);
            }
        }
    }
}

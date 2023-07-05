using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsChildOfMiniGrid : MonoBehaviour
{
    private MiniGrid tempGridParent;

    void Start()
    {
        Transform closestMiniGridTransform = FindObjectsOfType<MiniGrid>()[0].transform;

        foreach (MiniGrid grid in FindObjectsOfType<MiniGrid>())
        {
            if ((Vector3.Distance(grid.transform.position, transform.position)) < (Vector3.Distance(closestMiniGridTransform.position, transform.position)))
            {
                closestMiniGridTransform = grid.transform;
            }
        }
        tempGridParent = closestMiniGridTransform.GetComponent<MiniGrid>();
        tempGridParent.child = transform;
    }

    public void Move()
    {
        if (FindObjectOfType<GirlStuff>().tempIndex == 0)
        {
            StartCoroutine(MoveToTarget(FindObjectOfType<GirlStuff>().clothes.transform));
            FindObjectOfType<GirlStuff>().SwitchToGirl();
        }
        else if (FindObjectOfType<GirlStuff>().tempIndex == 1)
            StartCoroutine(MoveToTarget(FindObjectOfType<GirlStuff>().dish.transform));

    }

    IEnumerator MoveToTarget(Transform targetTransform)
    {
        Vector3 targetPos = targetTransform.position;

        while ((Vector3.Distance(transform.position, targetPos) > 0.01f))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 1f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        if (!tempGridParent.isGood)
        {

            FindObjectOfType<GirlStuff>().bomb.SetActive(true);
            //FindObjectOfType<GirlStuff>().React(false);
        }
        FindObjectOfType<GirlStuff>().ActivateNext();
        gameObject.SetActive(false);
    }

    void Update()
    {

    }
}

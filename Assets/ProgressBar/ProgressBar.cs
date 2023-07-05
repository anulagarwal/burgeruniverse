using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage;
    public Transform player, enemy;

    private float maxDistance;
    private Transform finishLine;
    private Transform tempEnemy;

    public AnimationCurve indicatorCurve;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Animal")[0].transform.position.x < -1.2f)
            tempEnemy = GameObject.FindGameObjectsWithTag("Animal")[0].transform;
        else
            tempEnemy = GameObject.FindGameObjectsWithTag("Animal")[1].transform;

        player.localPosition = enemy.localPosition = Vector3.zero;

        finishLine = FindObjectOfType<FinishLine>().transform;

        fillImage.fillAmount = 0f;

        maxDistance = Vector3.Distance(GameObject.FindGameObjectWithTag("Animal").transform.position, finishLine.position);

        Debug.Log("Start Distance:" + maxDistance.ToString());
    }

    void Update()
    {
        if (tempEnemy == null || !tempEnemy.gameObject.activeInHierarchy)
        {
            if (GameObject.FindGameObjectsWithTag("Animal")[0].transform.position.x < -1.2f)
                tempEnemy = GameObject.FindGameObjectsWithTag("Animal")[0].transform;
            else
                tempEnemy = GameObject.FindGameObjectsWithTag("Animal")[1].transform;
        }

        float enemyPos = 1f - (Vector3.Distance(tempEnemy.position, finishLine.position) / maxDistance);
        fillImage.fillAmount = 1f - (Vector3.Distance(AnimalChange.tempAnimal.position, finishLine.position) / maxDistance);
        player.localPosition = new Vector3(indicatorCurve.Evaluate(fillImage.fillAmount), 0f, 0f);
        enemy.localPosition = new Vector3(indicatorCurve.Evaluate(enemyPos), 0f, 0f);
    }
}

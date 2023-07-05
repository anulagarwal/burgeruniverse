using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExchangeIcon : MonoBehaviour
{
    public Image fillImage;
    public GameObject defaultImages, fullText;

    [HideInInspector] public bool isFull = false;
    private float targetFillAmount;

    private PutDownBag putDownBag;

    private void Start()
    {
        putDownBag = transform.parent.GetComponentInChildren<PutDownBag>();
    }

    private void OnEnable()
    {
        putDownBag = transform.parent.GetComponentInChildren<PutDownBag>();
        StartCoroutine(Checking());
    }

    //public void StartChecking()
    //{
    //    StartCoroutine(Checking());
    //}
    IEnumerator Checking()
    {
        while (true)
        {
            CheckIfFullOrFree(putDownBag.maxBoxesCount, putDownBag.boxesHolder.GetComponentsInChildren<FoodBoxPrefab>().Length);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void LateUpdate()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillAmount, 7f * Time.deltaTime);
    }

    public void CheckIfFullOrFree(int countOfMax, int countOfTemp)
    {
        targetFillAmount = (float)countOfTemp / (float)countOfMax;

        if (countOfTemp >= countOfMax)
            SetFull();
        else
            SetFree();
    }

    private void SetFull()
    {
        isFull = true;
        defaultImages.SetActive(false);
        fullText.SetActive(true);
    }

    private void SetFree()
    {
        isFull = false;
        defaultImages.SetActive(true);
        fullText.SetActive(false);
    }
}

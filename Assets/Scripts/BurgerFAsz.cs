using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerFAsz : MonoBehaviour
{
    public void StartBurgering()
    {
        StartCoroutine(Burgering());
    }

    IEnumerator Burgering()
    {
        yield return new WaitForSeconds(0.5f);

        int i = 3;
        while (i >= 1)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            i--;
            yield return new WaitForSeconds(0.3f);
        }



        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
    }

}

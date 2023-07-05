using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshUpgrades : MonoBehaviour
{
    public void Refresh()
    {
        StartCoroutine(DoRefresh());
    }

    IEnumerator DoRefresh()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesOrNoCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject hand;

    private void HandIn()
    {
        hand.SetActive(true);
        //hand.GetComponentInParent<HighlightPlus.HighlightEffect>().highlighted = true;
        //hand.GetComponentInParent<HighlightPlus.HighlightEffect>().enabled = true;
    }
}

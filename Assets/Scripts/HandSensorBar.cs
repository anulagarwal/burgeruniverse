using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSensorBar : MonoBehaviour
{
    Image barIm;

    public float increaseBy = 0.1f;

    void Start()
    {
        barIm = Bar.barImage;
        barIm.fillAmount = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Hand"))
        {
            GetComponentInChildren<ParticleSystem>().Play();
            barIm.fillAmount += increaseBy;
            if (barIm.fillAmount >= 1f)
            {
                FindObjectOfType<HitmanTaskManager>().ActivateNextText();
                FindObjectOfType<Piano>().gameObject.AddComponent<Rigidbody>();
                GetComponentInChildren<ParticleSystem>().transform.parent = null;
                Destroy(this);
            }
        }

    }
}

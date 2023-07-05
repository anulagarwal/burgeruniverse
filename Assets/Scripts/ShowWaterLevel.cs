using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWaterLevel : MonoBehaviour
{
    public bool canMove = true;

    private bool isShowActive = false;

    public Color[] colors;
    public SpriteRenderer[] renderers;

    private void Start()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = colors[0];
        }
    }

    void Update()
    {
        if (canMove)
        {
            transform.position += transform.up * 0.04f * Time.deltaTime;

            if (!isShowActive && transform.localPosition.y >= 0f)
            {
                isShowActive = true;
                transform.Find("Show").gameObject.SetActive(true);
            }

            if ((transform.localPosition.y > -0.069f) && (transform.localPosition.y < -0.04f))
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].color = colors[1];
                }
            }
            else if ((transform.localPosition.y > -0.04f) && (transform.localPosition.y < -0.016f))
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].color = colors[2];
                }
            }
            else if ((transform.localPosition.y > -0.016f) && (transform.localPosition.y < 0f))
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].color = colors[3];
                }
            }
            else if ((transform.localPosition.y > 0f) && (transform.localPosition.y < 0.0273f))
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].color = colors[4];
                }
            }
            if (transform.localPosition.y >= 0.0273f)
            {
                canMove = false;
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].color = colors[5];
                }
            }
        }
    }
}

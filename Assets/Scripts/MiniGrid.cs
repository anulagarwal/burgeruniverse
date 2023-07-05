using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGrid : MonoBehaviour
{
    public Transform child;

    public Material selectedMat, goodMat, badMat, defaultMat;

    private MeshRenderer rend;

    public bool isGood = false;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (isClicking)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isClicking = false;
                if (isGood)
                    rend.material = goodMat;
                else
                {
                    rend.material = badMat;
                    FindObjectOfType<GirlStuff>().isFail = true;
                }

                child.GetComponent<SetAsChildOfMiniGrid>().Move();
            }
        }
    }

    private bool isClicking = false;

    private void OnMouseEnter()
    {
        if (!isClicking && Input.GetMouseButton(0))
        {
            isClicking = true;
            rend.material = selectedMat;
        }
    }

    private void OnMouseDown()
    {
        if (!isClicking && Input.GetMouseButton(0))
        {
            isClicking = true;
            rend.material = selectedMat;
        }
    }


}

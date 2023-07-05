using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatHolder : MonoBehaviour
{
    public Material bakeMat, bakedMat;

    private bool canBake = false;

    private bool isIn = false;

    public Transform arrow, meat, meatHolder, camMeat;

    private Quaternion startRot;
    private Vector3 startPos;

    private void Start()
    {
        startPos = meat.localPosition;
        startRot = meat.localRotation;

        Invoke("PlayAnim", 0.9f);
    }

    private void PlayAnim()
    {
        GetComponent<Animation>().Play();
    }

    void Update()
    {
        if (canBake)
        {
            bakeMat.Lerp(bakeMat, bakedMat, 0.56f * Time.deltaTime);
            arrow.Translate(Vector3.right * 53f * Time.deltaTime);
            //bakeMat.color=Color.Lerp(bakeMat.color, )
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (GameObject.FindGameObjectWithTag("TutPush") != null)
                GameObject.FindGameObjectWithTag("TutPush").SetActive(false);
            if (isIn)
            {
                GetComponent<Animation>().Play("MeatIn");
            }
            else
            {
                GetComponent<Animation>().Play("MeatOut");
            }
            isIn = !isIn;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.parent = camMeat;
            camMeat.GetComponent<Animation>().Play();
            Invoke("AddMeat", 0.8f);

            arrow.parent.gameObject.SetActive(false);
        }
    }

    private void AddMeat()
    {
        GetComponent<Animation>().Play("AddMeat");
    }

    public Transform meatToMeatTransform;
    private void MeatToMeat()
    {
        meat.parent = meatToMeatTransform;
        meat.localPosition = Vector3.zero;
        meat.localRotation = Quaternion.identity;

        FindObjectOfType<BurgerFAsz>().StartBurgering();
    }

    private void StopBaking()
    {
        meat.parent = transform;
        meat.localPosition = startPos;
        meat.localRotation = startRot;
        canBake = false;
    }

    private void StartBaking()
    {
        meat.parent = meatHolder;
        meat.localPosition = Vector3.zero;
        meat.localRotation = Quaternion.identity;
        canBake = true;
    }
}

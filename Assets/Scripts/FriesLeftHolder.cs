using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriesLeftHolder : MonoBehaviour
{
    public Material bakeMat, bakedMat;

    private bool canBake = false;

    private bool isIn = false;

    public bool isFry = true;
    public Transform arrow;

    void Update()
    {
        if (canBake)
        {
            if (isFry)
                bakeMat.Lerp(bakeMat, bakedMat, 0.5f * Time.deltaTime);

            if (arrow.localPosition.x < 160f)
                arrow.Translate(Vector3.right * 69f * Time.deltaTime);
            //bakeMat.color=Color.Lerp(bakeMat.color, )
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isIn)
                GetComponent<Animation>().Play("FriesIn");
            else
                GetComponent<Animation>().Play("FriesOut");
            isIn = !isIn;
        }
    }

    private void StopBaking()
    {
        canBake = false;
    }

    public Animation oilAnim;
    private void StartBaking()
    {
        if (!isFry)
            oilAnim.Play();
        canBake = true;
    }
}

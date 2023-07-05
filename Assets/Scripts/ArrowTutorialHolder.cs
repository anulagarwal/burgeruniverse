using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTutorialHolder : MonoBehaviour
{
    public static ArrowTutorialHolder Instance;
    private DynamicJoystick joystick;

    private void Awake()
    {
        Instance = this;
    }

    int tempIndex = 0;

    public void EnableNextTutAfter(float after, bool isNext = false)
    {
        Invoke("EnableNextTutSiman", after);
    }

    public void EnableNextTutSiman()
    {
        if (tempIndex < PlayerPrefs.GetInt("TutShow", 0))
            return;

        ArrowController.tut.LookAtThis(transform.GetChild(tempIndex));

        transform.GetChild(tempIndex).gameObject.SetActive(true);

        if (transform.GetChild(tempIndex).name.Contains("NoColl"))
            transform.GetChild(tempIndex).GetComponent<Collider>().enabled = false;
        else
            transform.GetChild(tempIndex).GetComponent<Collider>().isTrigger = false;

        transform.GetChild(tempIndex).Find("CamCasino_Tut").gameObject.SetActive(true);
        Invoke("CamOff", 2f);
    }

    public void EnableNextTut(int indexTut = -1)
    {
        if (indexTut < PlayerPrefs.GetInt("TutShow", 0))
            return;

        if (indexTut > 0)
        {
            tempIndex = indexTut;
            for (int i = 0; i < tempIndex - 1; i++)
            {
                if (transform.GetChild(i).gameObject.name.Contains("Arrow"))
                {
                    if (transform.GetChild(i).gameObject.name.Contains("VIP"))
                    {
                        transform.GetChild(i).name = "VOOTMAR";
                    }
                    else
                    {
                        if (Time.timeSinceLevelLoad > 5f)
                            transform.GetChild(i).gameObject.SetActive(true);

                        transform.GetChild(i).name = "VOOTMAR";
                    }
                }
            }
        }

        ArrowController.tut.LookAtThis(transform.GetChild(tempIndex));

        if (!transform.GetChild(tempIndex).gameObject.name.Contains("VIP"))
            transform.GetChild(tempIndex).gameObject.SetActive(true);

        if (transform.GetChild(tempIndex).name.Contains("NoColl"))
            transform.GetChild(tempIndex).GetComponent<Collider>().enabled = false;
        else
            transform.GetChild(tempIndex).GetComponent<Collider>().isTrigger = false;

        if (!transform.GetChild(tempIndex).name.Contains("NoColl"))
        {
            transform.GetChild(tempIndex).Find("CamCasino_Tut").gameObject.SetActive(true);
        }
        Invoke("CamOff", 2f);
    }

    private void CamOff()
    {
        transform.GetChild(tempIndex).Find("CamCasino_Tut").gameObject.SetActive(false);
        PlayerPrefs.SetInt("TutShow", tempIndex);
        transform.GetChild(tempIndex).GetComponent<Collider>().isTrigger = true;
        tempIndex++;
    }

    void Start()
    {
        joystick = FindObjectOfType<DynamicJoystick>();

        tempIndex = (PlayerPrefs.GetInt("TutShow", 0));

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);

            if ((i < tempIndex)/* && (PlayerPrefs.GetInt("TutShow", 0) != 0)*/)
                transform.GetChild(i).name = "VOOTMAR";
        }

        if (tempIndex < transform.childCount - 1)
            EnableNextTutAfter(1.8f);
    }
}

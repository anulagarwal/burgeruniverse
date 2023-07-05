using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsIfNotFirst : MonoBehaviour
{
    public GameObject[] objectsToDestroy;

    public Collider enableColl;

    void OnEnable()
    {
        if (gameObject.name.Contains("TUTORIAL"))
        {
            if (GameObject.FindGameObjectWithTag("Securities").transform.GetChild(0).gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("TutorialWas", 1);
                Destroy(gameObject);
                return;
            }

            if (PlayerPrefs.GetInt("TutorialWas", 0) == 0)
                PlayerPrefs.SetInt("TutorialWas", 1);
            else
                Destroy(gameObject);
        }
        else if (Time.timeSinceLevelLoad < 7f)
        {
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
            Invoke("EnableCollR", 0.5f);
        }
    }

    //void Start()
    //{
    //    if (gameObject.name.Contains("TUTORIAL"))
    //    {
    //        if (GameObject.FindGameObjectWithTag("Securities").transform.GetChild(0).gameObject.activeInHierarchy)
    //        {
    //            PlayerPrefs.SetInt("TutorialWas", 1);
    //            Destroy(gameObject);
    //            return;
    //        }

    //        if (PlayerPrefs.GetInt("TutorialWas", 0) == 0)
    //            PlayerPrefs.SetInt("TutorialWas", 1);
    //        else
    //            Destroy(gameObject);
    //    }
    //    else if (Time.timeSinceLevelLoad < 7f)
    //    {
    //        foreach (GameObject obj in objectsToDestroy)
    //        {
    //            Destroy(obj);
    //        }
    //        Invoke("EnableCollR", 0.5f);
    //    }
    //}

    private void EnableCollR()
    {
        if (enableColl != null)
            enableColl.enabled = true;
    }
}

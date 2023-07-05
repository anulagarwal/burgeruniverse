using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    //private Joystick joystick;

    public List<bool> isAttractedToChild = new List<bool>();

    public static Attract Instance;

    private void Awake()
    {
        if (gameObject.name.Contains("Attract"))
            Instance = this;
    }

    void Start()
    {
        //joystick = FindObjectOfType<Joystick>();

        for (int i = 0; i < transform.childCount; i++)
        {
            isAttractedToChild.Add(false);
        }
    }

    public Transform AddToAttract(Hip hip)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!isAttractedToChild[i])
            {
                isAttractedToChild[i] = true;
                return transform.GetChild(i);
            }
        }
        return null;
    }

    public void DeleteAttraction(Transform attractionTransform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) == attractionTransform)
            {
                isAttractedToChild[i] = false;
            }
        }
    }

    void Update()
    {
        //if (Mathf.Abs(joystick.Vertical) < 0.25f)
        //return;

        //transform.position = new Vector3(transform.position.x, transform.position.y + joystick.Vertical * Time.deltaTime * 15f, transform.position.z);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0.75f, 5.55f), transform.position.z);
    }
}

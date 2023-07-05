using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigHitman : MonoBehaviour
{
    public GameObject activateImage, deactivateText, activateText;
    public Transform displays, climb;

    public Animation camAnim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayAnim("Climb");
            activateImage.SetActive(true);
            deactivateText.SetActive(false);
            activateText.SetActive(true);
            camAnim.Play();
        }
    }

    public GameObject agent1, agent2, displays2;

    public void EnableRig()
    {
        //displays.gameObject.SetActive(true);
        foreach (Rig rig in FindObjectsOfType<Rig>())
        {
            rig.weight = 1f;
        }
        GetComponentInParent<Animator>().Play("Def");
        transform.parent = null;

        displays2.SetActive(true);
        //agent2.SetActive(true);
        //agent1.SetActive(false);
    }

    public void PlayAnim(string name)
    {
        displays.gameObject.SetActive(false);
        if (name == "Climb")
        {
            transform.parent.parent = climb;
            climb.GetComponent<Animation>().Play();
        }
        GetComponent<Rig>().weight = 0f;
        GetComponentInParent<Animator>().Play(name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigHitman2 : MonoBehaviour
{
    public GameObject disableText, enableText, enableImage;
    public Transform displays, climb;

    public Animation camAnim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            disableText.SetActive(false);
            enableText.SetActive(true);
            enableImage.SetActive(true);

            PlayAnim("LayDown");
            camAnim.Play("Cam2");
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

    public GameObject weapon, disableWeapon;
    public Animation caseAwayAnim;

    public void PlayAnim(string name)
    {
        displays.gameObject.SetActive(false);
        if (name == "Climb")
        {
            transform.parent.parent = climb;
            climb.GetComponent<Animation>().Play();
        }
        else if (name == "LayDown")
        {
            disableWeapon.SetActive(false);
            weapon.SetActive(true);
            caseAwayAnim.Play();
        }
        GetComponent<Rig>().weight = 0f;
        GetComponentInParent<Animator>().Play(name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GirlHolderRestaurant2 : MonoBehaviour
{
    public Animation chairBackAnim;

    private Animator detective;

    public MultiAimConstraint headConstraint;
    public GameObject angryEmoji;

    public GameObject newspaper, newspaperTable;

    public void GirlStandUp()
    {
        newspaperTable.SetActive(true);
        newspaper.SetActive(false);

        angryEmoji.SetActive(true);
        FindObjectOfType<ProgressBarFill>().transform.parent.gameObject.SetActive(false);

        headConstraint.weight = 0f;
        GetComponentInChildren<RestaurantGirl>().GameWon();

        foreach (TwoBoneIKConstraint constraint in transform.GetComponentsInChildren<TwoBoneIKConstraint>())
        {
            constraint.weight = 0f;
        }

        GetComponentInChildren<Animator>().SetBool("standUp", true);
        GetComponentInChildren<Animator>().SetLayerWeight(3, 1f);//AngryFace
        chairBackAnim.Play();
        GetComponent<Animation>().Play();
    }

    public Rig detectiveRig;
    public GameObject bubbleSpeak;
    private void Pissed()
    {
        bubbleSpeak.SetActive(true);

        detective.SetBool("fail", true);
        detective.SetLayerWeight(2, 0f);
        detectiveRig.GetComponent<Animation>().Play();
        detective.transform.parent.GetComponent<Animation>().Play();
        detective.transform.parent.parent.Find("Chair").GetComponent<Animation>().Play();


        GetComponentInChildren<Animator>().SetBool("pissed", true);
        GetComponentInChildren<Animator>().SetLayerWeight(3, 0f);//AngryFace
        GetComponentInChildren<Animator>().SetLayerWeight(2, 0f);
        GetComponentInChildren<Animator>().SetLayerWeight(1, 0f);

    }

    public GameObject girlSpawnCam;

    private void Walk()
    {
        GetComponentInChildren<Animator>().SetBool("walk", true);
        girlSpawnCam.SetActive(true);
    }

    void Start()
    {
        detective = FindObjectOfType<Detective>().GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            GirlStandUp();
    }

    private void Slap()
    {
        FindObjectOfType<Detective>().EnableRagdoll();
    }
}

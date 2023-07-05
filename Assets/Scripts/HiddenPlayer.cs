using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HiddenPlayer : MonoBehaviour
{
    private void LetsPlay()
    {
        transform.Find("Rig 1").gameObject.SetActive(true);

        Invoke("Stopfollow", 0.2f);
    }

    private void Stopfollow()
    {
        foreach (MoveTarget targ in transform.GetComponentsInChildren<MoveTarget>())
        {
            targ.canFollowPart = false;
            targ.SetWeightDown();
        }

        FindObjectOfType<ProgressBarFill>().transform.parent.gameObject.GetComponent<Animation>().Play();
        FindObjectOfType<ProgressBarFill>().GetComponent<Animation>().Play();
        transform.Find("Rig 1").GetComponent<Rig>().weight = 1f;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Find("Rig 1").gameObject.SetActive(true);

            foreach (MoveTarget circle in transform.GetComponentsInChildren<MoveTarget>())
            {
                circle.gameObject.SetActive(false);
                circle.transform.localScale = Vector3.zero;
            }

            foreach (LookAtCam circle in transform.GetComponentsInChildren<LookAtCam>())
            {
                circle.gameObject.SetActive(false);
                circle.transform.localScale = Vector3.zero;
            }

            transform.Find("Rig 1").GetComponent<Animation>().Play();
            FindObjectOfType<CamShake>().Shake();

            GetComponent<Animator>().Play("Hide_Cry");
            transform.Find("CAGE").gameObject.SetActive(true);
            Invoke("Cry", 0.5f);
        }
    }

    private void Cry()
    {
        transform.Find("EmojiCry").gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCharacterController : MonoBehaviour {


    [HideInInspector] public Animator CurrentAnimator;

    public Transform CameraMain;
   
    private float h;
    private float v;

    private Vector3 AdditionalVector;

    void Start()
    {
        CurrentAnimator = GetComponent<Animator>();
    }
    
  
    float HandFaderPos;
    float HandFaderRot;
    void OnAnimatorIK()
    {
        if (CurrentAnimator)
        {
            CurrentAnimator.SetLookAtPosition(CameraMain.transform.position+CameraMain.transform.forward*2);
            CurrentAnimator.SetLookAtWeight(1);
        }
    }



	void Update () 
    {
	    h = Input.GetAxis("Horizontal");
	    v = Input.GetAxis("Vertical");           

        Vector3 forward = transform.forward;
        if (forward != Vector3.zero)
        {
            Vector3 inputVectorRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            if (inputVectorRaw != Vector3.zero)
                linearTargetDirection = CameraMain.transform.rotation * inputVectorRaw;

            forward = Vector3.RotateTowards(forward, linearTargetDirection, Time.deltaTime * (1f / 0.3f), 1f);
            if (forward != Vector3.zero)
            {
                forward.y = 0f;
                if (Quaternion.LookRotation(forward) != Quaternion.identity)
                    transform.rotation = Quaternion.LookRotation(forward);
            }
        }

        float finalV = Mathf.Lerp(CurrentAnimator.GetFloat("Vertical"), v,Time.deltaTime * 6f);
        float finalH = Mathf.Lerp(CurrentAnimator.GetFloat("Horizontal"), h,Time.deltaTime * 6f);

        CurrentAnimator.SetBool("Sprint", Input.GetKey(KeyCode.LeftShift));

        CurrentAnimator.SetFloat("Horizontal", finalH);
        CurrentAnimator.SetFloat("Vertical", finalV);
    }
    private Vector3 linearTargetDirection;
}

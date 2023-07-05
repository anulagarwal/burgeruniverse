using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressYesOrNo : MonoBehaviour
{
    public Animation flashAnim;

    public Transform buttonPos;

    private void OnMouseUp()
    {
        FindObjectOfType<TargetYesOrNo>().StartPushingButton(buttonPos.position, GetComponent<Animation>());
    }

    private void MoveHandBack()
    {
        FindObjectOfType<TargetYesOrNo>().WeightToZero();
    }

    private void FlashPicture()
    {
        flashAnim.Play();
    }

    private void DoChoice()
    {
        FindObjectOfType<HandYesOrNo>().DoChoice(gameObject.name.Contains("YES"));
    }
}

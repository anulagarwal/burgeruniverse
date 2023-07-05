using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFaszpuncika : MonoBehaviour
{
    public Animation faceAnim;

    private void StartFaceMoving()
    {
        faceAnim.Play();
    }

    private void SendSelfie()
    {
        FindObjectOfType<TakeSelfie>().PhoneBack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    private void StopPlayer()
    {
        FindObjectOfType<RigHitman>().EnableRig();
    }
}

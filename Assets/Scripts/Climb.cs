using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    private void NextAnim()
    {
        transform.GetComponentInChildren<Animator>().SetBool("climbUp", true);
    }

    public void UnparentChild()
    {
        transform.GetChild(0).parent = null;
    }
}

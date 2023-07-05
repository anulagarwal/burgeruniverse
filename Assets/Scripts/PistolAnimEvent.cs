using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAnimEvent : MonoBehaviour
{
    private void ConfettiPlay()
    {
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
        Invoke("StopConfetti", 2f);
    }

    private void StopConfetti()
    {
        Camera.main.transform.GetChild(0).gameObject.SetActive(false);
    }


    public void FollowObj(int objIndex)
    {
        if (objIndex == 1)
            FindObjectOfType<PointerFollowGun>().StartFollow(transform.GetChild(objIndex).GetChild(0));
        else
            FindObjectOfType<PointerFollowGun>().StartFollow(transform.GetChild(objIndex));
    }

    private void StopFollow()
    {
        FindObjectOfType<PointerFollowGun>().StopFollowing();

    }
}

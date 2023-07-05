using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPref;

    public void SpawnBall()
    {
        //Instantiate(ballPref, transform.position, Quaternion.identity).transform.Rotate(Vector3.up, 90f);
        GetComponent<Animation>().Play();
        HandHolderForEmoji.Instance.gameObject.SetActive(true);
    }

    private void SpawnIt()
    {
        Instantiate(ballPref, transform.position, Quaternion.identity).transform.Rotate(Vector3.up, 90f);
        HandHolderForEmoji.Instance.gameObject.SetActive(false);
    }
}

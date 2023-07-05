using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    private void Attack()
    {
        transform.GetChild(0).GetComponent<Animation>().Play();
        FindObjectOfType<BoomerGameManager>().swipeBall.SetActive(true);

        Destroy(gameObject, 0.95f);
    }
}

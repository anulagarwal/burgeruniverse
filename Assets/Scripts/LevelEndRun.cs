using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndRun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player_Character"))
        {
            other.GetComponentInParent<CubesHolder>().canMove = false;
            other.GetComponent<TypeRunCharacter>().Win();
            GetComponent<Animator>().SetBool("kiss", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRun : MonoBehaviour
{
    public void EnableRagdoll(bool canEnable = true)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = canEnable;
        }
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = !canEnable;
        }
    }

    private float boundaries = 2f;
    private float startX;

    private bool canRun = true;

    private void ShootWithLaser()
    {
        GetComponentInChildren<LaserRobber>().Shoot();
    }

    public void ShootEnd()
    {
        GetComponent<Animator>().SetBool("shoot", true);
        GetComponent<Animator>().SetBool("run", false);
        canRun = false;
    }

    public void StopPlayer()
    {
        Invoke("StartRun", 1f);
        GetComponent<Animator>().SetBool("run", false);
        canRun = false;
    }

    private void StartRun()
    {
        GetComponent<Animator>().SetBool("run", true);
        canRun = true;
    }

    private void Update()
    {
        if (!canRun)
            return;

        transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(Vector3.up, 180f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -25f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 25f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            float newPosX = transform.position.x + 2f * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(newPosX, startX - boundaries, startX + boundaries), transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            float newPosX = transform.position.x - 2f * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(newPosX, startX - boundaries, startX + boundaries), transform.position.y, transform.position.z);
        }
    }

    void Start()
    {
        startX = transform.position.x;
        //EnableRagdoll(false);

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = true;
        }
    }

    public GameObject[] replaceNames;

    private void Dance()
    {
        for (int i = 0; i < replaceNames.Length; i++)
        {
            string tempName = replaceNames[i].name;
            tempName = tempName.Replace("TP", "TT");
            replaceNames[i].name = tempName;
        }

        GetComponent<Animation>().Play();
    }

    public void StartDance()
    {
        Invoke("Dance", 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Obi;
using Cinemachine;

public class Player : MonoBehaviour
{

    //[HideInInspector] public Joystick joystick;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody rb;

    private float movementSpeed = 16f;

    public bool canMove = true;


    public Collider coll;
    private Collider[] collidersInChildren;
    private Rigidbody[] rigidbodiesInChildren;
    private SkinnedMeshRenderer meshRend;

    public static Player PlayerInstance;

    IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * 14f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    //private void Awake()
    //{
    //    if (gameObject.name.Contains("Player"))
    //        PlayerInstance = this;
    //}

    public void Win()
    {
        canMove = false;
        animator.Play("Dance4");
    }


    public GameObject endPanel, clearedPanel;


    private void OnCollisionEnter(Collision collision)
    {


    }

    public Material[] skins;


    void Start()
    {
        //SKIN SELECT
        transform.GetChild(0).gameObject.SetActive(false);
        int randomIndex = Random.Range(0, transform.childCount - 1);
        transform.GetChild(randomIndex).gameObject.SetActive(true);
        transform.GetChild(randomIndex).GetComponent<SkinnedMeshRenderer>().material = skins[Random.Range(0, skins.Length)];

        canMove = true;

        //joystick = FindObjectOfType<Joystick>();
        animator = transform.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();


        collidersInChildren = transform.Find("Root").GetComponentsInChildren<Collider>();
        rigidbodiesInChildren = transform.Find("Root").GetComponentsInChildren<Rigidbody>();
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();

        EnableRagdoll(false);
    }

    void Update()
    {

    }


    public void EnableRagdoll(bool canEnable, bool isEnemy = false, Transform enemyPos = null)
    {
        animator.enabled = !canEnable;

        //if (canEnable)
        //{
        //    if (GetComponentInChildren<FixedJoint>() == null)
        //        animator.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.AddComponent<FixedJoint>().connectedBody = rb;
        //}
        //else
        //{
        //    foreach (FixedJoint joint in GetComponentsInChildren<FixedJoint>())
        //    {
        //        Destroy(joint);
        //    }
        //}

        foreach (var collider in collidersInChildren)
        {
            if (collider != null)
                collider.enabled = canEnable;
        }

        foreach (var rb in rigidbodiesInChildren)
        {
            if (rb != null)
                rb.isKinematic = !canEnable;
        }

        coll.enabled = !canEnable;
        rb.isKinematic = false;


        if (canEnable && !isEnemy)
        {
            transform.GetComponentInChildren<Hip>().StartLevitating();
            //transform.GetComponentInChildren<Hip>().GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (canEnable && isEnemy)
            transform.GetComponentInChildren<Hip>().StartMoveTowardEnemy(enemyPos);
    }
}

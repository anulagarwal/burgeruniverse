using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeRunCharacter : MonoBehaviour
{
    public GameObject endPanel, ClearedPanel, endCam;

    private Animator animator;

    private float jumpToHeight, jumpCounts;

    public void Jump(float jumpTo, float countOfJumps)
    {
        jumpCounts = countOfJumps;
        jumpToHeight = jumpTo;
        animator.SetBool("jump", true);

        //StopAllCoroutines();
        //StartCoroutine(Jumping());

        StartLerping();
    }

    public void Win()
    {
        animator.SetBool("kiss", true);
        endCam.SetActive(true);
        Invoke("InvokeWin", 1f);
    }

    private void InvokeWin()
    {
        ClearedPanel.SetActive(true);
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void InvokeLose()
    {
        endPanel.SetActive(true);
    }

    //IEnumerator Jumping()
    //{
    //    Vector3 targetPos = new Vector3(transform.position.x, jumpToHeight, transform.position.z);
    //    while (Vector3.Distance(transform.position, targetPos) > 0.05f)
    //    {
    //        transform.position = Vector3.LerpUnclamped(transform.position, targetPos, 0.3f * Time.deltaTime);

    //        yield return new WaitForEndOfFrame();
    //    }
    //    animator.SetBool("jump", false);
    //}

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        foreach (Collider collider in transform.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }

        GetComponent<Collider>().enabled = true;
    }

    public Animation enemyGroundAnimMoveRight;
    private void MoveEnemyRight()
    {
        transform.parent.GetComponentInParent<Animation>().Play();

        foreach (CubesHolder holder in FindObjectsOfType<CubesHolder>())
        {
            if (!holder.gameObject.name.Contains("Player"))
                holder.tempPosY -= 0.2f;
        }

        if (enemyGroundAnimMoveRight.gameObject != null)
        {
            enemyGroundAnimMoveRight.Play("GoRight");
        }
    }

    public void KillCharacter()
    {
        GetComponent<Animator>().enabled = false;
        GetComponentInParent<CubesHolder>().canMove = false;

        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
        foreach (Collider collider in transform.GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }

        GetComponent<Collider>().enabled = false;

        Invoke("MoveEnemyRight", 0.5f);

        if (gameObject.name.Contains("Player"))
            Invoke("InvokeLose", 1f);
    }

    private bool shouldLerp = false;

    private Vector3 endPosition, startPosition;
    private float timeStartedLerping, lerpTime;

    private void StartLerping()
    {
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, jumpToHeight, transform.position.z);
        lerpTime = 0.3f * jumpCounts;
        timeStartedLerping = Time.time;
        shouldLerp = true;
    }

    private Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }

    void Update()
    {
        if (shouldLerp)
        {
            transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
            if (transform.position == endPosition)
            {
                shouldLerp = false;
                animator.SetBool("jump", false);
            }
        }
    }
}

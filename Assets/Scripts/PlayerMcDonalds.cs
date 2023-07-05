using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMcDonalds : MonoBehaviour
{
    public static PlayerMcDonalds Instance;

    void Awake()
    {
        Instance = this;
    }

    private Rigidbody rb;
    private Animator animator;
    private DynamicJoystick joystick;

    public float movementSpeed = 5f;

    public void DeactivateScooter()
    {
        isScooterOn = false;
        transform.position = new Vector3(transform.position.x, 0.57f, transform.position.z);
        GetComponent<CapsuleCollider>().height = 1.060547f;
        transform.Find("Scooter").gameObject.SetActive(false);
        movementSpeed /= 1.22f;
        transform.Find("PoofScooter").GetComponent<ParticleSystem>().Play();
    }

    public void ActivateScooter()
    {
        isScooterOn = true;
        animator.SetBool("run", false);
        transform.position = new Vector3(transform.position.x, 0.7661529f, transform.position.z);
        GetComponent<CapsuleCollider>().height = 1.45f;
        transform.Find("Scooter").gameObject.SetActive(true);
        movementSpeed *= 1.22f;
        transform.Find("PoofScooter").GetComponent<ParticleSystem>().Play();
    }

    public void MoveAway(Vector3 fromPos)
    {
        StartCoroutine(MovingAway(fromPos));
    }

    IEnumerator MovingAway(Vector3 startCapsulePos)
    {
        Invoke("StopAllCoroutinesScript", 1f);

        startCapsulePos = new Vector3(startCapsulePos.x, transform.position.y, startCapsulePos.z);
        Vector3 targetPos;
        targetPos = transform.position - (startCapsulePos - transform.position).normalized * 4f;

        while (Vector3.Distance(transform.position, startCapsulePos) < 1.18f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 2f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void StopAllCoroutinesScript()
    {
        StopAllCoroutines();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<DynamicJoystick>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Arrow") || other.gameObject.name.Contains("VOOTMAR"))
        {
            other.gameObject.SetActive(false);


            if (other.gameObject.name.Contains("Next"))
                ArrowTutorialHolder.Instance.EnableNextTutAfter(2.5f, true);

            if (other.gameObject.name.Contains("Fast"))
                ArrowTutorialHolder.Instance.EnableNextTutSiman();

            ArrowController.tut.StopLookingAt();


            other.gameObject.name = "VOOTMAR";
        }

        if (other.gameObject.name.Contains("ChipSpawner"))
        {
            RigOn_Chips();

        }
        if (other.gameObject.name.Contains("PokerTable"))
        {
            ////PlayerChipsHolder.Instance.FlyChipsTo(other.GetComponentInChildren<PokerTableChips>());      

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("PutDown"))
        {
            //GetComponentInChildren<PlayerChipsHolder>().StopAllCoroutines();
        }

    }

    public Animation rigAnim;

    public Animation leftHandChip, rightHandChip, cheaterHand;

    private bool isRigOn = false;

    public void RigOn_Chips()
    {
        if (!isRigOn)
        {
            isRigOn = true;
            rightHandChip.Play("IK_Up");
            leftHandChip.Play("IK_Up");
        }
    }

    public void RigOff_Chips()
    {
        if (isRigOn)
        {
            isRigOn = false;
            rightHandChip.Play("IK_Down");
            leftHandChip.Play("IK_Down");
        }
    }

    private void RigOn_Cheater()
    {
        cheaterHand.Play("IK_Up");
    }

    private void RigOff_Cheater()
    {
        cheaterHand.Play("IK_Down");
    }

    private bool isScooterOn = false;

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (joystick.Direction.magnitude > 0.1f)
        {
            direction = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y);
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Prioritize joystick input if it exists.
            if (Mathf.Abs(moveHorizontal) > 0.1f || Mathf.Abs(moveVertical) > 0.1f)
            {
                direction = new Vector3(moveHorizontal, 0f, moveVertical);
            }
        }

        if (direction.magnitude > 0.1f)
        {
            animator.SetBool("run", true);

            Vector3 newDirection = transform.position + direction.normalized * 1000f;
            transform.LookAt(new Vector3(newDirection.x, transform.position.y, newDirection.z));

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Pin"))
            {
                rb.velocity = direction.normalized * 12f;
            }
            else
            {
                rb.velocity = direction.normalized * movementSpeed;
            }
        }
        else
        {
            animator.SetBool("run", false);
            rb.Sleep();
        }
    }

}


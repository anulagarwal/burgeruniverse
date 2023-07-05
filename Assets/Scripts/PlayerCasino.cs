using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasino : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private DynamicJoystick joystick;

    public float movementSpeed = 5f;

    private bool hasCheater = false;
    private Transform cheater;
    private RagdollCharacter cheaterRagdoll;

    public GameObject newTargetOfHandHold;

    private void OnEnable()
    {

        //if (PlayerPrefs.GetInt("PlayerCapacity", 0) != 0)
        //    GetComponentInChildren<PlayerChipsHolder>().capacity = PlayerPrefs.GetInt("PlayerCapacity", 0);
    }

    public void ThrowCheater()
    {


        animator.SetBool("walk", false);

        RigOff_Cheater();

        animator.SetBool("throw", true);


        Invoke("RealThrow", 0.5f);

    }

    private void RealThrow()
    {
        hasCheater = false;

        GameObject obj1 = Instantiate(newTargetOfHandHold, transform.Find("TargetOfHandHold"));
        obj1.transform.localPosition = Vector3.zero;
        obj1.transform.localRotation = Quaternion.identity;
        obj1.transform.localScale = Vector3.one;
        obj1.transform.parent = obj1.transform.parent.parent;

        transform.Find("TargetOfHandHold").gameObject.AddComponent<ThrowAwayTargetOfHandHold>();

        obj1.name = "TargetOfHandHold";

        Invoke("ReleaseCheater", 0.3f);
    }

    private void ReleaseCheater()
    {
        cheaterRagdoll.Release();
    }

    void Start()
    {
        foodHolder = GetComponentInChildren<FoodHolderPlayers>();
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<DynamicJoystick>();
        animator = GetComponentInChildren<Animator>();
    }

    private FoodHolderPlayers foodHolder;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Arrow"))
        {
            other.gameObject.SetActive(false);

            if (other.gameObject.name.Contains("Next"))
                ArrowTutorialHolder.Instance.EnableNextTutAfter(2.5f);
        }

        if (other.gameObject.name.Contains("ChipSpawner"))
        {
            RigOn_Chips();
            //other.GetComponentInChildren<ChipSpawnerMachine>().holder.Add(GetComponentInChildren<PlayerChipsHolder>());
            //other.GetComponentInChildren<ChipSpawnerMachine>().anim.Play();
            //other.GetComponentInChildren<ChipSpawnerMachine>().SpawnChip();
        }
        if (other.gameObject.name.Contains("PokerTable"))
        {
            ////PlayerChipsHolder.Instance.FlyChipsTo(other.GetComponentInChildren<PokerTableChips>());      
            //PlayerChipsHolder chipsh = GetComponentInChildren<PlayerChipsHolder>();
            other.GetComponentInChildren<PokerTableChips>().MoveAllChipsToTable(transform);

            RigOff_Chips();
        }

        if (other.gameObject.name.Contains("Cheater"))
        {
            cheaterRagdoll = other.GetComponentInChildren<RagdollCharacter>();
            if (!cheaterRagdoll.isCheating)
                return;

            cheater = cheaterRagdoll.transform;
            RigOn_Cheater();

            hasCheater = true;
            cheaterRagdoll.EnableRagdoll(transform.Find("TargetOfHandHold"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("ChipSpawner"))
        {
            //other.GetComponentInChildren<ChipSpawnerMachine>().holder.Remove(GetComponentInChildren<PlayerChipsHolder>());
            //other.GetComponentInChildren<ChipSpawnerMachine>().anim.transform.localScale = new Vector3(203.0143f, 167.2f, 169.6077f);
        }
        if (other.gameObject.name.Contains("PokerTable"))
        {
            //RigOff_Chips();
            //PlayerChipsHolder.Instance.StopAllCoroutines();
        }
    }

    public Animation rigAnim;

    public Animation leftHandChip, rightHandChip, cheaterHand;

    private bool isRigOn = false;

    public void RigOn_Chips()
    {
        //rigAnim.Play("RigWeightUp");
        if (!isRigOn)
        {
            isRigOn = true;
            rightHandChip.Play("IK_Up");
            leftHandChip.Play("IK_Up");
        }
    }

    public void RigOff_Chips()
    {
        //rigAnim.Play("RigWeightDown");
        if (isRigOn)
        {
            isRigOn = false;
            rightHandChip.Play("IK_Down");
            leftHandChip.Play("IK_Down");
        }
    }

    private void RigOn_Cheater()
    {
        //rigAnim.Play("RigWeightUp");
        cheaterHand.Play("IK_Up");
    }

    private void RigOff_Cheater()
    {
        //rigAnim.Play("RigWeightDown");
        cheaterHand.Play("IK_Down");
    }

    void Update()
    {
        if (joystick.Direction.magnitude <= 0.1f)
        {
            animator.SetBool("run", false);
            rb.Sleep();
        }
        else
        {
            //TRANSFORM UP

            //-------------------------------------------------------------------------

            animator.SetBool("run", true);
            Vector3 direction = transform.position + new Vector3(joystick.Direction.x, 0f, joystick.Direction.y).normalized * 1000f;
            transform.LookAt(new Vector3(direction.x, transform.position.y, direction.z));

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Pin"))        //HA EZ A PINNES JATEK
                rb.velocity = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y).normalized * 12f;
            else
                rb.velocity = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y).normalized * movementSpeed;

            //-------------------------------------------------------------------------

        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BoxPrefab : MonoBehaviour
//{
//    int type = 0;

//    //public Material[] mats;

//    public PlayerChipsHolder chipHolder;

//    void Start()
//    {
//        //type = Random.Range(0, 3);
//        //GetComponent<MeshRenderer>().material = mats[type];
//        MoveToHand();
//    }

//    //bool canMoveUp = true;

//    public void MoveUpByOne(Transform newParent)
//    {
//        //if (!canMoveUp)
//        //    return;

//        //transform.parent = newParent;
//        //transform.localPosition = Vector3.zero;
//    }

//    public void MoveToHand()
//    {
//        if (chipHolder != null)
//            MoveToTarget(chipHolder.GetNextTransformOf(type));

//    }

//    void Update()
//    {

//    }

//    protected float animation;


//    public void MoveToTarget(Transform targ)
//    {
//        //canMoveUp = false;

//        //if (targ.GetComponentInParent<PlayerMcDonalds>() != null)
//        animation = 0.01f;

//        if (GetComponentInParent<PlayerMcDonalds>() != null)
//            transform.parent = null;


//        //if (targ.GetComponentInParent<PlayerMcDonalds>() == null)
//        //transform.parent = targ;

//        ChipMan chipManScript = targ.GetComponentInParent<ChipMan>();
//        if (chipManScript != null)
//        {
//            if (chipManScript.gameObject.name.Contains("WAITER"))
//                chipManScript.RigOn_Chips(true);
//        }

//        StartCoroutine(MoveParabola(targ));
//    }

//    private bool stillMoving = false;
//    private Transform targ;

//    private void CheckMoving()
//    {
//        if (stillMoving)
//        {
//            StopAllCoroutines();
//            GetComponent<Animation>().Play();

//            if (targ.name.Contains("Tray"))
//            {
//                targ.gameObject.SetActive(true);
//                targ.transform.parent.GetComponentInChildren<Guest>().GotFood();
//                Destroy(gameObject);
//            }
//            else if (targ.name.Contains("bag"))
//            {
//                transform.GetChild(0).gameObject.SetActive(false);
//                transform.GetChild(1).gameObject.SetActive(false);
//                GetComponent<MeshRenderer>().enabled = false;
//                transform.GetChild(2).gameObject.SetActive(true);
//            }
//            else if (targ.name.Contains("Box") || targ.name.Contains("box"))
//            {
//                transform.GetChild(1).gameObject.SetActive(false);
//                transform.GetChild(2).gameObject.SetActive(false);
//                transform.GetChild(0).gameObject.SetActive(true);
//                GetComponent<MeshRenderer>().enabled = true;
//            }

//            transform.parent = targ;
//            transform.localPosition = Vector3.zero;
//            transform.localRotation = Quaternion.identity;


//            stillMoving = false;
//        }
//    }

//    public bool movingParabola = false;

//    IEnumerator MoveParabola(Transform target)
//    {
//        movingParabola = true;
//        targ = target;
//        stillMoving = true;
//        CancelInvoke("CheckMoving");
//        Invoke("CheckMoving", 1f);

//        //float rotSpeed = 540f;
//        ////if (Random.Range(0, 2) == 0)
//        ////    rotSpeed = -540f;

//        //rotSpeed = Random.Range(-510, 510);

//        //GetComponent<Rigidbody>().AddTorque(Vector3.up * rotSpeed);

//        yield return new WaitForSeconds(0.05f);

//        transform.parent = null;

//        Vector3 parabolaStart;

//        parabolaStart = transform.position;

//        while ((Vector3.Distance(transform.position, target.position) > 0.25f) && transform.position.y >= 0.714f)
//        {
//            animation += Time.deltaTime * 1.4f;
//            animation = animation % 5f;
//            transform.position = MathParabola.Parabola(parabolaStart, target.position, 1.3f, animation % 5f);

//            yield return new WaitForEndOfFrame();
//        }

//        stillMoving = false;

//        //Destroy(GetComponent<Rigidbody>());
//        GetComponent<Animation>().Play();

//        if (target.name.Contains("Tray"))
//        {
//            target.gameObject.SetActive(true);
//            target.transform.parent.GetComponentInChildren<Guest>().GotFood();
//            Destroy(gameObject);
//        }
//        else if (target.name.Contains("bag"))
//        {
//            transform.GetChild(0).gameObject.SetActive(false);
//            transform.GetChild(1).gameObject.SetActive(false);
//            GetComponent<MeshRenderer>().enabled = false;
//            transform.GetChild(2).gameObject.SetActive(true);
//        }
//        else if (target.name.Contains("Box") || target.name.Contains("box"))
//        {
//            transform.GetChild(1).gameObject.SetActive(false);
//            transform.GetChild(2).gameObject.SetActive(false);
//            transform.GetChild(0).gameObject.SetActive(true);
//            GetComponent<MeshRenderer>().enabled = true;
//        }

//        transform.parent = target;
//        transform.localPosition = Vector3.zero;
//        transform.localRotation = Quaternion.identity;


//        //transform.Rotate(Vector3.forward, Random.Range(-15f, 15f));

//        //GetComponent<Animation>().Play();

//        //canMoveUp = true;

//        movingParabola = false;
//        if (transform.GetSiblingIndex() != 0 && !transform.parent.name.Contains("CAR"))
//        {
//            Destroy(transform.parent.GetChild(0).gameObject);
//            Debug.Log("DESTROYED FIRS CHILD");

//        }


//        //if (transform.GetComponentInParent<CheckIChildIsMissing>() != null)
//            //transform.GetComponentInParent<CheckIChildIsMissing>().CheckLater();

//        if (transform.GetComponentInParent<PlayerChipsHolder>() != null)
//            transform.GetComponentInParent<PlayerChipsHolder>().CapacityCheck();

//        StopAllCoroutines();
//    }
//}


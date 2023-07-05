//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class McDonaldsBoxSpawnerMachine : MonoBehaviour
//{
//    public GameObject boxPrefab, boxPrefab2;

//    //void Start()
//    //{
//    //    Invoke("SpawnChip", 0.5f);
//    //}

//    public bool canSpawn = true;

//    //[HideInInspector] public Animation anim;

//    //private FoodShelf putDown;
//    //private FoodShelfBag putDownBag;
//    private Transform boxHolder;

//    private void Start()
//    {
//        if (gameObject.name.Contains("GetUp"))
//        {
//            //putDown = transform.parent.GetComponentInChildren<FoodShelf>();
//            putDownBag = transform.parent.GetComponentInChildren<FoodShelfBag>();
//            boxHolder = transform.parent.GetChild(0);

//            Invoke("CheckIfHolderIsEmpty", 3f);
//        }
//        //anim = transform.parent.GetComponentInChildren<Animation>();
//    }

//    private void CheckIfHolderIsEmpty()
//    {
//        for (int i = 0; i < holder.Count; i++)
//        {
//            if (holder[i] == null)
//            {
//                holder.RemoveAt(i);
//                break;
//            }
//        }

//        Invoke("CheckIfHolderIsEmpty", 3f);
//    }

//    [SerializeField]
//    public List<PlayerChipsHolder> holder = new List<PlayerChipsHolder>();

//    public void SpawnChip()
//    {
//        if (gameObject.name.Contains("GetUp"))
//        {
//            if (putDownBag == null)
//            {
//                if (putDown.tempIndex <= 0)
//                    return;
//            }
//            else
//            {
//                if (putDownBag.tempIndex <= 0)
//                    return;
//            }
//        }

//        for (int i = 0; i < holder.Count; i++)
//        {
//            if (holder[i].capacity > holder[i].tempCapacity)
//            {
//                if (gameObject.name.Contains("GetUp"))
//                {
//                    int tempChild = boxHolder.GetComponentsInChildren<BoxPrefab>().Length - 1;
//                    boxHolder.GetComponentsInChildren<BoxPrefab>()[tempChild].chipHolder = holder[i];
//                    boxHolder.GetComponentsInChildren<BoxPrefab>()[tempChild].MoveToHand();
//                    if (putDownBag == null)
//                        putDown.tempIndex--;
//                    else
//                        putDownBag.tempIndex--;
//                }
//                else
//                {
//                    if (Random.Range(0, 2) == 0)
//                        Instantiate(boxPrefab, transform.position, Quaternion.identity).GetComponent<BoxPrefab>().chipHolder = holder[i];
//                    else
//                        Instantiate(boxPrefab2, transform.position, Quaternion.identity).GetComponent<BoxPrefab>().chipHolder = holder[i];
//                }
//                holder[i].tempCapacity++;
//            }
//            else
//            {
//                holder[i].ReachedCapacity();
//                holder.Remove(holder[i]);

//                if (holder.Count == 1)
//                {
//                    //anim.Stop();
//                    //anim.transform.localScale = new Vector3(203.0143f, 167.2f, 169.6077f);
//                }
//            }
//        }

//        if (holder.Count == 0)
//        {
//            canSpawn = false;

//            //transform.parent.GetComponentInChildren<Animation>().Stop();
//        }
//        else
//            canSpawn = true;

//        if (canSpawn)
//            Invoke("SpawnChip", 0.2f);
//        else
//        {
//            //anim.Stop();
//            //anim.transform.localScale = new Vector3(203.0143f, 167.2f, 169.6077f);
//            CancelInvoke("SpawnChip");
//        }
//    }
//}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChipSpawnerMachine : MonoBehaviour
//{
//    public GameObject chipPrefab;

//    //void Start()
//    //{
//    //    Invoke("SpawnChip", 0.5f);
//    //}

//    public bool canSpawn = true;

//    [HideInInspector] public Animation anim;

//    private void Start()
//    {
//        anim = transform.parent.GetComponentInChildren<Animation>();
//    }

//    //[SerializeField]
//    //public List<PlayerChipsHolder> holder = new List<PlayerChipsHolder>();

//    public void SpawnChip()
//    {
//        for (int i = 0; i < holder.Count; i++)
//        {
//            if (holder[i].capacity > holder[i].GetComponentsInChildren<ChipPrefab>().Length)
//                Instantiate(chipPrefab, transform.position, Quaternion.identity).GetComponent<ChipPrefab>().chipHolder = holder[i];
//            else
//            {
//                holder[i].ReachedCapacity();
//                holder.Remove(holder[i]);

//                if (holder.Count == 1)
//                {
//                    anim.Stop();
//                    anim.transform.localScale = new Vector3(203.0143f, 167.2f, 169.6077f);
//                }
//            }
//        }

//        if (holder.Count == 0)
//        {
//            canSpawn = false;
//            transform.parent.GetComponentInChildren<Animation>().Stop();
//        }
//        else
//            canSpawn = true;

//        if (canSpawn)
//            Invoke("SpawnChip", 0.1f);
//        else
//        {
//            anim.Stop();
//            anim.transform.localScale = new Vector3(203.0143f, 167.2f, 169.6077f);
//            CancelInvoke("SpawnChip");
//        }
//    }
//}

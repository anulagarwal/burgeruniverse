using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    Animation anim;

    int tempIndex = 1;

    void Start()
    {
        foodShelf = boxesBag.parent.GetComponentInChildren<FoodUpShelf>();

        anim = GetComponent<Animation>();
        StartCoroutine(LookForward());
        ChangeSkin();
    }

    public void MoveForward()
    {
        tempIndex++;
        if (tempIndex == 4)
        {
            StopAllCoroutines();
            StartCoroutine(Pay());
            return;
        }
        anim.Play("CarAnim" + tempIndex.ToString());
    }

    public LayerMask mask;

    public SpawnMoneyPos moneySpanwer;
    public Transform boxesBag;

    private FoodUpShelf foodShelf;

    IEnumerator Pay()
    {
        bool paid = false;
        while (!paid)
        {
            yield return new WaitForSeconds(0.4f);

            Transform boxToGet = foodShelf.GetLastBox();
            if (boxToGet != null)
            {
                moneySpanwer.SpawnMoneyOnce();
                paid = true;

                yield return new WaitForSeconds(1.1f);

                boxToGet = foodShelf.GetLastBox();
                boxToGet.GetComponent<FoodBoxPrefab>().MoveToParent(transform);
            }
        }
        yield return new WaitForSeconds(1f);

        anim.Play("CarAnim" + tempIndex.ToString());
        Invoke("Respawn", 2f);
    }

    private void ChangeSkin()
    {
        for (int i = 1; i < 5; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        transform.GetChild(Random.Range(1, 5)).gameObject.SetActive(true);

    }

    private void Respawn()
    {
        ChangeSkin();
        tempIndex = 0;
        MoveForward();
        StartCoroutine(LookForward());
    }

    IEnumerator LookForward()
    {
        while (true)
        {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;

            float rayLength = 5f;

            if (Physics.Raycast(ray, out hitInfo, rayLength, mask, QueryTriggerInteraction.Collide))
            {
                Debug.Log("COLLIDED WITH: " + hitInfo.collider.gameObject.name);

                Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            }
            else
            {
                Debug.Log("NO COLLISION");

                if (!anim.isPlaying)
                    MoveForward();

                Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.green);
            }


            yield return new WaitForSeconds(0.6f);
        }
    }
}

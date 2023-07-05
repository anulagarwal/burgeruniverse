using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodUp : MonoBehaviour
{
    public GameObject foodToSpawn;

    public void SpawnFood(Transform parentTransf)
    {
        GameObject obj1 = Instantiate(foodToSpawn, transform.position, Quaternion.identity);
        obj1.GetComponent<FoodBoxPrefab>().MoveToParent(parentTransf);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

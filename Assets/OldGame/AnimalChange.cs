using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class AnimalChange : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    [HideInInspector] public static Transform tempAnimal;

    void Awake()
    {
        foreach (GameObject animal in GameObject.FindGameObjectsWithTag("Animal"))
        {
            if (animal.transform.position.x > -1f)
                tempAnimal = animal.transform;
        }

        AnimalChangeButton(1);
    }

    public void AnimalChangeButton(int index)
    {
        Vector3 tempAnimalPos = tempAnimal.position;
        Destroy(tempAnimal.gameObject);
        tempAnimal = Instantiate(animalPrefabs[index], new Vector3(tempAnimalPos.x, 0f, tempAnimalPos.z), Quaternion.identity).transform;
        tempAnimal.position = new Vector3(0f, tempAnimal.position.y, tempAnimal.position.z);
        CameraManager.Instance.UpdatePlayerCamTransform(tempAnimal);

        transform.GetChild(0).transform.position = transform.GetChild(index + 1).transform.position;
    }
}

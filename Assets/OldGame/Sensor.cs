using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class Sensor : MonoBehaviour
{
    private float minSpeed = 0.25f, maxSpeed = 0.6f;

    private void OnTriggerEnter(Collider other)
    {
        string tempAnimalName;

        if (other.gameObject.layer == 20)
        {
            Debug.Log("CollidedWith:" + other.gameObject.name);
            tempAnimalName = other.gameObject.name;

            if (other.transform.position.x > -1.5f)
                Debug.Log("yz");
            else
            {
                Debug.Log("AI NAME:" + tempAnimalName);

                if (tempAnimalName.Contains("Boat") && name.Contains("Boat"))
                    other.GetComponent<MoveForward>().canRun = true;
                else if (tempAnimalName.Contains("Drone") && name.Contains("Drone"))
                    other.GetComponent<MoveForward>().canRun = true;
                else if (tempAnimalName.Contains("Car") && name.Contains("Car"))
                {
                    if (other.transform.Find("Particles") != null)
                        other.transform.Find("Particles").gameObject.SetActive(true);
                    other.GetComponent<MoveForward>().canRun = true;
                }
                else
                {
                    if (other.transform.Find("Particles") != null)
                        other.transform.Find("Particles").gameObject.SetActive(false);

                    other.GetComponent<MoveForward>().canRun = false;
                    EnemyManager.Instance.SpawnNewEnemy(name.Contains("Boat"));
                }

                return;
            }

            switch (tempAnimalName)
            {
                case "Boat(Clone)":
                    if (name.Contains("Boat"))
                        other.GetComponent<MoveForward>().canRun = true;
                    else
                        other.GetComponent<MoveForward>().canRun = false;
                    break;

                case "Drone(Clone)":
                    if (name.Contains("Drone"))
                        other.GetComponent<MoveForward>().canRun = true;
                    else
                        other.GetComponent<MoveForward>().canRun = false;
                    break;

                case "Car(Clone)":
                    if (name.Contains("Car"))
                    {
                        if (other.transform.Find("Particles") != null)
                            other.transform.Find("Particles").gameObject.SetActive(true);
                        other.GetComponent<MoveForward>().canRun = true;
                    }
                    else
                    {
                        if (other.transform.Find("Particles") != null)
                            other.transform.Find("Particles").gameObject.SetActive(false);
                        other.GetComponent<MoveForward>().canRun = false;
                    }
                    break;
            }
        }
    }
}

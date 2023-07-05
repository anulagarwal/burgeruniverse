using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus3Cards : MonoBehaviour
{
    public Transform enemyDeckTransform;

    public CardObstSpawn cardSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HAND")
        {
            cardSpawner.Add3CardsToEnemy(enemyDeckTransform);

            GameObject obj2 = Instantiate(Resources.Load("PlusCardParticle") as GameObject, transform.position - Vector3.up * 0.35f, Quaternion.identity);
            obj2.transform.Rotate(Vector3.right, -90f);

            Destroy(gameObject);
        }
    }
}

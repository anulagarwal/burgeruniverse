using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public CardObstSpawn cardSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HAND")
        {
            cardSpawner.ChangeCardsColors(CardHolder.CardHolderPlayer.GetTempCard('c'));

            GameObject obj2 = Instantiate(Resources.Load("ColorChangeParticle") as GameObject, transform.position - Vector3.up * 0.35f, Quaternion.identity);
            obj2.transform.Rotate(Vector3.right, -90f);

            Destroy(gameObject);
        }
    }
}

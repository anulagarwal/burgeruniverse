using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStop : MonoBehaviour
{
    public GameObject chooseCardUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HAND")
        {
            chooseCardUI.SetActive(true);

            FindObjectOfType<HandRun>().forwardSpeed = 0f;
            CardHolder.CardHolderPlayer.PlayCardSelectionAnim();

            GameObject obj2 = Instantiate(Resources.Load("ColorChangeParticle") as GameObject, transform.position - Vector3.up * 0.35f, Quaternion.identity);
            obj2.transform.Rotate(Vector3.right, -90f);

            Destroy(gameObject);
        }
    }
}

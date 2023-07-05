using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiMoveUp : MonoBehaviour
{
    public static EmojiMoveUp Instance;

    public float speed = 10f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    public bool isOver = false;

    public void Increase()
    {
        if (isOver)
            return;

        float clamp = transform.localPosition.x + speed * Time.deltaTime;
        clamp = Mathf.Clamp(clamp, -390f, 390f);
        transform.localPosition = new Vector3(clamp, 0f, 0f);

        if (transform.localPosition.x <= -133f)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (transform.localPosition.x <= 133f)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        if (transform.localPosition.x <= -385f)
        {
            FindObjectOfType<GirlHolderRestaurant2>().GirlStandUp();
            FindObjectOfType<WaitressHolder>().StopAllCoroutines();
            FindObjectOfType<WaitressHolder>().CancelInvoke("StartMoving");
            isOver = true;
        }
    }

    void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : MonoBehaviour
{
    public string animName;
    public float delay;

    public bool canMove = false;
    private float randomSpeed;

    void Start()
    {
        if (gameObject.name.Contains("BOOMER"))
            Invoke("PlayAnim", delay);
        else
        {
            if (Random.Range(0, 2) == 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            animName = "Walk " + Random.Range(0, 4).ToString();
            PlayAnim();

            canMove = true;
            randomSpeed = Random.Range(0.5f, 1.3f);
        }
    }

    public void StartConfusion()
    {
        canMove = false;
        GetComponent<Animator>().Play("Angry " + Random.Range(0, 2).ToString());
        Destroy(Instantiate(Resources.Load("AngryEmoji"), transform.position + Vector3.up * 2.5f, Quaternion.identity), 3f);

        Invoke("DieOfConfusion", Random.Range(1.3f, 1.7f));
    }

    public void StartGlobalWarming()
    {
        canMove = false;
        GetComponent<Animator>().Play("Hot");
        Destroy(Instantiate(Resources.Load("FireEmoji"), transform.position + Vector3.up * 2.5f, Quaternion.identity), 3f);

        Invoke("DieOfConfusion", Random.Range(1.3f, 1.7f));
    }

    public void DieOfConfusion()
    {
        GetComponent<Animator>().SetBool("angry" + Random.Range(0, 2).ToString(), true);

        gameObject.AddComponent<DestroyThisBoomer>();
        FindObjectOfType<BoomerCoinText>().AddCount();
        Destroy(this);
    }

    void PlayAnim()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play(animName);
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * randomSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            canMove = false;
            GetComponent<Animator>().SetBool("fall", true);

            gameObject.AddComponent<DestroyThisBoomer>();
            FindObjectOfType<BoomerCoinText>().AddCount();
            Destroy(this);
        }
    }
}

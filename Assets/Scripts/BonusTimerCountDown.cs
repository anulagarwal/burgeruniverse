using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusTimerCountDown : MonoBehaviour
{
    public GameObject spinReward;
    public float timeValue = 90f;
    public TextMeshProUGUI timerText;

    float timerAtStart;
    Image fillIm;
    private void Awake()
    {
        fillIm = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        if (fillIm.gameObject.name == "OUT_W")
            fillIm = fillIm.transform.GetChild(0).GetComponent<Image>();
        fillIm.fillAmount = 0f;
    }

    private void OnEnable()
    {
        if (spinReward != null)
            spinReward.SetActive(false);

        if (!destroyOnEnd)
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().isTrigger = false;
        }

        timerAtStart = timeValue;


        transform.parent.GetComponentInChildren<Rigidbody>().transform.Rotate(Vector3.up, Random.Range(0f, 360f));
    }

    bool canCountDown = true;

    public bool destroyOnEnd = false;

    void Update()
    {
        if (!canCountDown)
            return;

        if (timeValue > 0)
            timeValue -= Time.deltaTime;
        else
            timeValue = 0;

        fillIm.fillAmount = (timeValue / timerAtStart);

        DisplayTime(timeValue);

        if (PlayerPrefs.GetInt("Spinned", 0) == 0)
        {
            PlayerPrefs.SetInt("Spinned", 1);
            fillIm.fillAmount = 0f;
        }

        if (fillIm.fillAmount <= 0f)
        {
            if (destroyOnEnd)
            {
                if (GetComponentInParent<PlayerMcDonalds>() == null)
                {
                    ParticleSystem particle = transform.parent.GetComponentInChildren<ParticleSystem>();
                    particle.transform.parent = null;
                    particle.Play();
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    GetComponentInParent<PlayerMcDonalds>().DeactivateScooter();
                }

                return;
            }

            canCountDown = false;

            GetComponent<Collider>().enabled = true;
            GetComponent<Collider>().isTrigger = true;

            timerText.text = "SPIN";
            if (PlayerPrefs.GetInt("FirstSpin", 0) == 0)
            {
                if (spinReward != null)
                    spinReward.SetActive(false);
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<BoxCollider>().isTrigger = true;
            }
            else
            {
                if (spinReward != null)
                    spinReward.SetActive(true);
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
            timeToDisplay = 0;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("PlayerCasino"))
        {
            if (!canCountDown && !destroyOnEnd && (PlayerPrefs.GetInt("FirstSpin", 0) == 0))
            {
                PlayerPrefs.SetInt("FirstSpin", 1);

                //SPIN WHEEL

                VibrationManager.Instance.Vibr_LightImpact();

                transform.parent.GetComponentInChildren<Rigidbody>().AddTorque(-Vector3.forward * Random.Range(95, 145));
                Invoke("StopTheWheel", 2.55f);
            }
        }
    }

    void StopTheWheel()
    {
        Invoke("Decide", 1f);
    }

    public Transform[] images;

    public SpawnMoneyPos money;

    void Decide()
    {
        PlayerPrefs.SetInt("WheelSpinnedTimes", PlayerPrefs.GetInt("WheelSpinnedTimes", 0) + 1);

        VibrationManager.Instance.Vibr_SoftImpact();

        float highest = -2f;
        int highestIndex = 0;

        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].position.y > highest)
            {
                highest = images[i].position.y;
                highestIndex = i;
            }
        }

        images[highestIndex].GetComponentInParent<Animation>().Play();

        switch (images[highestIndex].gameObject.name)
        {
            case "Money":
                for (int i = 0; i < 10; i++)
                    money.SpawnMoneyOnce();
                break;
            case "Scooter":
                FindObjectOfType<PlayerMcDonalds>().ActivateScooter();
                break;
            case "Waiter":
                transform.Find("Bonuses").GetChild(0).gameObject.SetActive(true);
                break;
        }

        GetComponent<Collider>().enabled = false;
        GetComponent<Collider>().isTrigger = false;
        fillIm.fillAmount = 0f;
        timeValue = timerAtStart;
        canCountDown = true;
    }

    public void AddRewardSpin()
    {
        if (spinReward != null)
            spinReward.SetActive(false);

        VibrationManager.Instance.Vibr_LightImpact();

        transform.parent.GetComponentInChildren<Rigidbody>().AddTorque(-Vector3.forward * Random.Range(95, 145));
        Invoke("StopTheWheel", 2.55f);
    }
}

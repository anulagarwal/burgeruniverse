using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public static Image barImage;

    public Animator playerAnim;

    void Awake()
    {
        barImage = GetComponent<Image>();
        barImage.fillAmount = 0f;
    }

    public GameObject clearedPanel, endPanel, endCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Hitman_2")
            {
                playerAnim.Play("Dance");
                playerAnim.gameObject.GetComponentInChildren<Rig>().weight = 0f;
            }

            clearedPanel.SetActive(true);
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<HitmanTaskManager>().gameObject.SetActive(false);
            endCam.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            endPanel.SetActive(true);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBarUfo : MonoBehaviour
{
    private Image progressBar;

    public Gradient color;

    public Color[] colors;

    void Start()
    {
        progressBar = GetComponent<Image>();
        progressBar.fillAmount = 0f;
        StartCoroutine(Check());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddValue(int value)
    {
        progressBar.fillAmount += (1 / 18f) * value;
    }

    IEnumerator Check()
    {
        while (true)
        {
            progressBar.fillAmount = (1 / 18f) * GameObject.FindGameObjectsWithTag("Player").Length;

            if (progressBar.fillAmount <= 0.25f)
                progressBar.color = colors[0];
            else if (progressBar.fillAmount > 0.25f && progressBar.fillAmount <= 0.5f)
                progressBar.color = colors[1];
            else if (progressBar.fillAmount > 0.5f && progressBar.fillAmount <= 0.75f)
                progressBar.color = colors[2];
            else
                progressBar.color = colors[3];

            yield return new WaitForSeconds(0.1f);
        }
    }

}

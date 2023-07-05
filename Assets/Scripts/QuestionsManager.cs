using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionsManager : MonoBehaviour
{
    void Start()
    {
        tempEnemiesText = answers[0].transform.GetChild(1).gameObject;
    }

    private int tempIndex = 0;

    public GameObject[] questions, answers;

    [HideInInspector] public GameObject tempEnemiesText;

    public void SubmitAnswer(string str)
    {
        foreach (CubesHolder holder in FindObjectsOfType<CubesHolder>())
        {
            if (holder.gameObject.name.Contains("Player"))
            {
                holder.AddCubes(str);
            }
            else if (holder.gameObject.name.Contains("Enemy2"))
            {
                holder.AddCubes(answers[tempIndex].transform.GetChild(1).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().transform.parent.parent.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text);
            }
            else
            {
                holder.AddCubes(answers[tempIndex].transform.GetChild(1).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().transform.parent.parent.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text);
            }
        }
        Invoke("NextQuestionAndAnswers", 4f);
    }

    private void NextQuestionAndAnswers()
    {
        questions[tempIndex].SetActive(false);
        questions[tempIndex + 1].SetActive(true);

        answers[tempIndex].SetActive(false);
        answers[tempIndex + 1].SetActive(true);

        tempIndex++;

        tempEnemiesText = answers[tempIndex].transform.GetChild(1).gameObject;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }
}

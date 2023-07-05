using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesHolder : MonoBehaviour
{
    void Start()
    {
        tempPosY = transform.position.y;

        AddCubes("FASZ");
    }

    bool isFirstClick = true;
    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * 3.4f * Time.deltaTime);
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (isFirstClick)
                {
                    isFirstClick = false;
                    canMove = true;
                }
                else
                    Move(true);
            }
        }
    }

    public void Stop()
    {
        canMove = false;

        if (gameObject.name.Contains("Player"))
        {
            textbgimagekeyboardphone.SetActive(true);
            questionsAnim.Play("QuestionIn");
        }
    }

    public CubesHolder enemy1, enemy2;

    public GameObject[] grounds2, grounds3;

    public void Move(bool justThisCanMove = false)
    {
        canMove = true;

        if (!justThisCanMove)
        {
            if (!grounds2[0].activeInHierarchy && (enemy1 != null))
            {
                for (int i = 0; i < grounds2.Length; i++)
                {
                    grounds2[i].SetActive(true);
                }
            }
            else if (grounds2[0].activeInHierarchy && (enemy1 != null))
            {
                for (int i = 0; i < grounds3.Length; i++)
                {
                    grounds3[i].SetActive(true);
                }
            }
        }

        textbgimagekeyboardphone.SetActive(false);

        if (!justThisCanMove)
        {
            //if (enemy1 != null)
            enemy1.Move(true);
            //if (enemy2 != null)
            enemy2.Move(true);
        }

        //questionsAnim.Play("QuestionOut");
    }

    public GameObject textbgimagekeyboardphone, enemiesText;
    public Animation questionsAnim;

    public bool canMove = false;

    public TypeRunCharacter character;
    public Material[] materials;
    public GameObject wordCubePref;

    int wordLength = 0;
    string tempWord;
    [HideInInspector] public float tempPosY, height = 0.3f;

    public int tempWordLength;

    public void AddCubes(string word)
    {
        tempWord = word;
        tempWordLength = wordLength = word.Length;

        if (word == "FASZ")
            StartCoroutine(SpawnCubes(false));
        else
            StartCoroutine(SpawnCubes());
    }

    private int matIndex = 1;
    IEnumerator SpawnCubes(bool canOverwriteWords = true)
    {
        if (canOverwriteWords)
        {
            FindObjectOfType<QuestionsManager>().tempEnemiesText.SetActive(true);
            textbgimagekeyboardphone.SetActive(false);
        }

        if (matIndex == 1)
            matIndex = 0;
        else
            matIndex = 1;

        if (canOverwriteWords)
        {
            yield return new WaitForSeconds(2f);
            DisableUI();
        }

        character.Jump(character.transform.position.y + height * wordLength, wordLength);

        while (wordLength > 0)
        {
            GameObject wordCube = Instantiate(wordCubePref, transform.position, Quaternion.identity);
            if (canOverwriteWords)
                wordCube.GetComponent<WordCube>().ChangeTexts(tempWord[wordLength - 1].ToString());

            wordCube.GetComponent<MeshRenderer>().material = materials[matIndex];
            wordCube.transform.parent = transform;
            wordCube.transform.position = new Vector3(wordCube.transform.position.x, tempPosY, wordCube.transform.position.z);
            tempPosY += height;

            wordLength--;
            yield return new WaitForSeconds(0.3f);
        }


        bool hasTheMostAmount = true;

        for (int i = 0; i < FindObjectsOfType<CubesHolder>().Length; i++)
        {
            if (FindObjectsOfType<CubesHolder>()[i].gameObject.GetInstanceID() == gameObject.GetInstanceID())
                continue;

            if (FindObjectsOfType<CubesHolder>()[i].tempWordLength > tempWordLength)
            {
                hasTheMostAmount = false;
            }

        }

        if (hasTheMostAmount && canOverwriteWords)
        {
            yield return new WaitForSeconds(0.6f);
            Move();
        }
    }


    private void DisableUI()
    {
        questionsAnim.Play("QuestionOut");
    }
}

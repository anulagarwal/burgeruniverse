using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar2 : MonoBehaviour
{
    public GameObject theme;

    public GameObject[] foodPrefabs, enemyFoodPrefabs;

    void Start()
    {
        theme.SetActive(true);

        for (int i = 0; i < buttonImages.Length; i++)
        {
            foodImages[i].sprite = breakfastSprites[i + tempRound * 3];
            buttonImages[i].color = Color.white;
        }
    }

    public static ProgressBar2 Instance;

    private void Awake()
    {
        Instance = this;
    }

    private int tempRound = 0;

    public Image[] buttonImages, foodImages;
    public Sprite[] breakfastSprites;
    public int[] scores;

    public Color highLightedColor;

    private int tempIndex;

    public void ButtonPressed(int index)
    {
        tempIndex = index;

        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (index == i)
                buttonImages[i].color = highLightedColor;
            else
                buttonImages[i].color = Color.white;
        }
    }

    int randomEnemyGuessIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            randomEnemyGuessIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            randomEnemyGuessIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            randomEnemyGuessIndex = 2;
        }

    }

    public void TimeDown()
    {
        int enemyTempScore = (scores[(tempRound * 3) + randomEnemyGuessIndex]);
        if ((scores[(tempRound * 3) + randomEnemyGuessIndex]) == 3)
            ProgressBarSide.EnemyInstance.IncreaseBars(0.2f);
        else
            ProgressBarSide.EnemyInstance.IncreaseBars();


        //TEMPSCORE
        int tempScore = (scores[(tempRound * 3) + tempIndex]);

        GameObject tempPlayerFood = foodPrefabs[(tempRound * 3) + tempIndex];
        GameObject tempEnemyFood = enemyFoodPrefabs[(tempRound * 3) + randomEnemyGuessIndex];

        #region Positioning Foods
        if (tempRound == 0)
        {
            tempPlayerFood.transform.localPosition = new Vector3(-0.046f, 0.3f, -0.063f);
            tempEnemyFood.transform.localPosition = new Vector3(-0.046f, 0.3f, -0.063f);
        }
        else if (tempRound == 1)
        {
            tempPlayerFood.transform.localPosition = new Vector3(0.07f, 0.3f, 0.018f);
            tempEnemyFood.transform.localPosition = new Vector3(0.07f, 0.3f, 0.018f);
        }
        else if (tempRound == 2)
        {
            tempPlayerFood.transform.localPosition = new Vector3(-0.053f, 0.3f, 0.069f);
            tempEnemyFood.transform.localPosition = new Vector3(-0.053f, 0.3f, 0.069f);
        }
        #endregion

        tempPlayerFood.SetActive(true);
        tempEnemyFood.SetActive(true);

        //EMOJIS
        if (tempScore == 3)
            CoinsHolder.HappyEmojiInstance.PlayThisAt(buttonImages[tempIndex].transform);
        else
            CoinsHolder.SadEmojiInstance.PlayThisAt(buttonImages[tempIndex].transform);

        //SCORE
        if (tempScore == 3)
            ProgressBarSide.PlayerInstance.IncreaseBars(0.2f);
        else
            ProgressBarSide.PlayerInstance.IncreaseBars();


        Invoke("PlayAgain", 0.5f);
    }

    private void PlayAgain()
    {
        tempRound++;

        if (tempRound > 3)
        {
            transform.parent.parent.gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < buttonImages.Length; i++)
        {
            foodImages[i].sprite = breakfastSprites[i + tempRound * 3];
            buttonImages[i].color = Color.white;
        }

        GetComponent<Animation>().Play();
    }
}

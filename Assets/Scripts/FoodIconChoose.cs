using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIconChoose : MonoBehaviour
{
    public Color goodColor, badColor;

    public SpriteRenderer im1, im2;
    public Sprite[] sprites;

    public int tempIndex;

    private FoodHolderPlayers playerFoods;
    private List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    void Start()
    {
        playerFoods = PlayerMcDonalds.Instance.GetComponentInChildren<FoodHolderPlayers>();
        goodColor = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color;

        foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>())
        {
            if (rend.color == goodColor)
                renderers.Add(rend);
        }
    }

    public void SetColors()
    {
        if (playerFoods.DoesHave(tempIndex))
        {
            foreach (SpriteRenderer im in renderers)
                im.color = goodColor;
        }
        else
        {
            foreach (SpriteRenderer im in renderers)
                im.color = badColor;
        }

        Invoke("SetColors", 0.15f);
    }

    public void UpdateImages(int index)
    {
        im1.sprite = im2.sprite = sprites[index];

        if (index == 0)
        {
            im1.transform.localScale = new Vector3(0.2761647f, 0.2761647f, 0.2761647f);
            im1.transform.localPosition = new Vector3(0f, 0.02f, -0.06f);
        }
        else if (index == 1)
        {
            im1.transform.localScale = new Vector3(0.2914176f, 0.2914176f, 0.2914176f);
            im1.transform.localPosition = new Vector3(0f, 0f, -0.06f);
        }
        else if (index == 2)
        {
            im1.transform.localScale = new Vector3(0.334045f, 0.334045f, 0.334045f);
            im1.transform.localPosition = new Vector3(0.13f, 0.04f, -0.06f);
        }
        else if (index == 3)
        {
            im1.transform.localScale = new Vector3(0.2928727f, 0.2928727f, 0.2928727f);
            im1.transform.localPosition = new Vector3(0.06f, 0.01f, -0.06f);
        }
        SetColors();
    }

    public void ChooseRandomImage()
    {
        if (transform.parent.GetComponentInChildren<Guest>() == null)
        {
            GetComponent<Animation>().Stop();
            return;
        }

        int randomIndex;

        randomIndex = 0;

        //if (PlayerPrefs.GetInt("Poop", 0) == 1)
        //{
        //    PlayerPrefs.SetInt("Fries", 1);
        //    PlayerPrefs.SetInt("Pizza", 1);
        //}

        if (PlayerPrefs.GetInt("Fries", 0) == 1)
            randomIndex = Random.Range(0, 2);

        if (PlayerPrefs.GetInt("Pizza", 0) == 1)
            randomIndex = Random.Range(0, 3);

        if (PlayerPrefs.GetInt("IceCream", 0) == 1)
            randomIndex = Random.Range(0, 4);

        if (!isVip)
        {
            tempIndex = randomIndex;
            transform.parent.GetComponentInChildren<Guest>().foodIndex = tempIndex;


            switch (randomIndex)
            {
                case 0:
                    InfoCountText.burger.UpdateText(1);
                    break;
                case 1:
                    InfoCountText.fries.UpdateText(1);
                    break;
                case 2:
                    InfoCountText.pizza.UpdateText(1);
                    break;
                case 3:
                    InfoCountText.iceCream.UpdateText(1);
                    break;
            }
        }

        UpdateImages(tempIndex);
    }

    public void PlayDownAnim()
    {
        if (!isVip)
        {
            switch (tempIndex)
            {
                case 0:
                    InfoCountText.burger.UpdateText(-1);
                    break;
                case 1:
                    InfoCountText.fries.UpdateText(-1);
                    break;
                case 2:
                    InfoCountText.pizza.UpdateText(-1);
                    break;
                case 3:
                    InfoCountText.iceCream.UpdateText(-1);
                    break;
            }
        }

        if (GetComponent<Animation>().GetClip("RotatingFoodIconDown") == null)
            GetComponent<Animation>().Play("RotatingFoodIconDown2");
        else
            GetComponent<Animation>().Play("RotatingFoodIconDown");
    }

    public bool isVip = false;
}

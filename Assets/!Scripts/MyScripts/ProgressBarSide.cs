using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBarSide : MonoBehaviour
{
    public static ProgressBarSide PlayerInstance, EnemyInstance;

    public Image[] bars;

    private Transform emojiTarget;

    private TextMeshProUGUI emojiText, tempScoreText;

    private void Awake()
    {
        if (gameObject.name.Contains("Player"))
            PlayerInstance = this;
        else
            EnemyInstance = this;
    }

    void Start()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].fillAmount = 0f;
        }

        emojiTarget = transform.GetChild(transform.childCount - 1);
        emojiTarget.transform.localPosition = Vector3.right * 99f * 5f;

        emojiText = emojiTarget.GetComponentInChildren<TextMeshProUGUI>();
        tempScoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private int tempBar = 0;
    [HideInInspector] public float tempScore = 0f;

    public void IncreaseBars(float increaseBy = 0.1f)
    {
        tempScore += increaseBy;
        float tempBarLimit = 0.2f * (tempBar + 1);

        if ((bars[tempBar].fillAmount + increaseBy) <= tempBarLimit)
        {
            bars[tempBar].fillAmount += increaseBy;
            if (bars[tempBar].fillAmount == 0.2f)
                tempBar++;
        }
        else
        {
            bars[tempBar].fillAmount += 0.1f;

            for (int i = 1; i < bars.Length; i++)
            {
                if (bars[i].fillAmount < bars[i - 1].fillAmount)
                    bars[i].fillAmount = bars[i - 1].fillAmount;
            }

            tempBar++;
            bars[tempBar].fillAmount += 0.1f;
        }

        for (int i = 1; i < bars.Length; i++)
        {
            if (bars[i].fillAmount < bars[i - 1].fillAmount)
                bars[i].fillAmount = bars[i - 1].fillAmount;
        }

        if (increaseBy == 0.1f)
            emojiTargetTargetPos = new Vector3(emojiTarget.transform.localPosition.x - 99f, 0f, 0f);
        else if (increaseBy == 0.2f)
            emojiTargetTargetPos = new Vector3(emojiTarget.transform.localPosition.x - 99f * 2f, 0f, 0f);

        StartCoroutine(MoveEmojiTargetToTarget());
    }

    private Vector3 emojiTargetTargetPos;

    IEnumerator MoveEmojiTargetToTarget()
    {
        yield return new WaitForSeconds(1f);

        emojiText.text = "X" + (tempBar + 1).ToString();
        tempScoreText.text = (tempScore * 100f).ToString("0");

        while (Vector3.Distance(emojiTargetTargetPos, emojiTarget.transform.localPosition) > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            emojiTarget.transform.localPosition = Vector3.MoveTowards(emojiTarget.transform.localPosition, emojiTargetTargetPos, 200 * Time.deltaTime);
        }
    }
}

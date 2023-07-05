using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBar : MonoBehaviour
{
    public Gradient grad;

    private TextMeshProUGUI textmeshpro;
    private Image barImage;
    private string[] strings = { "MISERABLE", "SAD", "DECENT", "FINE", "HAPPY" };

    void Start()
    {
        textmeshpro = transform.GetComponentInChildren<TextMeshProUGUI>();
        barImage = transform.Find("BarIn").GetComponent<Image>();

        textmeshpro.color = barImage.color = grad.Evaluate(barImage.fillAmount);

        #region TextCheck
        if (barImage.fillAmount < 0.2f)
            textmeshpro.text = strings[0];
        else if (barImage.fillAmount < 0.4f)
            textmeshpro.text = strings[1];
        else if (barImage.fillAmount < 0.6f)
            textmeshpro.text = strings[2];
        else if (barImage.fillAmount < 0.8f)
            textmeshpro.text = strings[3];
        else
            textmeshpro.text = strings[4];
        #endregion
    }

    public void IncreaseBar(float incBy)
    {
        barImage.fillAmount += incBy;
        textmeshpro.color = barImage.color = grad.Evaluate(barImage.fillAmount);

        #region TextCheck
        if (barImage.fillAmount < 0.2f)
            textmeshpro.text = strings[0];
        else if (barImage.fillAmount < 0.4f)
            textmeshpro.text = strings[1];
        else if (barImage.fillAmount < 0.6f)
            textmeshpro.text = strings[2];
        else if (barImage.fillAmount < 0.8f)
            textmeshpro.text = strings[3];
        else
            textmeshpro.text = strings[4];
        #endregion
    }
}

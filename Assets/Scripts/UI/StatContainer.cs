using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatContainer : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Image statImage;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI statValueText;
    
    public void Configure(Sprite sprite, string statName, float statValue , bool useColor = false)
    {
        statImage.sprite = sprite;
        statText.text = statName;
        
        float absStatValue = Mathf.Abs(statValue);

        if (useColor)
        {
            ColorizeStatValueText(absStatValue);
        }
        else
        {
            statValueText.color = Color.white;
            statValueText.text = statValue.ToString("F2");
        }

    }

    private void ColorizeStatValueText(float statValue)
    {
        float sign = Mathf.Sign(statValue);

        if (statValue == 0) sign = 0;

        Color statValueTextColor = Color.white;
        
        if(sign > 0) statValueTextColor = Color.green;
        else if (sign < 0) statValueTextColor = Color.red;

        statValueText.color = statValueTextColor;
        statValueText.text = statValue.ToString("F2"); // Format to one decimal place
    }

    public float GetFontSize()
    {
        return statText.fontSize;
    }

    public void SetFontSize(float minFontSize)
    {
        statText.fontSizeMax = minFontSize;
        statValueText.fontSizeMax = minFontSize;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatContainer : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Image statImage;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI statValueText;
    
    public void Configure(Sprite sprite, string statName, float statValue)
    {
        statImage.sprite = sprite;
        statText.text = statName;
        statValueText.text = statValue.ToString("F1"); // Format to one decimal place
    }
}

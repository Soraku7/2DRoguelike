using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    
    [field: SerializeField] public Button Button { get ; private set;}

    public void Configure(Sprite sprite , string name , int level)
    {
        icon.sprite = sprite;
        nameText.text = name;

        Color imageColor;
        imageColor = ColorHolder.GetColor(level);

        foreach (var image in levelDependentImages)
        {
            image.color = imageColor;
        }
    }

    public void Deselect()
    {
        Button.transform.DOScale(1 , 0.3f)
            .SetEase(Ease.OutBack); // 设置缓动效果
    }

    public void Select()
    {
        Button.transform.DOScale(1.2f , 0.3f)
        .SetEase(Ease.OutBack); // 设置缓动效果
    }
}

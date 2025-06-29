using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [Header("Stats")]
    [SerializeField] private Transform statsContainerParent;
    [SerializeField] private StatContainer statContainerPrefab;
     
    [field: SerializeField] public Button Button { get ; private set;}
    
    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outline;


    public void Configure(Sprite sprite , string name , int level , WeaponDataSO weaponData)
    {
        icon.sprite = sprite;
        nameText.text = name + " (Level " + (level + 1) + ")";

        Color imageColor;
        imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;
        
        outline.color = ColorHolder.GetOutlineColor(level);

        foreach (var image in levelDependentImages)
        {
            image.color = imageColor;
        }
        
        Dictionary<Stat, float> calculateStat = WeaponStatsCalculate.GetStats(weaponData, level);
        ConfigureStatContainers(calculateStat);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float> calculateStat)
    {
        StatContainerManager.GenerateStatContainer(calculateStat , statsContainerParent);
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

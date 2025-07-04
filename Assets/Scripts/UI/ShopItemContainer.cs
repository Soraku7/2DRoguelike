using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopItemContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    
    [Header("Stats")] 
    [SerializeField] private Transform statsContainerParent;
    [SerializeField] private StatContainer statContainerPrefab;
     
    [field: SerializeField] public Button PurchaseButton { get ; private set;}
    
    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outline;

    [Header("Lock Elements")] 
    [SerializeField] private Image lockImage;
    [SerializeField] private Sprite lockedSprite , unlockedSprite;
    public bool IsLocked { get; private set; } = false;
    
    public void Configure(ObjectDataSO objectData)
    {
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name;
        priceText.text = objectData.Price.ToString();

        Color imageColor;
        imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;
        
        outline.color = ColorHolder.GetOutlineColor(objectData.Rarity);

        foreach (var image in levelDependentImages)
        {
            image.color = imageColor;
        }
        
        ConfigureStatContainers(objectData.BaseStats);
    }
    
    public void Configure(WeaponDataSO weaponData , int level)
    { 
        
        icon.sprite = weaponData.Sprite;
        nameText.text = weaponData.Name + " (Level " + (level + 1) + ")";
        priceText.text = WeaponStatsCalculate.GetPruchasePrice(weaponData, level).ToString();

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

    private void ConfigureStatContainers(Dictionary<Stat, float> stats)
    {
        StatContainerManager.GenerateStatContainer(stats , statsContainerParent);
    }

    public void LockButtonCallback()
    {
        IsLocked = !IsLocked;
        UpdateLockVisuals();
    }

    private void UpdateLockVisuals()
    {
        lockImage.sprite = IsLocked ? lockedSprite : unlockedSprite;
    }
}
using System;
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

    [SerializeField] public Button purchaseButton;
    
    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outline;

    [Header("Lock Elements")] 
    [SerializeField] private Image lockImage;
    [SerializeField] private Sprite lockedSprite , unlockedSprite;
    public bool IsLocked { get; private set; } = false;

    [Header("Purchasing")] 
    public WeaponDataSO WeaponData { get; private set; }
    public ObjectDataSO ObjectData { get; private set; }
    private int weaponLevel;
    
    [Header("Actions")]
    public static Action<ShopItemContainer , int> onPurchased;

    private void Awake()
    {
        CurrencyManager.onUpdate += CurrencyUpdateCallback;
    }


    private void OnDestroy()
    {
        CurrencyManager.onUpdate -= CurrencyUpdateCallback;
    }

    public void Configure(ObjectDataSO objectData)
    {
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name;
        priceText.text = objectData.Price.ToString();
        
        ObjectData = objectData;

        Color imageColor;
        imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;
        
        outline.color = ColorHolder.GetOutlineColor(objectData.Rarity);

        foreach (var image in levelDependentImages)
        {
            image.color = imageColor;
        }
        
        ConfigureStatContainers(objectData.BaseStats);

        purchaseButton.onClick.AddListener(Purchase);
        purchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(objectData.Price);
    }
    
    public void Configure(WeaponDataSO weaponData , int level)
    { 
        weaponLevel = level;
        
        icon.sprite = weaponData.Sprite;
        nameText.text = weaponData.Name + " (Level " + (level + 1) + ")";
        priceText.text = WeaponStatsCalculate.GetPruchasePrice(weaponData, level).ToString();

        WeaponData = weaponData;

        int weaponPrice = WeaponStatsCalculate.GetPruchasePrice(weaponData, level);

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
        
        purchaseButton.onClick.AddListener(Purchase);
        purchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(weaponPrice);
    }

    private void Purchase()
    {
        onPurchased?.Invoke(this , weaponLevel); 
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
    
    
    private void CurrencyUpdateCallback()
    {
        int itemPrice;

        if (WeaponData != null) itemPrice = WeaponStatsCalculate.GetPruchasePrice(WeaponData, weaponLevel);
        else itemPrice = ObjectData.Price;

        purchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(itemPrice);
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfo : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI recyclePriceText;
    
    [Header("Color")]
    [SerializeField] private Color container;
    
    [Header("Stats")]
    [SerializeField] private Transform statsParent;

    public void Configure(Weapon weapon)
    {
        Configure(weapon.WeaponData.Sprite, weapon.WeaponData.Name + " " + weapon.Level,
            ColorHolder.GetColor(weapon.Level), WeaponStatsCalculate.GetRecyclePrice(weapon.WeaponData, weapon.Level),
            WeaponStatsCalculate.GetStats(weapon.WeaponData , weapon.Level));
    }

    public void Configure(ObjectDataSO objectData)
    {
        Configure(objectData.Icon, objectData.Name, 
            ColorHolder.GetColor(objectData.Rarity), objectData.RecyclePrice,
            objectData.BaseStats);
    }
    
    private void Configure(Sprite itemIcon, string name, Color containerColor, int recyclePrice,
        Dictionary<Stat, float> stats)
    {
        icon.sprite = itemIcon;
        itemNameText.text = name;
        itemNameText.color = containerColor;
        
        recyclePriceText.text = recyclePrice.ToString();

        container = containerColor;

        StatContainerManager.GenerateStatContainer(stats , statsParent);
    }
}

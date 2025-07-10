using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour , IGameStateListener
{
    [Header("Player Components")]
    [SerializeField] private PlayerWeapons playerWeapons;
    [SerializeField] private PlayerObjects playerObjects; 
    
    [Header("Elements")] 
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private InventoryItemContainer inventoryItemContainer;
    [SerializeField] private ShopManagerUI shopManagerUI;
    [SerializeField] private InventoryItemInfo itemInfo;

    private void Awake()
    {
        ShopManager.onItemPurchased += ItemPurchasedCallback;
        WeaponMerge.onMerge += WeaponMergedCallback;
    }

    private void OnDestroy()
    {
        ShopManager.onItemPurchased -= ItemPurchasedCallback;
        WeaponMerge.onMerge -= WeaponMergedCallback;
    }


    public void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP) Configure();
    }

    private void Configure()
    {
        inventoryItemsParent.Clear();

        Weapon[] weapons = playerWeapons.GetWeapons();
        
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i] == null) continue;
            
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);
            
            container.Configure(weapons[i] , i , () => ShowItemInfo(container));
        }

        ObjectDataSO[] objectDatas = playerObjects.Objects.ToArray();

        for (int i = 0; i < objectDatas.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);
            
            container.Configure(objectDatas[i] , () => ShowItemInfo(container));
        }
    }

    private void ShowItemInfo(InventoryItemContainer container)
    {
        if (container.Weapon != null) ShowWeaponInfo(container.Weapon , container.Index);
        else ShowObjectInfo(container.ObjectData);
    }

    private void ShowObjectInfo(ObjectDataSO containerObjectData)
    {
        itemInfo.Configure(containerObjectData);
        
                
        itemInfo.RecycleButton.onClick.RemoveAllListeners();
        itemInfo.RecycleButton.onClick.AddListener(() => RecycleObject(containerObjectData));
        
        shopManagerUI.ShowItemInfo();
    }

    private void RecycleObject(ObjectDataSO objectData)
    {
        playerObjects.RecycleObject(objectData);
        
        Configure();
        
        shopManagerUI.HideItemInfo();
    }

    private void ShowWeaponInfo(Weapon containerWeapon , int index)
    {
        itemInfo.Configure(containerWeapon);
        
        
        itemInfo.RecycleButton.onClick.RemoveAllListeners();
        itemInfo.RecycleButton.onClick.AddListener(() => RecycleWeapon(index));
        
        shopManagerUI.ShowItemInfo();
    }

    private void RecycleWeapon(int index)
    {
        Debug.Log("Recycling weapon at index: " + index);
        
        playerWeapons.RecycleWeapon(index);
        Configure();
        shopManagerUI.HideItemInfo();
    }


    private void ItemPurchasedCallback()
    {
        Configure();
    }
    
    
    private void WeaponMergedCallback(Weapon mergeWeapon)
    {
        Configure();
        itemInfo.Configure(mergeWeapon);
    }
}

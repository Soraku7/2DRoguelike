using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;
    
    [Header("Player Components")]
    [SerializeField] private PlayerWeapons playerWeapons;
    [SerializeField] private PlayerObjects playerObjects;
    
    [Header("Reroll")]
    [SerializeField] private Button rerollButton;
    [SerializeField] private int rollPrice;
    [SerializeField] private TextMeshProUGUI rerollPriceText;

    private void Awake()
    {
        ShopItemContainer.onPurchased += ItemPurchasedCallback;
        CurrencyManager.onUpdate += CurrencyUpdateCallback;
    }


    private void OnDestroy()
    {
        ShopItemContainer.onPurchased -= ItemPurchasedCallback;
        CurrencyManager.onUpdate -= CurrencyUpdateCallback;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
        {
            Configure();
            UpdateRerollVisual();
        }
    }

    private void Configure()
    {
        List<GameObject> toDestroy = new List<GameObject>();
        
        for(int i = 0 ; i < containersParent.childCount ; i++)
        {
            ShopItemContainer shopItemContainer = containersParent.GetChild(i).GetComponent<ShopItemContainer>();
            
            if(!shopItemContainer.IsLocked) toDestroy.Add(shopItemContainer.gameObject);
        }
        
        while(toDestroy.Count > 0)
        {
            Transform t = toDestroy[0].transform;
            t.SetParent(null);
            Destroy(t.gameObject);
            toDestroy.RemoveAt(0);
        }

        int containersToAdd = 6 - containersParent.childCount; 
        int weaponContainerCount = Random.Range(Mathf.Min(2 , containersToAdd) , containersToAdd);
        int objectContainerCount = containersToAdd - weaponContainerCount;

        for (int i = 0; i < weaponContainerCount; i++)
        {
            ShopItemContainer weaponContainerInstance = Instantiate(shopItemContainerPrefab, containersParent);
            WeaponDataSO randomWeapon = ResourcesManager.GetRandomWeapon();
            weaponContainerInstance.Configure(randomWeapon, Random.Range(0, 2));

        }
        
        for (int i = 0; i < objectContainerCount; i++)
        {
            ShopItemContainer objectContainerInstance = Instantiate(shopItemContainerPrefab, containersParent);
            
            ObjectDataSO randomObject = ResourcesManager.GetRandomObject();
            objectContainerInstance.Configure(randomObject);
        }
    }

    public void Reroll()
    {
        Configure();
        CurrencyManager.instance.UseCoins(rollPrice);
    }

    private void UpdateRerollVisual()
    {
        rerollPriceText.text = rollPrice.ToString();
        rerollButton.interactable = CurrencyManager.instance.HasEnoughCurrency(rollPrice);
    }

    private void CurrencyUpdateCallback()
    {
        UpdateRerollVisual();
    }
    
    
    private void ItemPurchasedCallback(ShopItemContainer container, int weaponLevel)
    {
        if (container.WeaponData != null) TryPurchaseWeapon(container.WeaponData, weaponLevel);
    }

    private void TryPurchaseWeapon(WeaponDataSO container, int weaponLevel)
    {
        if (playerWeapons.TryAddWeapon(container, weaponLevel))
        {
            
        }
    }
}

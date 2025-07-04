using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;
    public void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
        {
            Configure();
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
    }
}

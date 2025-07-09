using UnityEngine;

public class InventoryManager : MonoBehaviour , IGameStateListener
{
    [Header("Player Components")]
    [SerializeField] private PlayerWeapons playerWeapons;
    [SerializeField] private PlayerObjects playerObjects; 
    
    [Header("Elements")] 
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private InventoryItemContainer inventoryItemContainer;
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
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);

            Color containerColor = ColorHolder.GetColor(weapons[i].Level);
            Sprite icon = weapons[i].WeaponData.Sprite;
            
            container.Configure(containerColor, icon);
        }

        ObjectDataSO[] objectDatas = playerObjects.Objects.ToArray();

        for (int i = 0; i < objectDatas.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);

            Color containerColor = ColorHolder.GetColor(objectDatas[i].Rarity);
            Sprite icon = objectDatas[i].Icon;
            
            container.Configure(containerColor, icon);
        }
    }
}

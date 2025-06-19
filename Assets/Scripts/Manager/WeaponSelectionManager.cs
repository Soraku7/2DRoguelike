using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")] 
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab; 
    
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WEAPONSELECTION:
                Configure();
                break;
        }
    }

    private void Configure()
    {
        containersParent.Clear();

        for (int i = 0; i < 3; i++)
        {
            GenerateWeaponContainers();
        }
    }
    
    private void GenerateWeaponContainers()
    {
        WeaponSelectionContainer containerInstance = Instantiate(weaponContainerPrefab , containersParent);
    }
}

using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")] 
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab;
    [SerializeField] private PlayerWeapons playerWeapons;
    
    [Header("Data")]
    [SerializeField] private WeaponDataSO[] starterWeapons;
    private WeaponDataSO selectedWeapon; 
    private int initialWeaponLevel;
    
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                if (selectedWeapon == null)
                {
                    Debug.LogError("No weapon selected. Please select a weapon before starting the game.");
                    return;
                }
                
                playerWeapons.AddWeapon(selectedWeapon , initialWeaponLevel);
                selectedWeapon = null;
                break;
            
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
        
        WeaponDataSO weaponData = starterWeapons[Random.Range(0 , starterWeapons.Length)];
        
        int level = Random.Range(0 , 4);

        
        containerInstance.Configure(weaponData.Sprite, weaponData.Name , level);
        
        containerInstance.Button.onClick.RemoveAllListeners();
        containerInstance.Button.onClick.AddListener(() => WeaponSelectedCallback(containerInstance , weaponData , level));
    }
    
    private void WeaponSelectedCallback(WeaponSelectionContainer containerInstance , WeaponDataSO weaponData , int level)
    {
        selectedWeapon = weaponData;
        initialWeaponLevel = level;
        
        foreach(WeaponSelectionContainer container in containersParent.GetComponentsInChildren<WeaponSelectionContainer>())
        {
           if(container == containerInstance) container.Select();
           else container.Deselect();
        }
    }
}

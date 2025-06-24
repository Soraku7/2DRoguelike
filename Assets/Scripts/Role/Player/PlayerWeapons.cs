using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WeaponPosition[] weaponPositions;
    
    public void AddWeapon(WeaponDataSO selectedWeapon , int weaponLevel)
    {
        // Logic to add the weapon to the player's inventory
        Debug.Log($"Weapon {selectedWeapon.Name} added to player with level." + weaponLevel);
        
        //Instantiate(selectedWeapon.Prefab , weaponsParent);
        weaponPositions[Random.Range(0 , weaponPositions.Length)].AssignWeapon(selectedWeapon.Prefab , weaponLevel);
    }
}

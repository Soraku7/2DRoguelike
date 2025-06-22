using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public void AddWeapon(WeaponDataSO weaponData , int weaponLevel)
    {
        // Logic to add the weapon to the player's inventory
        Debug.Log($"Weapon {weaponData.Name} added to player with level." + weaponLevel.ToString());
    }
}

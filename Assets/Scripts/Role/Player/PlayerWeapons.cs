using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WeaponPosition[] weaponPositions;

    public bool TryAddWeapon(WeaponDataSO weapon, int weaponLevel)
    {
        for(int i = 0 ; i < weaponPositions.Length ; i++)
        {
            if (weaponPositions[i].Weapon != null) continue;
            
            weaponPositions[i].AssignWeapon(weapon.Prefab , weaponLevel);
            return true;
        }
        
        return false;
    }

    public Weapon[] GetWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        foreach (var weaponPosition in weaponPositions)
        {
            if (weaponPosition.Weapon == null) continue;
            
            weapons.Add(weaponPosition.Weapon);
        }
        
        return weapons.ToArray();
    }
}

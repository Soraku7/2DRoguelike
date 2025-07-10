using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMerge : MonoBehaviour
{
    public static WeaponMerge instance;

    [Header("Elements")]
    [SerializeField] private PlayerWeapons playerWeapons;
    
    [Header("Settings")]
    private List<Weapon> weaponToMerge = new List<Weapon>();
    
    [Header("Actions")]
    public static Action<Weapon> onMerge;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanMerge(Weapon weapon)
    {
        if (weapon.Level >= 3) return false;
        
        weaponToMerge.Clear();
        weaponToMerge.Add(weapon);
        
        Weapon[] weapons = playerWeapons.GetWeapons();
        
        foreach(var playerWeapon in weapons)
        {
            if (playerWeapon == null) continue;
            if (playerWeapon == weapon) continue;
            if (playerWeapon.WeaponData.Name != weapon.WeaponData.Name) continue;
            if (playerWeapon.Level != weapon.Level) continue;
            
            weaponToMerge.Add(playerWeapon);

            return true;
        }
        
        return false;
    }
    
    public void Merge()
    {
        Debug.Log("Merging weapons");
        if (weaponToMerge.Count < 2)
        {
            return;
        }
        
        DestroyImmediate(weaponToMerge[1].gameObject);
        
        weaponToMerge[0].Upgrade();
        
        Weapon weapon = weaponToMerge[0];
        weaponToMerge.Clear();
        
        onMerge?.Invoke(weapon);
    }
}
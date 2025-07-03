using UnityEngine;

public static class ResourcesManager
{
    private const string statIconsDataPath = "Data/Stat Icons";
    private const string objectsDataPath = "Data/Objects/";
    private const string weaponsDataPath = "Data/Weapons/";

    
    private static StatIcon[] statIcons;
    public static Sprite GetStatIcon(Stat stat)
    {
        if (statIcons == null)
        {
            StatIconSo data = Resources.Load<StatIconSo>(statIconsDataPath);
            if (data != null)
            {
                statIcons = data.StatIcons;
            }
            else
            {
                Debug.LogError($"Failed to load StatIconSo from path: {statIconsDataPath}");
                return null;
            }
        }

        foreach (var statIcon in statIcons)
        {
            if(stat == statIcon.stat) return statIcon.icon;
        }
        
        return null;
    }

    private static ObjectDataSO[] objectDatas;
    public static ObjectDataSO[] Objects
    {
        get
        {
            if(objectDatas == null)
            {
                objectDatas = Resources.LoadAll<ObjectDataSO>(objectsDataPath); 
            }
            return objectDatas;
        }
        private set
        {
            
        }
    }

    public static ObjectDataSO GetRandomObject()
    {
        return Objects[Random.Range(0, Objects.Length)];
    }
    
    private static WeaponDataSO[] weaponDatas;
    public static WeaponDataSO[] Weapons
    {
        get
        {
            if(weaponDatas == null)
            {
                weaponDatas = Resources.LoadAll<WeaponDataSO>(weaponsDataPath); 
            }
            return weaponDatas;
        }
        private set
        {
            
        }
    }

    public static WeaponDataSO GetRandomWeapon()
    {
        return Weapons[Random.Range(0, Weapons.Length)];
    }
}

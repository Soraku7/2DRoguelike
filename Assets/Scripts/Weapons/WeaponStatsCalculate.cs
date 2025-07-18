using System.Collections.Generic;
using UnityEngine;

public static class WeaponStatsCalculate
{
    public static Dictionary<Stat, float> GetStats(WeaponDataSO weaponData , int level)
    {
        float multiplier = 1 + (float)level / 3;
        Dictionary<Stat , float> calculatedStats = new Dictionary<Stat, float>();
        foreach (var stat in weaponData.BaseStats)
        {
            if(weaponData.Prefab.GetType() != typeof(RangeWeapon) && stat.Key == Stat.Range)
            {
                calculatedStats.Add(stat.Key, stat.Value);
            }
            else
            {
                calculatedStats.Add(stat.Key, stat.Value * multiplier);
            }
        }
        
        return calculatedStats;
    }
    
    public static int GetPruchasePrice(WeaponDataSO weaponData , int level)
    {
        float multiplier = 1 + (float)level / 3;
        return (int)(weaponData.PurchasePrice * multiplier);
    }
    
    public static int GetRecyclePrice(WeaponDataSO weaponData, int level)
    {
        float multiplier = 1 + (float)level / 3;
        return (int)(weaponData.RecyclePrice * multiplier);
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Scriptable Objects/WeaponDataSO")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int PurchasePrice { get; private set; }
    [field: SerializeField] public int RecyclePrice { get; private set; }
    
    [field: SerializeField] public Weapon Prefab { get; private set; }

    [SerializeField] public float attack;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float criticalChance;
    [SerializeField] public float criticalPrecent;
    [SerializeField] public float range;
    
    public Dictionary<Stat , float> BaseStats
    {
        get
        {
            return new Dictionary<Stat, float>()
            {
                {Stat.Attack, attack},
                {Stat.AttackSpeed, attackSpeed},
                {Stat.CriticalChance, criticalChance},
                {Stat.CriticalPrecent, criticalPrecent},
                {Stat.Range, range}
            };
        }
        private set
        {
            // This setter is intentionally left empty.
        }
    }
    
    public float GetStatValue(Stat stat)
    {
        if (BaseStats.TryGetValue(stat, out float value))
        {
            return value;
        }
        return 0f;
    }
}
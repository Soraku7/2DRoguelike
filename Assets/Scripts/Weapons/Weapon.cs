using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour , IPlayerStatsDepdendency
{
    [field: SerializeField] public WeaponDataSO WeaponData { get; private set; }
    
    [Header("Settings")] 
    [SerializeField] protected float range;

    [SerializeField] protected LayerMask enemyMask;

    [Header("Attack")] 
    [SerializeField] protected int damage;

    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;

    protected float attackTimer;
    
    [Header("Animation")] 
    [SerializeField] protected float aimLerp;

    [Header("Level")]
    public int Level { get; private set; }
    
    [Header("Critical")]
    protected int criticalChance;
    protected float criticalPercent;
    

    protected Enemy GetClosestEnemy()
    {
        Enemy closeEnemy = null;

        Collider2D[] enmies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        float minDistance = range;

        if (enmies.Length <= 0) return null;
        
        for (int i = 0; i < enmies.Length; i++)
        {
            Enemy EnemyChecked = enmies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position , EnemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closeEnemy = EnemyChecked;
                minDistance = distanceToEnemy;
            }
        }
        
        return closeEnemy;
    }

    protected int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        if (Random.Range(0, 101) <= criticalChance)
        {
            isCriticalHit = true;
            return Mathf.RoundToInt(damage * criticalPercent);
        }
        
        return damage;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    public abstract void UpdateStats(PlayerStatsManager playerStatsManager);

    protected void ConfigureStats()
    {
        float multiplier = 1 + Level / 3f;
        
        damage = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.Attack) * multiplier);
        attackDelay = 1f / (WeaponData.GetStatValue(Stat.AttackSpeed) * multiplier);

        criticalChance = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.CriticalChance) * multiplier);
        criticalPercent = WeaponData.GetStatValue(Stat.CriticalPrecent) * multiplier;

        if(WeaponData.Prefab.GetType() == typeof(RangeWeapon))
            range = WeaponData.GetStatValue(Stat.Range) * multiplier;
    }

    public void UpgrateTo(int targetLevel)
    {
        Level = targetLevel;
        ConfigureStats();
    }
}
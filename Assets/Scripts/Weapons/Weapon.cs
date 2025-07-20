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
    
    [Header("Audio")]
    protected AudioSource audioSource;

    protected void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = WeaponData.AttackSound;
        
        if(animator != null) animator.runtimeAnimatorController = WeaponData.AnimatorOverride; 
    }

    protected void PlayAttackSound()
    {
        if(!AudioManager.instance.IsSFXOn) return;
        
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }


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
        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculate.GetStats(WeaponData, Level);
        
        damage = Mathf.RoundToInt(calculatedStats[Stat.Attack]);
        attackDelay = 1f / calculatedStats[Stat.AttackSpeed];

        criticalChance = Mathf.RoundToInt(calculatedStats[Stat.CriticalChance]);
        criticalPercent = calculatedStats[Stat.CriticalPrecent];
        
        range = calculatedStats[Stat.Range];
    }

    public void UpgratdTo(int targetLevel)
    {
        Level = targetLevel;
        ConfigureStats();
    }
    
    
    public int GetRecyclePrice()
    {
        return WeaponStatsCalculate.GetRecyclePrice(WeaponData, Level);
    }

    public void Upgrade()
    {
        UpgratdTo(Level + 1);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    enum State
    {
        Idle,
        Attack
    }
    
    private State state;
    
    [Header("Elements")] 
    [SerializeField] private Transform hitDetectTransform;
    [SerializeField] private float hitDetectRadius;
    [SerializeField] private BoxCollider2D hitCollider;
    
    [Header("Settings")]
    private List<Enemy> damagedEnemies = new List<Enemy>();
    
    private void Start()
    {
        state = State.Idle;
    }
    
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            
            case State.Attack:
                Attacking();
                break;
        }
        
    }
    
    private void AutoAim()
    {
        Enemy closestMeleeEnemy = GetClosestEnemy();
        
        Vector2 targetUpVector = Vector2.up;

        if (closestMeleeEnemy != null)
        {
            ManageAttackTimer();
            targetUpVector = (closestMeleeEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
        }
        
        transform.up = Vector3.Lerp(transform.up , targetUpVector , aimLerp * Time.deltaTime);
        
        IncrementAttackTimer();
    }

    private void ManageAttackTimer()
    {

        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }

    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;

    }
    
    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;
        
        damagedEnemies.Clear();

        animator.speed = 1f / attackDelay;
        
        PlayAttackSound();
    }

    private void Attacking()
    {
        Attack();
    }

    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    private void Attack()
    {
        //Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectTransform.position, hitDetectRadius, enemyMask);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(hitDetectTransform.position, hitCollider.bounds.size,
            hitDetectTransform.localEulerAngles.z , enemyMask);

        foreach (var t in enemies)
        {
            Enemy enemy = t.GetComponent<Enemy>();

            if (!damagedEnemies.Contains(enemy))
            {
                int damage = GetDamage(out bool isCriticalHit);

                enemy.TakeDamage(damage , isCriticalHit);
                damagedEnemies.Add(enemy);
            }
        }
    }

    public override void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        ConfigureStats();
        
        damage = Mathf.RoundToInt(damage * (1 + playerStatsManager.GetStatValue(Stat.Attack) / 100f));
        attackDelay /= 1 + (playerStatsManager.GetStatValue(Stat.AttackSpeed) / 100f);
        
        criticalChance = Mathf.RoundToInt(1 + playerStatsManager.GetStatValue(Stat.CriticalChance) / 100f);
        criticalPercent += playerStatsManager.GetStatValue(Stat.CriticalPrecent);
        
        Debug.Log("damage" +damage);
    }
}

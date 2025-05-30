using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
    [SerializeField] private float range;

    [SerializeField] private LayerMask enemyMask;

    [Header("Attack")] 
    [SerializeField] private int damage;

    [SerializeField] private float attackDelay;
    [SerializeField] private Animator animator;
    private List<Enemy> damagedEnemies = new List<Enemy>();
    private float attackTimer;
    
    [Header("Animation")] 
    [SerializeField] private float aimLerp;


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
                enemy.TakeDamage(damage);
                damagedEnemies.Add(enemy);
            }
        }
    }

    private Enemy GetClosestEnemy()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectTransform.position, hitDetectRadius);
    }
}
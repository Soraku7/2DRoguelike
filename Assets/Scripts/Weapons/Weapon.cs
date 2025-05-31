using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float range;

    [SerializeField] protected LayerMask enemyMask;

    [Header("Attack")] 
    [SerializeField] protected int damage;

    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;

    protected float attackTimer;
    
    [Header("Animation")] 
    [SerializeField] protected float aimLerp;
    

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}
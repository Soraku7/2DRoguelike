using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class MeleeEnemy : Enemy
{


    [Header("Attack")] 
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;

    private float attackDelay;
    private float attackTimer;

    protected override void Start()
    {
        base.Start();
        
        healthText.text = health.ToString();
        
        attackDelay = 1f / attackFrequency;
    }

    private void Update()
    {
        if (!CanAttack()) return;
        
        if(attackTimer >= attackDelay)  TryAttack();
        else Wait();
        
        movement.FollowPlayer();
    }


    private void Attack()
    {
        attackTimer = 0;

        player.TakeDamage(damage);
    }
    
    private void TryAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= playerDetectRadius)
        {
            // PassAway();
            Attack();
        }
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }


}

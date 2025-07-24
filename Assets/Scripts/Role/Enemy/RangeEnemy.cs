using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement) , typeof(RangeEnemyAttack))]
public class RangeEnemy : Enemy
{  

    private RangeEnemyAttack attack;

    protected override void Start()
    {
        base.Start();
        
        healthText.text = health.ToString();
        
        attack = GetComponent<RangeEnemyAttack>();
        attack.StorePlayer(player);
    }
    
    private void Update()
    {
        if (!CanAttack()) return;
        ManageAttack();
        
        transform.localScale = player.transform.position.x > transform.position.x ? Vector3.one : Vector3.one.With(x: -1);
    }
    
    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
    
        if (distanceToPlayer > playerDetectRadius)
        {
            movement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }
    
    private void TryAttack()
    {
        attack.AutoAnim();
    }

}

using System;
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

    [Header("Settings")] 
    [SerializeField] private float range;

    [SerializeField] private LayerMask enemyMask;

    [Header("Attack")] 
    [SerializeField] private int damage;
    
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
                break;
            
            case State.Attack:
                break;
        }
        
        AutoAim();
        
        Attack();
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        
        Vector2 targetUpVector = Vector2.up;

        if (closestEnemy != null) targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
        
        transform.up = Vector3.Lerp(transform.up , targetUpVector , aimLerp * Time.deltaTime);
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectTransform.position, hitDetectRadius, enemyMask);

        foreach (var t in enemies)
        {
            t.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private Enemy GetClosestEnemy()
    {
        Enemy closeEnemy = null;
        Vector2 targetUpVector = Vector2.up;

        Collider2D[] enmies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        

        float minDistance = range;

        if (enmies.Length <= 0) return null;
        
        for (int i = 0; i < enmies.Length; i++)
        {
            Enemy enemyChecked = enmies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position , enemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closeEnemy = enemyChecked;
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
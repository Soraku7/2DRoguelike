using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float range;

    [SerializeField] private LayerMask enemyMask;

    [Header("Animation")] 
    [SerializeField] private float aimLerp;
    
    private void Update()
    {
        AutoAim();
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        
        Vector2 targetUpVector = Vector2.up;

        if (closestEnemy != null) targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
        
        transform.up = Vector3.Lerp(transform.up , targetUpVector , aimLerp * Time.deltaTime);
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
    }
}
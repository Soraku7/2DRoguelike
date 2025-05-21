using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float range;

    [SerializeField] private LayerMask enemyMask; 
    
    private void Update()
    {
        Enemy closeEnemy = null;

        Collider2D[] enmies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        

        float minDistance = range;

        if (enmies.Length == 0)
        {
            transform.up = Vector3.up;
            return;
        }
        
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

        if (closeEnemy == null)
        {
            transform.up = Vector3.up;
            return;
        }
        
        transform.up = (closeEnemy.transform.position - transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
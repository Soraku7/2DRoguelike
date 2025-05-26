using System;
using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private EnemyBullet bulletPrefab;
    private Player player;

    [Header("Settings")] 
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    private void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
    }

    public void AutoAnim()
    {

        ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            Shoot();
        }
    }
    
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;
        
        EnemyBullet bulletInstance = Instantiate(bulletPrefab , shootingPoint.position , Quaternion.identity);
        
        bulletInstance.Shoot(damage , direction);  
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }
    
}
using System;
using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
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

    private Vector2 gizmosDirection;
    private void Shoot()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        gizmosDirection = direction;
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(shootingPoint.position , (Vector2)shootingPoint.position + gizmosDirection * 5f);
    }
}
using System;
using UnityEngine;
using UnityEngine.Pool;

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

    [Header("Bullet Polling")]
    private ObjectPool<EnemyBullet> bulletPool;
    private void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
        
        bulletPool = new ObjectPool<EnemyBullet>(CreateFunction , ActionOnGet , ActionOnRelease , ActionOnDestroy);
    }
    
    private EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab , shootingPoint.position , Quaternion.identity);
        bulletInstance.Configure(this);
        
        return bulletInstance;
    }

    private void ActionOnGet(EnemyBullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        
        bullet.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void ReleaseBullet(EnemyBullet bullet)
    {
        bulletPool.Release(bullet);
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

        InstantShoot(direction);
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    public void InstantShoot(Vector2 direction)
    {
        EnemyBullet bulletInstance = bulletPool.Get();
        
        bulletInstance.Shoot(damage , direction); 
    }
    
}
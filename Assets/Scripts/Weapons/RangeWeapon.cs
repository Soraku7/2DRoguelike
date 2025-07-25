using System;
using UnityEngine;
using UnityEngine.Pool;

public class RangeWeapon : Weapon
{
    [Header("Elements")] 
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;

    [Header("Pooling")]
    private ObjectPool<Bullet> bulletPool;

    [Header("Actions")]
    public static Action onBulletShot;
    
    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }
    private Bullet CreateFunction()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab , shootingPoint.position , Quaternion.identity);
        bulletInstance.Configure(this);
        
        return bulletInstance;
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        
        bullet.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    
    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

    private void Update()
    {
        AutoAim();
    }
    
    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        
        Vector2 targetUpVector = Vector2.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.GetCenter() - (Vector2)transform.position).normalized;
            transform.up = targetUpVector;
            ManageShooting();
            return;
        }
        transform.up = Vector3.Lerp(transform.up , targetUpVector , aimLerp * Time.deltaTime);

      
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
        int damage = GetDamage(out bool isCriticalHit);
        
        Bullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(damage , transform.up , isCriticalHit);
        
        onBulletShot?.Invoke();
        
        PlayAttackSound();
    }


    public override void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        ConfigureStats();
        
        damage = Mathf.RoundToInt(damage * (1 + playerStatsManager.GetStatValue(Stat.Attack) / 100f));
        attackDelay /= 1 + (playerStatsManager.GetStatValue(Stat.AttackSpeed) / 100f);
        
        criticalChance = Mathf.RoundToInt(1 + playerStatsManager.GetStatValue(Stat.CriticalChance) / 100f);
        criticalPercent += playerStatsManager.GetStatValue(Stat.CriticalPrecent);
        
        range += playerStatsManager.GetStatValue(Stat.Range) / 10;
        
        Debug.Log("damage" +damage);
    }
}
using UnityEngine;

public class RangeWeapon : Weapon
{
    [Header("Elements")] 
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;

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
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
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
        Bullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Shoot(damage , transform.up);
    }
}
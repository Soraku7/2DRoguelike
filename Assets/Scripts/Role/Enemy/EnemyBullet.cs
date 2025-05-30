using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    
    [Header("Elements")]
    private Rigidbody2D rig;
    private Collider2D collider;
    private RangeEnemyAttack rangeEnemyAttack;

    [Header("Settings")] 
    [SerializeField] private float moveSpeed;
    private int damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        
        float timer = 0;

        StartCoroutine(ReleaseCoroutine());
    }

    IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(5f);
        
        rangeEnemyAttack.ReleaseBullet(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            StopAllCoroutines();
            player.TakeDamage(damage);
            this.collider.enabled = false;
            
            rangeEnemyAttack.ReleaseBullet(this);
        }
    }

    public void Shoot(int damage , Vector2 direction)
    {
        this.damage = damage;
        
        Debug.Log(direction);
        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;
    }

    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
    }

    public void Reload()
    {
        rig.linearVelocity = Vector2.zero;
        collider.enabled = true;
    }
}

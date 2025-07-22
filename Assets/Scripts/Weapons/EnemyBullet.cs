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
    [SerializeField] private float angularSpeed;
    private int damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

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

        if (Mathf.Abs(direction.x + 1) < 0.01f) direction.y += 0.1f;
        
        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;
        rig.angularVelocity = angularSpeed;
    }

    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
    }

    public void Reload()
    {
        rig.linearVelocity = Vector2.zero;
        rig.angularVelocity = 0;
        collider.enabled = true;
        
        DOTween.Clear();
        float timer = 0;
        //DOTwwen.To()中参数：前两个参数是固定写法，第三个是到达的最终值，第四个是渐变过程所用的时间
        Tween t = DOTween.To(() => timer, x => timer = x, 1, 5f)
            .OnStepComplete(() =>
            {
                rangeEnemyAttack.ReleaseBullet(this);
            });
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    
    [Header("Elements")]
    private Rigidbody2D rig;

    [Header("Settings")] 
    [SerializeField] private float moveSpeed;
    private int damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void Shoot(int damage , Vector2 direction)
    {
        this.damage = damage;
        
        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    private PlayerHealth playerHealth;

    [SerializeField] private CircleCollider2D collider;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public Vector2 GetCenter()
    {
        return (Vector2)transform.position + collider.offset;
    }
}

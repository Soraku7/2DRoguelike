using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerLevel))]
public class Player : MonoBehaviour
{
    public static Player instance;
    
    [Header("Components")]
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;

    [SerializeField] private CircleCollider2D collider;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        playerHealth = GetComponent<PlayerHealth>();
        playerLevel = GetComponent<PlayerLevel>();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public Vector2 GetCenter()
    {
        return (Vector2)transform.position + collider.offset;
    }

    public bool HasLevelUp()
    {
        return playerLevel.HasLevelUp();
    }
}

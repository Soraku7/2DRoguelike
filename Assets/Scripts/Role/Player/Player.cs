using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
}

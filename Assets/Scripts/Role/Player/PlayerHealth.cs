using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private int maxHealth;
    private int health;
    
    [Header("Element")]
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        health = maxHealth;
        
        healthSlider.value = 1;
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        
        health -= realDamage;

        float healthBarValue = (float)health / maxHealth;
        healthSlider.value = healthBarValue;

        if (health <= 0)
        {
            PassAway();
        }
    }
    
    private void PassAway()
    {
        Debug.Log("Player is dead");
    }
}

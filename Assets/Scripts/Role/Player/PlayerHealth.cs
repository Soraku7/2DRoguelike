using System;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour , IPlayerStatsDepdendency
{
    [Header("Settings")]
    [SerializeField] private int baseMaxHealth = 10;
    private int maxHealth;
    private int health;
    
    [Header("Element")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTex;

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        
        health -= realDamage;

        UpdateUI();
 
        if (health <= 0)
        {
            PassAway();
        }
    }

    private void UpdateUI()
    {
        float healthBarValue = (float)health / maxHealth;
        
        healthTex.text = health + " / " + maxHealth;
        healthSlider.value = healthBarValue;
    }
    
    private void PassAway()
    {
        Debug.Log("Player is dead");
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float addedHealth = playerStatsManager.GetStatValue(Stat.MaxHealth);
        maxHealth = baseMaxHealth + (int)addedHealth;
        maxHealth = Mathf.Max(maxHealth, 1); // Ensure max health is at least 1
        
        health = maxHealth;
        UpdateUI();
    }
}

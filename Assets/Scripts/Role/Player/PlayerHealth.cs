using System;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour , IPlayerStatsDepdendency
{
    [Header("Settings")]
    [SerializeField] private int baseMaxHealth = 10;
    private float maxHealth;
    private float health;
    private float armor;
    private float lifeSteal;
    private float dodge;
    
    private float healthRecoveryValue;
    private float healthRecoverySpeed;
    private float healthRecoveryTimer;
    private float healthRecoveryDuration;
    
    [Header("Element")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTex;

    [Header("Actions")]
    public static Action<Vector2> onAttackedDodged;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyTookDamageCallBack;
    }

    private void Update()
    {
        healthRecoveryTimer += Time.deltaTime;

        RecoverHealth();
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyTookDamageCallBack;
    }
    
    private void RecoverHealth()
    {
        if(healthRecoveryTimer >= healthRecoveryDuration)
        {
            healthRecoveryTimer = 0f;

            float healthToAdd = Mathf.Min(.1f, maxHealth - health);
            health += healthToAdd;
            
            UpdateUI();
        }
    }

    private void EnemyTookDamageCallBack(int damage, Vector2 enemyPos, bool isCriticalHit)
    {
        if (health >= maxHealth) return;
        
        float lifeStealValue = damage * lifeSteal;
        float healthToAdd = Math.Min(lifeStealValue , maxHealth - health);

		health += healthToAdd;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {

        if (ShouldDodge())
        {
            onAttackedDodged?.Invoke(transform.position);
            return;
        }
        
        float realDamage = damage * Mathf.Clamp(1 - (armor / 1000f) , 0 , 1000);
        realDamage = Mathf.Min(health, damage);
        
        health -= realDamage;
        
        Debug.Log("realtDamage:" + realDamage);

        UpdateUI();
 
        if (health <= 0)
        {
            PassAway();
        }
    }

    private bool ShouldDodge()
    {
        return Random.Range(0, 100) < dodge;
    }

    private void UpdateUI()
    {
        float healthBarValue = health / maxHealth;
        
        healthTex.text = (int)health + " / " + maxHealth;
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
        
        armor = playerStatsManager.GetStatValue(Stat.Armor);
        lifeSteal = playerStatsManager.GetStatValue(Stat.LifeSteal) / 100;
        dodge = playerStatsManager.GetStatValue(Stat.Dodge);

		healthRecoverySpeed = Mathf.Max(.0001f , playerStatsManager.GetStatValue(Stat.HealthRecoverySpeed));
        healthRecoveryDuration = 1f / healthRecoverySpeed;
    }
}
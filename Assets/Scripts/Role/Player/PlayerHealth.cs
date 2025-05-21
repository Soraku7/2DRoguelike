using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private int maxHealth;
    private int health;
    
    [Header("Element")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTex;

    private void Start()
    {
        health = maxHealth;

        UpdateUI();
    }

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
        
        healthTex.text = healthBarValue + " / " + maxHealth;
        healthSlider.value = healthBarValue;
    }
    
    private void PassAway()
    {
        Debug.Log("Player is dead");
        SceneManager.LoadScene(0);
    }
}

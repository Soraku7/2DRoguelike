using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    [Header("Visuals")] 
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Settings")]
    [SerializeField] private int requireXp;
    [SerializeField] private int currentXp;
    private int level;
    private int levelsEarnedThisWave;

    private void Awake()
    {
        Candy.onCollection += CandyCollectedCallback;
    }

    private void Start()
    {
        level = 0;
        UpdateRequireXp();
        UpdateVisual();
    }

    private void OnDestroy()
    {
        Candy.onCollection -= CandyCollectedCallback;
    }

    private void UpdateRequireXp()
    {
        requireXp = (level + 1) * 5;
    }

    private void UpdateVisual()
    {
        xpBar.value = (float)currentXp / requireXp;
        levelText.text = "lvl" + (level + 1);
    }

    private void CandyCollectedCallback(Candy candy)
    {
        currentXp++;

        if (currentXp >= requireXp) LevelUp();

        
        UpdateVisual();
    }

    private void LevelUp()
    {
        level++;
        levelsEarnedThisWave++;
        currentXp = 0;
        UpdateRequireXp();
    }

    public bool HasLevelUp()
    {
        if (levelsEarnedThisWave > 0)
        {
            levelsEarnedThisWave--;
            return true;
        }

        return false;
    }
}
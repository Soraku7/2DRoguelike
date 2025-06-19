using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    
    [Header("Data")]
    [SerializeField] private CharacterDataSO playerData;
    
    [Header("Settings")]
    // private List<StatData> addends = new List<StatData>();
    private Dictionary<Stat, float> addends = new Dictionary<Stat, float>();
    private Dictionary<Stat, float> playerStats = new Dictionary<Stat, float>();

    private void Awake()
    {
        playerStats = playerData.BaseStats;

        foreach (var stat in playerStats)
        {
            addends.Add(stat.Key, 0f);
        }
    }

    private void Start()
    {
        UpdatePlayerStats();
    }

    public void AddPlayerStat(Stat stat , float value)
    {
        if(addends.ContainsKey(stat))
        {
            addends[stat] += value;
        }
        
        UpdatePlayerStats();
       
    }

    private void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsDepdendency> playerStatsDepdendencies =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPlayerStatsDepdendency>();

        foreach (var depdendency in playerStatsDepdendencies)
        {
            depdendency.UpdateStats(this);
        }
    }

    public float GetStatValue(Stat stat)
    {
        float value = playerStats[stat] + addends[stat];
        return value;
    }
}



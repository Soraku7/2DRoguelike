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
    private Dictionary<Stat, float> objectAddends = new Dictionary<Stat, float>();

    private void Awake()
    {
        playerStats = playerData.BaseStats;

        foreach (var stat in playerStats)
        {
            addends.Add(stat.Key, 0f);
            objectAddends.Add(stat.Key, 0f);
        }
        
        CharacterSelectionManager.OnCharacterSelected += CharacterSelectedCallback;
    }

    private void Start()
    {
        UpdatePlayerStats();
    }
    
    private void OnDestroy()
    {
        CharacterSelectionManager.OnCharacterSelected -= CharacterSelectedCallback;
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
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include , FindObjectsSortMode.None).OfType<IPlayerStatsDepdendency>();

        foreach (var depdendency in playerStatsDepdendencies)
        {
            depdendency.UpdateStats(this);
        }
    }

    public float GetStatValue(Stat stat)
    {
        return playerStats[stat] + addends[stat] + objectAddends[stat];
    }

    public void AddObejct(Dictionary<Stat , float> objectData)
    {
        var keys = objectData.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            var key = keys[i];
            objectAddends[key] += objectData[key];
        }
        
        UpdatePlayerStats();
    }

    public void RemoveObjectStats(Dictionary<Stat, float> objectStats)
    {
        var keys = objectStats.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            var key = keys[i];
            objectAddends[key] -= objectStats[key];
        }
        
        UpdatePlayerStats();
    }
    
    private void CharacterSelectedCallback(CharacterDataSO characterData)
    {
        playerData = characterData;
        playerStats = playerData.BaseStats;
        
        UpdatePlayerStats();
    }
}



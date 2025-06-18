using System;
using System.Security;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private UpgrateContainer[] upgradeContainers;
    
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeContainers();
                break;
        }
    }
    
    private void ConfigureUpgradeContainers()
    {
        for(int i = 0; i < upgradeContainers.Length; i++)
        {
            string randomStatString = "";
            int randomStatIndex = Random.Range(1, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomStatIndex);
            
            randomStatString = Enums.FormatStat(stat);
            
            string buttonString;
            Action action = GetActionToPerform(stat , out buttonString);
            upgradeContainers[i].Configure(null , randomStatString , buttonString);
            
            upgradeContainers[i].Button.onClick.RemoveAllListeners();
            
            upgradeContainers[i].Button.onClick.AddListener(() => action?.Invoke());
            upgradeContainers[i].Button.onClick.AddListener(BonusSelectedCall);
        }
    }

    private void BonusSelectedCall()
    {
        GameManager.instance.WaveCompleteCallback();
    }
    
    private Action GetActionToPerform(Stat stat , out string buttonString)
    {
        
        buttonString = "";
        float value;
        
        value = Random.Range(1, 10);
        
        switch (stat)
        {
            case Stat.Attack:
                value = Random.Range(1, 10);
                break;
            
            case Stat.AttackSpeed:                
                value = Random.Range(1, 10);
                break;
            
            case Stat.CriticalChance:               
                value = Random.Range(1, 10);
                break;
            
            case Stat.CriticalPrecent:                 
                value = Random.Range(1f, 2f);
                buttonString = "+" + value.ToString("F2") + "x";
                break;
            
            case Stat.MoveSpeed:                 
                value = Random.Range(1, 10);
                break;
            
            case Stat.MaxHealth:                
                value = Random.Range(1, 5);
                buttonString = "+" + value;
                break;
            
            case Stat.Range:                
                value = Random.Range(1, 5);
                buttonString = "+" + value;
                break;
            
            case Stat.HealthRecoverySpeed:                
                value = Random.Range(1, 10);
                break;
            
            case Stat.Armor:                 
                value = Random.Range(1, 10);
                break;
            
            case Stat.Luck:                 
                value = Random.Range(1, 10);
                break;
            
            case Stat.Dodge:                
                value = Random.Range(1, 10);
                break;
            
            case Stat.LifeSteal:                
                value = Random.Range(1, 10);
                break;
        }
        buttonString = "+" + value + "%";
        return () => playerStatsManager.AddPlayerStat(stat, value);
    }
}
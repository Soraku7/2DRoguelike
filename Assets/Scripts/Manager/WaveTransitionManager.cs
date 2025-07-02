using System;
using System.Security;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour , IGameStateListener
{
    public static WaveTransitionManager instance;
    
    [Header("Player")]
    [SerializeField] private PlayerObjects playerObjects;
    
    [Header("Elements")]
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private UpgrateContainer[] upgradeContainers;
    [SerializeField] private GameObject upgradeContainerParent;

    [Header("Chest Relate Stuff")]
    [SerializeField] private ChestObjectContainer chestObjectContainerPrefab;
    [SerializeField] private Transform chestContainerParent;
    
    [Header("Settings")] 
    private int chestCollected;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        
        Chest.onCollect += ChestCollectedCallback;
    }

    private void OnDestroy()
    {
        Chest.onCollect -= ChestCollectedCallback;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                // ConfigureUpgradeContainers();
                TryOpenChest();
                break;
        }
    }

    private void TryOpenChest()
    {
        chestContainerParent.Clear();
        
        if (chestCollected > 0)
        {
            ShowObjects();
        }
        else ConfigureUpgradeContainers();
    }

    private void ShowObjects()
    {
        chestCollected--;
        
        upgradeContainerParent.SetActive(false);

        ObjectDataSO[] objectDatas = ResourcesManager.Objects;
        ObjectDataSO randomObject = objectDatas[Random.Range(0, objectDatas.Length)];

        ChestObjectContainer constainerInstance = Instantiate(chestObjectContainerPrefab, chestContainerParent);
        constainerInstance.Configure(randomObject);
        
        constainerInstance.TakeButton.onClick.AddListener(() => TakeButtonCallback(randomObject));
        constainerInstance.RecycleButton.onClick.AddListener(() => RecycleButtonCallback(randomObject));
    }
    
    private void TakeButtonCallback(ObjectDataSO objectData)
    {
        playerObjects.AddObject(objectData);
        TryOpenChest();
    }

    private void RecycleButtonCallback(ObjectDataSO objectData)
    {
        CurrencyManager.instance.AddCurrency(objectData.RecyclePrice);
        TryOpenChest();
    }

    
    private void ConfigureUpgradeContainers()
    {
        
        upgradeContainerParent.SetActive(true);
        
        for(int i = 0; i < upgradeContainers.Length; i++)
        {
            string randomStatString = "";
            int randomStatIndex = Random.Range(1, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomStatIndex);
            
            Sprite upgrateIcon = ResourcesManager.GetStatIcon(stat);
            
            randomStatString = Enums.FormatStat(stat);
            
            string buttonString; 
            Action action = GetActionToPerform(stat , out buttonString);
            upgradeContainers[i].Configure(upgrateIcon , randomStatString , buttonString);
            
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
                buttonString = "+" + value + "%";
                break;
            
            case Stat.AttackSpeed:                
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%"; 
                break;
            
            case Stat.CriticalChance:               
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%";
                break;
            
            case Stat.CriticalPrecent:                 
                value = Random.Range(1f, 2f);
                buttonString = "+" + value.ToString("F2") + "x";
                break;
            
            case Stat.MoveSpeed:                 
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%";
                break;
            
            case Stat.MaxHealth:                
                value = Random.Range(1, 5);
                buttonString = "+" + value;
                buttonString = "+" + value;
                break;
            
            case Stat.Range:                
                value = Random.Range(1, 5);
                buttonString = "+" + value;
                break;
            
            case Stat.HealthRecoverySpeed:                
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%";
                break;
            
            case Stat.Armor:                 
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%";
                break;
            
            case Stat.Luck:                 
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%";
                break;
            
            case Stat.Dodge:                
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%";
                break;
            
            case Stat.LifeSteal:                
                value = Random.Range(1, 10);
                buttonString = "+" + value + "%";
                break;
        }
        // buttonString = Enums.FormatStat(stat) + " " + buttonString;
        return () => playerStatsManager.AddPlayerStat(stat, value);
    }
    
    
    private void ChestCollectedCallback()
    {
        chestCollected++;
    }

    public bool HasCollectedChest()
    {
        return chestCollected > 0;
    }
}
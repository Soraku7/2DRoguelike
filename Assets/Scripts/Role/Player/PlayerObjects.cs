using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    [field: SerializeField] public List<ObjectDataSO> Objects { get; private set; }
    private PlayerStatsManager playerStatsManager;

    private void Awake()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }
    
    private void Start()
    {
        foreach (var objectData in Objects)
        {
            playerStatsManager.AddObejct(objectData.BaseStats);
        }
    }

    public void AddObject(ObjectDataSO objectData)
    {
        Objects.Add(objectData);
        playerStatsManager.AddObejct(objectData.BaseStats);
    }

    public void RecycleObject(ObjectDataSO objectData)
    {
        Objects.Remove(objectData);
        
        CurrencyManager.instance.AddCurrency(objectData.RecyclePrice);
        
        playerStatsManager.RemoveObjectStats(objectData.BaseStats);
    }
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
    [SerializeField] private Chest chestPrefab;
    
    [Header("Settings")]
    [SerializeField] [Range(0 , 100)]private float cashDropChance;
    [SerializeField] [Range(0 , 100)]private float chestDropDelay;

    [Header("Pooling")] 
    private ObjectPool<Candy> candyPool;
    private ObjectPool<Cash> cashPool;
    
    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassAwayCallBack;
        Candy.onCollection += ReleaseCandy;
        Cash.onCollection += ReleaseCash;
    }

    private void Start()
    {
        candyPool = new ObjectPool<Candy>(CandyCreateFunction,
            CandyActionOnGet,
            CandyActionOnRelease,
            CandyActionOnDestroy);
        cashPool = new ObjectPool<Cash>(CashCreateFunction,
            CashActionOnGet,
            CashActionOnRelease,
            CashActionOnDestroy);
    }

    private void OnDestroy()
    {
        Enemy.onPassedAway -= EnemyPassAwayCallBack;
        Candy.onCollection -= ReleaseCandy;
        Cash.onCollection -= ReleaseCash;
    }

    #region CandyPool

    private Candy CandyCreateFunction()
    {
        Candy bulletInstance = Instantiate(candyPrefab , transform);
        // bulletInstance.Configure(this);
        
        return bulletInstance;
    }

    private void CandyActionOnGet(Candy candy)
    {
        
        candy.gameObject.SetActive(true);
    }
    
    private void CandyActionOnRelease(Candy candy)
    {
        candy.gameObject.SetActive(false);
    }

    private void CandyActionOnDestroy(Candy candy)
    {
        Destroy(candy.gameObject);
    }

    #endregion
    

    #region CashPool
    private Cash CashCreateFunction()
    {
        Cash bulletInstance = Instantiate(cashPrefab , transform);
        // bulletInstance.Configure(this);
        
        return bulletInstance;
    }

    private void CashActionOnGet(Cash cash)
    {
        
        cash.gameObject.SetActive(true);
    }
    
    private void CashActionOnRelease(Cash cash)
    {
        cash.gameObject.SetActive(false);
    }

    private void CashActionOnDestroy(Cash cash)
    {
        Destroy(cash.gameObject);
    }
    

    #endregion


    private void EnemyPassAwayCallBack(Vector2 enemyPosition)
    {
        bool shouldSpawnCash = Random.Range(0, 101) <= cashDropChance;
        
        DroppableCurrency droppable = shouldSpawnCash ? cashPool.Get() : candyPool.Get();
        droppable.transform.position = enemyPosition;
        
        TryDropChest(enemyPosition);
    }

    private void TryDropChest(Vector2 spawnPosition)
    {
        bool shouldSpawnChest = Random.Range(0, 101) <= chestDropDelay;

        if (!shouldSpawnChest) return;
        
        Instantiate(chestPrefab , spawnPosition , Quaternion.identity , transform);

    }
    

    private void ReleaseCandy(Candy candy)
    {
        candyPool.Release(candy);
    }

    private void ReleaseCash(Cash cash)
    {
        cashPool.Release(cash);   
    }
}

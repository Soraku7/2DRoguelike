using System;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Candy candyPrefab;

    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassAwayCallBack;
    }

    private void OnDestroy()
    {
        Enemy.onPassedAway -= EnemyPassAwayCallBack;
    }

    private void EnemyPassAwayCallBack(Vector2 enemyPosition)
    {
        Instantiate(candyPrefab, enemyPosition, Quaternion.identity , transform);
    }
}

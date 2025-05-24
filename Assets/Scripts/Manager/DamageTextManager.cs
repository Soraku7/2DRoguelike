using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageTextManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    private void Awake()
    {
        Enemy.onDamageTaken += InstantiateDamageText;
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= InstantiateDamageText;
    }

    [NaughtyAttributes.Button]
    private void InstantiateDamageText(int damage , Vector2 enemyPos)
    {
        Vector3 spawnPosition = enemyPos + Vector2.up * 1.5f;
        DamageText damageTextInstance = Instantiate(damageTextPrefab ,spawnPosition , Quaternion.identity , transform);
        damageTextInstance.Animate(damage);
    }
}

using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;
    
    [Header("Pooling")]
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        MeleeEnemy.onDamageTaken += EnemyHitCallBack;
    }

    private void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction , ActionOnGet , ActionOnRelease , ActionOnDestroy);
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab , transform);
    }

    private void ActionOnGet(DamageText damageText)
    {
        damageText.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }

    private void OnDestroy()
    {
        MeleeEnemy.onDamageTaken -= EnemyHitCallBack;
    }
    
    private void EnemyHitCallBack(int damage , Vector2 enemyPos)
    {
        DamageText damageTextInstance = damageTextPool.Get();
        
        Vector3 spawnPosition = enemyPos + Vector2.up * 1.5f;
        damageTextInstance.transform.position = spawnPosition;
        
        damageTextInstance.Animate(damage);

        float timer = 0;

        DOTween.To(() => timer, x => timer = x, 1, 1f).OnStepComplete(() =>
            {
                damageTextPool.Release(damageTextInstance);
            });    

    }
}
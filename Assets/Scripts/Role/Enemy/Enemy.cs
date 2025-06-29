using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Component")]
    protected EnemyMovement movement;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    protected int health;
    [SerializeField] protected TextMeshPro healthText;
    
    [Header("Element")]
    protected Player player;
    
    [Header("Spawn Sequence Related")] 
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    [SerializeField] protected Collider2D collider;
    protected bool hasSpawn = false;
    
    [Header("Effect")]
    [SerializeField] protected ParticleSystem passAwayParticle;

    [Header("Attack")] 
    [SerializeField] protected float playerDetectRadius;
    
    [Header("Action")]
    public static Action<int , Vector2 , bool> onDamageTaken;
    public static Action<Vector2> onPassedAway;
    
    [Header("Debugger")]
    [SerializeField] protected bool debug;

    protected virtual void Start()
    {
        health = maxHealth;
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
        
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            Destroy(gameObject);
        }

        StartSpawnSequence();
    }

    protected bool CanAttack()
    {
        return render.enabled;
    }

    private void StartSpawnSequence()
    {
        
        render.enabled = false;
        spawnIndicator.enabled = true;
        
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.3f;
        spawnIndicator.transform.DOScale(targetScale, 0.3f).SetLoops(4).OnComplete(() =>
        {
            SetRenderVisibility();
            
            hasSpawn = true;
            collider.enabled = true;

            movement.StorePlayer(player);
        });
    }
    
    private void SetRenderVisibility(bool visible = true)
    {
        render.enabled = visible;
        spawnIndicator.enabled = !visible;
    }

        
    public void PassAway()
    {
        onPassedAway?.Invoke(transform.position);

        PassAwayAfterWave();
    }

    public void PassAwayAfterWave()
    {
        passAwayParticle.transform.SetParent(null);
        passAwayParticle.Play();
        
        Destroy(gameObject);
    }
    
    public void TakeDamage(int damage , bool isCirticalHit)
    {
        int realDamage = Mathf.Min(health, damage);
        
        health -= realDamage;
        
        Debug.Log("Player takes " + damage + " damage");
        
        healthText.text = health.ToString();
        
        onDamageTaken?.Invoke(damage , transform.position , isCirticalHit);

        if (health <= 0)
        {
            PassAway();
        }
    }
    
    private void OnDrawGizmos()
    {
        if (!debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRadius);

        Gizmos.color = Color.white;
        
    }
}

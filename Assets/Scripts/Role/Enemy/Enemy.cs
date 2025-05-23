using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    
    [Header("Health")] 
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private TextMeshPro healthText;
    
    [Header("Elements")]
    private Player player;
    
    [Header("Component")]
    private EnemyMovement movement;
    
    [Header("Spawn Sequence Related")] 
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private Collider2D collider;
    private bool hasSpawn = false;
    
    [Header("Effect")]
    [SerializeField] private ParticleSystem passAwayParticle;
    
    [Header("Attack")] 
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectRadius;

    private float attackDelay;
    private float attackTimer;
    
    [Header("Action")]
    public static Action<int , Vector2> onDamageTaken;
    
    [Header("Debugger")]
    [SerializeField] private bool debug;

    private void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();
        
        player = FindFirstObjectByType<Player>();

        movement = GetComponent<EnemyMovement>();

        if (player == null)
        {
            Debug.LogWarning("Player not found");
            Destroy(gameObject);
        }

        StartSpawnSequence();
        
        attackDelay = 1f / attackFrequency;
    }

    private void Update()
    {
        if(attackTimer >= attackDelay)  TryAttack();
        else Wait();
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
    
    private void Attack()
    {
        attackTimer = 0;

        player.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        
        health -= realDamage;
        
        Debug.Log("Player takes " + damage + " damage");
        
        healthText.text = health.ToString();
        
        onDamageTaken?.Invoke(damage , transform.position);

        if (health <= 0)
        {
            PassAway();
        }
    }
    
    private void TryAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= playerDetectRadius)
        {
            // PassAway();
            Attack();
        }
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void SetRenderVisibility(bool visible = true)
    {
        render.enabled = visible;
        spawnIndicator.enabled = !visible;
    }
    
    private void PassAway()
    {
        passAwayParticle.transform.SetParent(null);
        passAwayParticle.Play();
        
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        if (!debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRadius);

        Gizmos.color = Color.white;
        
    }

}

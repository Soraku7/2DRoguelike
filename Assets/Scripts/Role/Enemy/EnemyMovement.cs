using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related")] 
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private SpriteRenderer spawnIndicator;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectRadius;
    
    [SerializeField] private ParticleSystem passAwayParticle;
    
    [Header("Debugger")]
    [SerializeField] private bool debug;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("Player not found");
            Destroy(gameObject);
        }
        
        render.enabled = false;
        spawnIndicator.enabled = true;
        
        
    }
    
    private void Update()
    {
        FollowPlayer();
        
        TryAttack();
    }

    private void PassAway()
    {
        passAwayParticle.transform.SetParent(null);
        passAwayParticle.Play();
        
        Destroy(gameObject);
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        Vector2 targetPosition = (Vector2)transform.position + direction * (moveSpeed * Time.deltaTime);
        
        transform.position = targetPosition;
    }
    
    private void TryAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= playerDetectRadius)
        {
            PassAway();
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRadius);
    }
}
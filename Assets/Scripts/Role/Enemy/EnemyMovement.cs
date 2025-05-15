using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectRadius;
    
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
    }
    
    private void Update()
    {
        FollowPlayer();
        
        TryAttack();
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
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRadius);
    }
}
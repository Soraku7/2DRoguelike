using System;
using DG.Tweening;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;


    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    private void Update()
    {
        // if(player != null) FollowPlayer();
    }

    public void StorePlayer(Player _player)
    {
        this.player = _player;
    }

    public void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        Vector2 targetPosition = (Vector2)transform.position + direction * (moveSpeed * Time.deltaTime);
        
        transform.position = targetPosition;
    }

   


}
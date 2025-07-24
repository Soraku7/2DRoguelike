using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : MonoBehaviour , IPlayerStatsDepdendency
{

    [Header("Elements")]
    private Rigidbody2D rig;
    
    [SerializeField] private MobileJoystick joystick;

    [Header("Settings")]
    [SerializeField] private float baseMoveSpeed;
    private float moveSpeed;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    { 
        rig.linearVelocity = InputManager.instance.GetMoveVector() * (moveSpeed * Time.deltaTime);
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float moveSpeedPercent = playerStatsManager.GetStatValue(Stat.MoveSpeed) / 100f;
        moveSpeed = baseMoveSpeed * (1 + moveSpeedPercent);
    }
}

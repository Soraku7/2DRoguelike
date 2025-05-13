using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : MonoBehaviour
{

    [Header("Elements")]
    private Rigidbody2D rig;
    
    [SerializeField] private MobileJoystick joystick;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rig.linearVelocity = joystick.GetMoveVector() * (moveSpeed * Time.deltaTime);

    }

}

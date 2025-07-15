using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerLevel))]
public class Player : MonoBehaviour
{
    public static Player instance;
    
    [Header("Components")]
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;

    [SerializeField] private CircleCollider2D collider;
    [SerializeField] private SpriteRenderer playerRenderer;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        playerHealth = GetComponent<PlayerHealth>();
        playerLevel = GetComponent<PlayerLevel>();
        
        CharacterSelectionManager.OnCharacterSelected += CharacterSelectedCallback;
    }

    private void OnDestroy()
    {
        CharacterSelectionManager.OnCharacterSelected -= CharacterSelectedCallback;
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public Vector2 GetCenter()
    {
        return (Vector2)transform.position + collider.offset;
    }

    public bool HasLevelUp()
    {
        return playerLevel.HasLevelUp();
    }

    private void CharacterSelectedCallback(CharacterDataSO characterData)
    {
        playerRenderer.sprite = characterData.Sprite;
    }
}

using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    [FormerlySerializedAs("daveCollider")]
    [Header("Collider")]
    [SerializeField] private Collider2D collectableCollider;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Candy candy))
        {
            if (!collider.IsTouching(collectableCollider)) return;
            
            candy.Collect(GetComponent<Player>());
        }
        
        if (collider.TryGetComponent(out Cash cash))
        {
            if (!collider.IsTouching(collectableCollider)) return;
            
            cash.Collect(GetComponent<Player>());
        }
    }
}

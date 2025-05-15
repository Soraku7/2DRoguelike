using UnityEngine;

public class SpritSorter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        spriteRenderer.sortingOrder = Mathf.Abs((int)(transform.position.y * 10));
    }
}

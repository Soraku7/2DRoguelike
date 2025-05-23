using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    [NaughtyAttributes.Button]
    public void Animate()
    {
        damageText.text = Random.Range(0, 1000).ToString();
        animator.Play("Animate");
    }
}

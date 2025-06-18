using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;
    
    public void Animate(string damage , bool isCriticalHit)
    {
        damageText.text = damage;
        
        damageText.color = isCriticalHit ? Color.yellow : Color.white;
        
        animator.Play("Animate");
    }

    [NaughtyAttributes.Button]
    public void TestAnimate()
    {
        animator.Play("Animate");
        Debug.Log("Animate");
    }
}

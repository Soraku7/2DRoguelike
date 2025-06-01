using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D) , typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rig;
    private Collider2D collider;
    private RangeWeapon rangeWeapon;
    
    [Header("Settings")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyLayer;
    private int damage;
    private bool isCriticalHit;
    private Enemy target;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        StartCoroutine(ReleaseCoroutine());
    }
    
    IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(5f);
        
        Destroy(gameObject);
    }

    public void Configure(RangeWeapon rangeWeapon)
    {
        this.rangeWeapon = rangeWeapon;
    }
    
    public void Shoot(int damage , Vector2 direction , bool isCriticalHit)
    {
        Invoke(nameof(Release), 3);
        
        this.damage = damage;
        this.isCriticalHit = isCriticalHit;
        
        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (target != null) return;
        
        if (IsInLayerMask(collider.gameObject, enemyLayer))
        {
            target = collider.GetComponent<Enemy>();
            Debug.Log(target.name);
            CancelInvoke();
            
            Attack(target);
            Release();
        }
    }

    private void Release()
    {
        if (!gameObject.activeSelf) return; 
        rangeWeapon.ReleaseBullet(this);
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(damage , isCriticalHit);
    }

    private bool IsInLayerMask(GameObject colliderGameObject, LayerMask layerMask)
    {
        return (layerMask.value & (1 << colliderGameObject.layer)) != 0;
    }
    
    public void Reload()
    {
        target = null;
        
        rig.linearVelocity = Vector2.zero;
        collider.enabled = true;
    }
}

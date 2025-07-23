using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RangeEnemyAttack))]
public class Zoomby : Enemy
{
    [Header("Elements")] 
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthBarText;
    [SerializeField] private Animator animator;
    
    enum State
    {
        None,
        Idle,
        Moving,
        Attacking
    }
    
    [Header("State Machine")]
    private State state;
    private float timer;
    
    [Header("Idle State")]
    [SerializeField] private float maxIdleDuration;
    private float idleDuration;

    [Header("Moving State")] 
    [SerializeField] private float moveSpeed;
    private Vector2 targetPosition;
    
    [Header("Attack State")]
    private int attackCounter;
    private RangeEnemyAttack attack;
    
    private void Awake()
    {
        state = State.None;
        onSpawnSequenceComplete += SpawnSequenceCompeleteCallback;
        onDamageTaken += DamageTakeCallback;
    }
    
    protected override void Start()
    {
        base.Start();
        attack = GetComponent<RangeEnemyAttack>();
    }

    private void Update()
    {
        ManageState();
    }

    private void OnDestroy()
    {
        onSpawnSequenceComplete -= SpawnSequenceCompeleteCallback;
        onDamageTaken -= DamageTakeCallback;
    }
    
    
    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                ManageIdleState();
                break;
            
            case State.Moving:
                ManageMovingState();
                break;
            
            case State.Attacking:
                ManageAttackingState();
                break;
            
            default:
                break;
        }
    }

    private void ManageAttackingState()
    {
        
    }
    
    private void Attack()
    {
        Vector2 direction = Quaternion.Euler(0, 0, -45 * attackCounter) * Vector2.up;

        attack.InstantShoot(direction);
        attackCounter++;

    }

    private void ManageMovingState()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        Debug.Log("移动");

        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f) StartAttackState();
    }

    private void StartAttackState()
    {
        Debug.Log("开始攻击");
        state = State.Attacking;
        animator.Play("Attack");

        attackCounter = 0;
    }

    private void ManageIdleState()
    {
        timer += Time.deltaTime;

        if (timer >= idleDuration)
        {
            timer = 0;
            StartMovingState();
        }
    }
    
    private void StartMovingState()
    {
        state = State.Moving;
        targetPosition = GetRandomPosition();
        
        animator.Play("Moving");
    }
    
    
    private void DamageTakeCallback(int damage, Vector2 position, bool isCritical)
    {
        UpdateHealthBar();
    }

    private void SpawnSequenceCompeleteCallback()
    {
        healthBar.gameObject.SetActive(true);
        UpdateHealthBar();
        
        SetIdleState();
    }

    private void SetIdleState()
    {
        state = State.Idle;
        
        idleDuration = Random.Range(1f, maxIdleDuration);
    }

    private void UpdateHealthBar()
    {
        healthBar.value = (float)health / maxHealth;
        healthBarText.text = $"{health} / {maxHealth}";
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 targetPosition = Vector2.zero;
        
        targetPosition.x = Random.Range(-Constants.arenaSize.x / 3, Constants.arenaSize.x / 3);
        targetPosition.y = Random.Range(-Constants.arenaSize.y / 3, Constants.arenaSize.y / 3);
        
        Debug.Log("获取随机位置: " + targetPosition);

        return targetPosition;
    }

    public override void PassAway()
    {
        onBossPassedAway?.Invoke(transform.position);
        PassAwayAfterWave();
    }
}

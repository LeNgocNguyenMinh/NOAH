using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemy : MonoBehaviour, IEnemyStandable, ITriggerCheckable, IDamageable
{
    public bool IsInChaseRange { get; set; } = false;
    public bool IsInAttackRange { get; set; } = false;

    [field: SerializeField]public GameObject MainObject { get; set; }
    [field: SerializeField]public float IdleDuration { get; set;}

    public StandingEnemyStateMachine StateMachine { get; set; }
    public StandingEnemyIdleState IdleState { get; set; }
    public StandingEnemyAttackState AttackState { get; set; }
    public StandingEnemyDieState DieState { get; set; }

    public bool IsFacingRight{ get; set;} = true;

    public Animator Animator { get; set; }
    public DropOnDie DropOnDie { get; set; }

    private void Awake()
    {
        StateMachine = new StandingEnemyStateMachine();
        IdleState = new StandingEnemyIdleState(this, StateMachine);
        AttackState = new StandingEnemyAttackState(this, StateMachine);
        DieState = new StandingEnemyDieState(this, StateMachine);
    }
    private void Start()
    {
        DropOnDie = GetComponent<DropOnDie>();
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);   
    }
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }
    public void Die()
    {
        StateMachine.ChangeState(DieState);
    }
    public void DestroyAfterDead()
    {
        Destroy(MainObject);
    }

    public void FlipSprite()
    {
        Transform playerTransform = FindObjectOfType<Player>().transform;
        Vector2 direct = (playerTransform.position - transform.position).normalized;
        if(IsFacingRight && direct.x < 0)
        {
            IsFacingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if(!IsFacingRight && direct.x > 0)
        {
            IsFacingRight = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    public void SetIsInAttackRange(bool value)
    { 
        Debug.Log(value);
        IsInAttackRange = value;
    }

    public void SetIsInChaseRange(bool value)
    {    
    }

    public enum AnimationTriggerType
    {
        IdleAnimFinish,
        DeadAnimFinish,
        AttackAnimFinish,
        PlayerHurt  
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour, IEnemyMoveable , ITriggerCheckable, IDamageable 
{
    public bool IsInChaseRange { get; set; } = false;
    public bool IsInAttackRange { get; set; } = false;
    [field: SerializeField]public GameObject mainObject { get; set; }
    [field: SerializeField]public Rigidbody2D RB { get; set; }
    [field: SerializeField]public float WalkSpeed { get; set;}
    [field: SerializeField]public float WalkDuration { get; set;}
    [field: SerializeField]public float IdleDuration { get; set;}
    [field: SerializeField]public float ChaseSpeed { get; set;}
    [field: SerializeField]public float MoveRange { get; set;}
    [field: SerializeField]public float AttackCoolDown { get; set;}

    public bool IsFacingRight{ get; set;} = true;
    public MovingEnemyStateMachine StateMachine { get; set; }
    public MovingEnemyIdleState IdleState { get; set; }
    public MovingEnemyChaseState ChaseState { get; set; }
    public MovingEnemyAttackState AttackState { get; set; }
    public MovingEnemyDieState DieState { get; set; }
    public MovingEnemyWalkState WalkState { get; set; } 
    
    public Animator Animator { get; set; }
    public DropOnDie DropOnDie { get; set; }

    
    private void Awake()
    {
        StateMachine = new MovingEnemyStateMachine();
        IdleState = new MovingEnemyIdleState(this, StateMachine);
        ChaseState = new MovingEnemyChaseState(this, StateMachine);
        AttackState = new MovingEnemyAttackState(this, StateMachine);
        DieState = new MovingEnemyDieState(this, StateMachine);
        WalkState = new MovingEnemyWalkState(this, StateMachine);
    }
    private void Start()
    {
        DropOnDie = GetComponent<DropOnDie>();
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(WalkState);   
    }
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void FlipSprite(Vector2 velocity)
    {
        if(IsFacingRight && velocity.x < 0)
        {
            IsFacingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if(!IsFacingRight && velocity.x > 0)
        {
            IsFacingRight = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    public void SetIsInChaseRange(bool value)
    {
        IsInChaseRange = value;
    }
    public void SetIsInAttackRange(bool value)
    {
        IsInAttackRange = value;
    }

    public void Move(Vector2 direct, float speedValue)
    {
        RB.velocity = direct * speedValue;
        FlipSprite(RB.velocity);
    }

    public void Stop()
    {
        RB.velocity = Vector2.zero;
    }
    public enum AnimationTriggerType
    {
        WalkAnimFinish,
        DeadAnimFinish,
        AttackAnimFinish,
        IdleAnimFinish,
        PlayerHurt  
    }
    public void Die()
    {
        StateMachine.ChangeState(DieState);
    }
    public void DestroyAfterDead()
    {
        Destroy(mainObject);
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemyIdleState : StandingEnemyState
{
    private float idleTimer;
    private bool finishIdleAnim = false;
    public StandingEnemyIdleState(StandingEnemy enemy, StandingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Idle");
        finishIdleAnim = false;
        idleTimer = enemy.IdleDuration;
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        idleTimer -= Time.deltaTime;
        if(enemy.IsInAttackRange && finishIdleAnim)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(StandingEnemy.AnimationTriggerType triggerType)
    {
        if(triggerType == StandingEnemy.AnimationTriggerType.IdleAnimFinish)
        {
            finishIdleAnim = true;
        }
    }
}

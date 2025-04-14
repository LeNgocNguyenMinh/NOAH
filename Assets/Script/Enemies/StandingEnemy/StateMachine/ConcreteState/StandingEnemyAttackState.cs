using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemyAttackState : StandingEnemyState
{
    private float attackCountDown = 0;
    private bool finishATKAnim = false;
    public StandingEnemyAttackState(StandingEnemy enemy, StandingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Attack");
        finishATKAnim = false;
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(!enemy.IsInAttackRange && finishATKAnim)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
        enemy.FlipSprite();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(StandingEnemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == StandingEnemy.AnimationTriggerType.AttackAnimFinish)
        {
            finishATKAnim = true;
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }
}

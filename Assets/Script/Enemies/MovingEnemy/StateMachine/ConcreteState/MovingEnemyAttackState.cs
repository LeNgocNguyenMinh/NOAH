using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyAttackState : MovingEnemyState
{
    public MovingEnemyAttackState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Attack");
        enemy.Stop();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(MovingEnemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == MovingEnemy.AnimationTriggerType.AttackAnimFinish)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }
}

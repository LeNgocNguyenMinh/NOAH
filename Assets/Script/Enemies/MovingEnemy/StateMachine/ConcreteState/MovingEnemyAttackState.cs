using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyAttackState : MovingEnemyState
{
    private bool finishATKAnim = false;
    public MovingEnemyAttackState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Attack");
        enemy.Stop();
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
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
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
            finishATKAnim = true;
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }
}

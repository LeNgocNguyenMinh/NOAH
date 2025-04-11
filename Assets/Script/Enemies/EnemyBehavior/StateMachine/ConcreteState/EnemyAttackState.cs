using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackCountDown = 0;
    private bool finishATKAnim = false;
    public EnemyAttackState(MovingEnemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
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
        if(finishATKAnim && attackCountDown <=0)
        {
            finishATKAnim = false;
            enemy.Animator.SetTrigger("Attack");
            enemy.Stop();
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

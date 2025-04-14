using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyIdleState : MovingEnemyState
{
    private float idleTimer;
    private bool finishIdleAnim = false;
    public MovingEnemyIdleState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Stop();
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
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(MovingEnemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == MovingEnemy.AnimationTriggerType.IdleAnimFinish && idleTimer <= 0)
        {
            enemy.StateMachine.ChangeState(enemy.WalkState);
        }
    }
}

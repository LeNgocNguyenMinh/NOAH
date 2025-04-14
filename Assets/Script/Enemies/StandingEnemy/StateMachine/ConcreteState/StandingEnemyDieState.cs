using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemyDieState : StandingEnemyState
{
    public StandingEnemyDieState(StandingEnemy enemy, StandingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }  
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Dead");

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
    public override void AnimationTriggerEvent(StandingEnemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == StandingEnemy.AnimationTriggerType.DeadAnimFinish)
        {
            enemy.DropOnDie.DropEXP(enemy.transform.position);
            enemy.DestroyAfterDead();
        }
    }
}

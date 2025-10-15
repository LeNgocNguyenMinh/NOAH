using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyDieState : MovingEnemyState
{
    public MovingEnemyDieState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }  
        public override void EnterState()
    {
        base.EnterState();
        enemy.Stop();
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
    public override void AnimationTriggerEvent(MovingEnemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == MovingEnemy.AnimationTriggerType.DeadAnimFinish)
        {
            enemy.DropOnDie.DropReward();
            enemy.DestroyAfterDead();
        }
    }
}

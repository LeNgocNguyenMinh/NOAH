using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyDieState : MovingEnemyState
{
    private int count = 0;
    public MovingEnemyDieState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }  
    public override void EnterState()
    {
        base.EnterState();
        enemy.Stop();
        enemy.HideUI();
        enemy.DropOnDie.DropReward();
        enemy.Col.enabled = false;
        enemy.Animator.SetTrigger("DeadStart");
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
        if(triggerType == MovingEnemy.AnimationTriggerType.DeadStartAnimFinish)
        {
            enemy.Animator.SetTrigger("DeadFloat");
        }
        if(triggerType == MovingEnemy.AnimationTriggerType.DeadFloatAnimFinish)
        {
            count++;
            if(count == 3)
            {
                enemy.DestroyAfterDead();
            }
        }
    }
}

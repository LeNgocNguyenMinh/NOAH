using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyState 
{
    protected MovingEnemy enemy;
    protected MovingEnemyStateMachine stateMachine;

    public MovingEnemyState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }
    public virtual void EnterState()
    {
        
    }
    public virtual void ExitState()
    {
        
    }
    public virtual void FrameUpdate()
    {
        
    }
    public virtual void PhysicsUpdate()
    {
        
    }
    public virtual void AnimationTriggerEvent(MovingEnemy.AnimationTriggerType triggerType)
    {
        
    }

}

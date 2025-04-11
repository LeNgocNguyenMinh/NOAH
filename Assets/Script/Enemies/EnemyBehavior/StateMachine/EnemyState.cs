using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected MovingEnemy enemy;
    protected EnemyStateMachine stateMachine;

    public EnemyState(MovingEnemy enemy, EnemyStateMachine stateMachine)
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

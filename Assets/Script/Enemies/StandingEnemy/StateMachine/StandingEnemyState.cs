using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemyState 
{
    protected StandingEnemy enemy;
    protected StandingEnemyStateMachine stateMachine;

    public StandingEnemyState(StandingEnemy enemy, StandingEnemyStateMachine stateMachine)
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
    public virtual void AnimationTriggerEvent(StandingEnemy.AnimationTriggerType triggerType)
    {
        
    }
}

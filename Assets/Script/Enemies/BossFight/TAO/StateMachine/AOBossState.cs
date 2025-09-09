using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossState : MonoBehaviour
{
    protected AOBossStateMachine aoBossStateMachine;
    protected AOBoss aoBoss;
    public AOBossState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine)
    {
        this.aoBoss = aoBoss;
        this.aoBossStateMachine = aoBossStateMachine;
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
    public virtual void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
        
    }
}

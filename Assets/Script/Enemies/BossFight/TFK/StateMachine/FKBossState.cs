using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossState : MonoBehaviour
{
    protected FKBossStateMachine aoBossStateMachine;
    protected FKBoss aoBoss;
    public FKBossState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine)
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
    public virtual void AnimationTriggerEvent(FKBoss.AnimationTriggerType triggerType)
    {
        
    }
}

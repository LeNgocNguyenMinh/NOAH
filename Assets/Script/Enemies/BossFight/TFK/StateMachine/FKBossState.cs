using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossState : MonoBehaviour
{
    protected FKBossStateMachine fkBossStateMachine;
    protected FKBoss fkBoss;
    public FKBossState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine)
    {
        this.fkBoss = fkBoss;
        this.fkBossStateMachine = fkBossStateMachine;
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

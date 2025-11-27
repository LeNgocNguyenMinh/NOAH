using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossRestState : FKBossState
{
    public FKBossRestState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossAnimator.SetTrigger("Rest");
        fkBoss.Gate.SetActive(false);
        fkBoss.BossIsAwake = false;        
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(FKBoss.AnimationTriggerType triggerType)
    {
    }
}

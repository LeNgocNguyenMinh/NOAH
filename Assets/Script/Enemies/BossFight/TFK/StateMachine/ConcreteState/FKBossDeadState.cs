using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossDeadState : FKBossState
{
    public FKBossDeadState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossAnimator.SetTrigger("Dead");
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
        if(triggerType == FKBoss.AnimationTriggerType.DeadAnimFinish)
        {
            fkBoss.IsDead = true;
            fkBoss.BossVanish();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossIdleState : FKBossState
{
    public FKBossIdleState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossAnimator.SetTrigger("Idle");
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
        if (triggerType == FKBoss.AnimationTriggerType.IdleAnimFinish)
        {
            fkBoss.StateMachine.ChangeState(fkBoss.ATK2ReadyState);
        }
    }
}

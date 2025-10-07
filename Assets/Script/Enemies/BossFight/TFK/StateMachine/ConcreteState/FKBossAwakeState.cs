using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossAwakeState : FKBossState
{
    public FKBossAwakeState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.FKBossAnimator.SetTrigger("Awake");
        aoBoss.HealthBarCV.SetActive(true);

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
        if (triggerType == FKBoss.AnimationTriggerType.AwakeAnimFinish)
        {
            aoBoss.BossIsAwake = true;
            aoBoss.StateMachine.ChangeState(aoBoss.IdleState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossAwakeState : FKBossState
{
    public FKBossAwakeState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossAnimator.SetTrigger("Awake");
        fkBoss.HealthBarCV.SetActive(true);

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
            fkBoss.BossIsAwake = true;
            fkBoss.StateMachine.ChangeState(fkBoss.IdleState);
        }
    }
}

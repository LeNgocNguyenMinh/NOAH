using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossAwakeState : AOBossState
{
    public AOBossAwakeState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AOBossAnimator.SetTrigger("Awake");
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
    public override void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
        if (triggerType == AOBoss.AnimationTriggerType.AwakeAnimFinish)
        {
            aoBoss.BossIsAwake = true;
            aoBoss.StateMachine.ChangeState(aoBoss.IdleState);
        }
    }
}

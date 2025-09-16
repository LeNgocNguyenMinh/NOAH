using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossIdleState : AOBossState
{
    public AOBossIdleState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AOBossAnimator.SetTrigger("Idle");
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
        if (triggerType == AOBoss.AnimationTriggerType.IdleAnimFinish)
        {
            aoBoss.StateMachine.ChangeState(aoBoss.ATK1ReadyState);
        }
    }
}

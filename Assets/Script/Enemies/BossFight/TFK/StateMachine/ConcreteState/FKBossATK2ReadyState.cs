using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossATK2ReadyState : FKBossState
{
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    public FKBossATK2ReadyState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.FKBossAnimator.SetTrigger("ATK2Ready");
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
        if(triggerType == FKBoss.AnimationTriggerType.ATK2ReadyAnimFinish)
        {
            aoBoss.StateMachine.ChangeState(aoBoss.ATK2IdleState);
        }
    }
}

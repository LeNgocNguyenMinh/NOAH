using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossATK2ReadyState : AOBossState
{
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    public AOBossATK2ReadyState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AOBossAnimator.SetTrigger("ATK2Ready");
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
        if(triggerType == AOBoss.AnimationTriggerType.ATK2ReadyAnimFinish)
        {
            aoBoss.StateMachine.ChangeState(aoBoss.ATK2IdleState);
        }
    }
}

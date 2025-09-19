using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossATK2EndState : AOBossState
{
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    public AOBossATK2EndState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AOBossAnimator.SetTrigger("ATK2End");
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossRestState : AOBossState
{
    public AOBossRestState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.LHandRB.bodyType = RigidbodyType2D.Static;
        aoBoss.RHandRB.bodyType = RigidbodyType2D.Static;
        aoBoss.HeadRB.bodyType = RigidbodyType2D.Static;
        aoBoss.LHandCld.isTrigger = false;
        aoBoss.RHandCld.isTrigger = false;
        aoBoss.HeadCld.isTrigger = false;
        aoBoss.BossIsAwake = false;
        aoBoss.AOBossAnimator.SetTrigger("Rest");
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

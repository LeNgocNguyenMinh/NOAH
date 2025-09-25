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
            aoBoss.LHandRB.bodyType = RigidbodyType2D.Dynamic;
            aoBoss.RHandRB.bodyType = RigidbodyType2D.Dynamic;
            aoBoss.HeadRB.bodyType = RigidbodyType2D.Dynamic;
            aoBoss.LHandCld.isTrigger = true;
            aoBoss.RHandCld.isTrigger = true;
            aoBoss.HeadCld.isTrigger = true;
            aoBoss.StateMachine.ChangeState(aoBoss.ATK1ReadyState);
        }
    }
}

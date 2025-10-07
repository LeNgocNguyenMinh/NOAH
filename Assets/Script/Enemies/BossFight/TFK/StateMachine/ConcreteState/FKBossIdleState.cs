using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossIdleState : FKBossState
{
    public FKBossIdleState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.FKBossAnimator.SetTrigger("Idle");
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

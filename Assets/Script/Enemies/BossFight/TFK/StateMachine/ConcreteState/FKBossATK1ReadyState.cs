using System.Collections;
using UnityEngine;

public class FKBossATK1ReadyState : FKBossState
{
    public FKBossATK1ReadyState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.FKBossAnimator.SetTrigger("ATK1Ready");
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
        if(triggerType == FKBoss.AnimationTriggerType.ATK1ReadyAnimFinish)
        {
            aoBoss.StateMachine.ChangeState(aoBoss.ATK1IdleState);
        }
    }
}

using System.Collections;
using UnityEngine;

public class AOBossATK1ReadyState : AOBossState
{
    public AOBossATK1ReadyState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AOBossAnimator.SetTrigger("ATK1Ready");
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
        if(triggerType == AOBoss.AnimationTriggerType.ATK1ReadyAnimFinish)
        {
            aoBoss.StateMachine.ChangeState(aoBoss.ATK1IdleState);
        }
    }
}

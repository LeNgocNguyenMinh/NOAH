using System.Collections;
using UnityEngine;

public class FKBossATK1ReadyState : FKBossState
{
    public FKBossATK1ReadyState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossAnimator.SetTrigger("ATK1Ready");
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
        if (triggerType == FKBoss.AnimationTriggerType.ATK1ReadyAnimFinish)
        {
            fkBoss.StateMachine.ChangeState(fkBoss.ATK1AttackState);
        }
    }
}

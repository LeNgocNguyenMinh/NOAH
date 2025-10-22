using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossATK2IdleState : FKBossState
{
    private int count;
    private float delayCount;
    public FKBossATK2IdleState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        count = fkBoss.ATK2IdleTime;
        fkBoss.FKBossAnimator.SetTrigger("ATK2Idle");
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
        if (triggerType == FKBoss.AnimationTriggerType.ATK2IdleAnimFinish)
        {
            if(count < 0)
            {
                fkBoss.StateMachine.ChangeState(fkBoss.ATK2AttackState);
            }
            else
            {
                count --;
            }
        }
    }
}

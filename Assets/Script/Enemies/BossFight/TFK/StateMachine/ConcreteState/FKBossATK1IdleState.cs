using System.Collections;
using UnityEngine;

public class FKBossATK1IdleState : FKBossState
{
    private int timer;
    public FKBossATK1IdleState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        timer = fkBoss.ATK1IdleTime;
        fkBoss.FKBossAnimator.SetTrigger("ATK1Idle");
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
        if (triggerType == FKBoss.AnimationTriggerType.ATK1IdleAnimFinish)
        {
            if(timer < 0)
            {
                fkBoss.StateMachine.ChangeState(fkBoss.ATK1AttackState);
            }
            else
            {
                timer --;
            }
        }
    }
}

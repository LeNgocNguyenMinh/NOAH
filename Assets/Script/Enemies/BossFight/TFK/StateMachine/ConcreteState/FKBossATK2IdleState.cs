using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossATK2IdleState : FKBossState
{
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    private int atkCount;
    private float delayCount;
    public FKBossATK2IdleState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
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
    }
}

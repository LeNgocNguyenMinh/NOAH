using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossATK2EndState : FKBossState
{
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    public FKBossATK2EndState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
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

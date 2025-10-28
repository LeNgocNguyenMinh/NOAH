using UnityEngine;
using DG.Tweening;
public class FKBossATK1EndState : FKBossState
{
    Vector3 rhDirect;
    public FKBossATK1EndState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.StateMachine.ChangeState(fkBoss.ATK2ReadyState);
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

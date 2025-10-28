using System.Collections;
using UnityEngine;

public class FKBossATK2AttackState : FKBossState
{
    private float count;
    public FKBossATK2AttackState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossAnimator.SetTrigger("ATK2Attack");
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
        if(triggerType == FKBoss.AnimationTriggerType.ATK2RHShoot && (fkBoss.ATKTestType == FKBoss.TestType.ATK2RH || fkBoss.ATKTestType == FKBoss.TestType.AllATK))
        {
            Instantiate(fkBoss.ATK2RHBullet, fkBoss.ATK2RHShootPoint.position, Quaternion.identity).GetComponent<FKBossATK2RHBullet>().SetValue(fkBoss.ATK2RHBulletMaxSpeed, fkBoss.ATK2RHBulletMaxHeight);
        }
        if(triggerType == FKBoss.AnimationTriggerType.ATK2LHAttack && (fkBoss.ATKTestType == FKBoss.TestType.ATK2LH || fkBoss.ATKTestType == FKBoss.TestType.AllATK))
        {
            Instantiate(fkBoss.ATK2LHObject, fkBoss.ATK2LHStartPoint.position, Quaternion.identity).GetComponent<FKBossATK2LHRoute>().SetValue(fkBoss.ATK2LHStartPoint.position, fkBoss.ATK2LHSpawnDelay, fkBoss.ATK2LHDisBet);
        }
        if(triggerType == FKBoss.AnimationTriggerType.ATK2AttackAnimFinish)
        {
            if(fkBoss.ATK2Count < fkBoss.ATK2AtttackNum)
            {
                fkBoss.ATK2Count ++;
                fkBoss.StateMachine.ChangeState(fkBoss.ATK2IdleState);
            }
            else
            {
                fkBoss.ATK2Count = 0;
                fkBoss.StateMachine.ChangeState(fkBoss.ATK2EndState);
            }
        }
    }
}

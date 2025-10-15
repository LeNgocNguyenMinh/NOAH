using System.Collections;
using UnityEngine;

public class FKBossATK1AttackState : FKBossState
{
    private float coolDown;

    public FKBossATK1AttackState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossAnimator.SetTrigger("ATK1Attack");
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
        if(triggerType == FKBoss.AnimationTriggerType.ATK1RHThrow)
        {
            Instantiate(fkBoss.Banana, fkBoss.RHThrowPoint.position, Quaternion.identity).GetComponent<FKBossATK1RHBullet>().SetValue(fkBoss.ATK1RHFlyTime, fkBoss.ATK1RhRotateSpeed);
        }
        else if(triggerType == FKBoss.AnimationTriggerType.ATK1LHThrow)
        {
            Instantiate(fkBoss.WaterMelon, fkBoss.LHThrowPoint.position, Quaternion.identity).GetComponent<FKBossATK1LHBigBullet>().SetValue(fkBoss.ATK1LHWMSpeed, fkBoss.ATK1LHWMFlyDist, fkBoss.ATK1LHWMPSpeed, fkBoss.ATK1LHWMPFlyDist, fkBoss.BossStatus.bossDamage, fkBoss.ATK1RtSpeed);
        }
        if(triggerType == FKBoss.AnimationTriggerType.ATK1AttackAnimFinish)
        {
            if(fkBoss.ATK1Count < fkBoss.ATK1AtttackNum)
            {
                fkBoss.ATK1Count ++;
                fkBoss.StateMachine.ChangeState(fkBoss.ATK1IdleState);
            }
            else
            {
                fkBoss.ATK1Count = 0;
                fkBoss.StateMachine.ChangeState(fkBoss.ATK1EndState);
            }
        }
    }
}

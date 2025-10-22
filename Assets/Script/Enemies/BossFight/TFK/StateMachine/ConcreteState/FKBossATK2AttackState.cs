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
        if(triggerType == FKBoss.AnimationTriggerType.ATK2RHShoot && count <= 0)
        {
            count = fkBoss.ATK2IdleTime;    
            Instantiate(fkBoss.ATK2RHBullet, fkBoss.ATK2RHShootPoint.position, Quaternion.identity).GetComponent<FKBossATK2RHBullet>().SetValue(fkBoss.ATK2RHBulletMaxSpeed, fkBoss.ATK2RHBulletMaxHeight);
        }
        /* else if(triggerType == FKBoss.AnimationTriggerType.ATK2LHAttack)
        {
            fkBoss.ATK2LHAttack();
        } */
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

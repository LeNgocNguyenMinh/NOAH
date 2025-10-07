using System.Collections;
using UnityEngine;

public class FKBossATK1AttackState : FKBossState
{
    private float timer = 0f;
    private Vector3 readyPos;
    private bool hasHitGround;
    public FKBossATK1AttackState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AttackCount++;
        timer = aoBoss.ATK1RHStayTime;
        readyPos = aoBoss.ATK1RHDirect;
        hasHitGround = false;
        aoBoss.FKBossAnimator.SetTrigger("ATK1RHGoDown");
        aoBoss.ATK1RHBox.enabled = true;
        LHandBulletSpawn();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer -= Time.deltaTime;
        aoBoss.RightHand.position = 
        Vector3.MoveTowards(aoBoss.RightHand.position, readyPos, aoBoss.ATK1RHFallSpeed * Time.deltaTime);
        if(Vector3.Distance(aoBoss.RightHand.position, readyPos) <= 0.1f && !hasHitGround)
        {
            hasHitGround = true;
            aoBoss.FKBossAnimator.SetTrigger("ATK1RHHitGround");
            aoBoss.RightHand.position = readyPos;
        }
        if(timer <= 0)
        {
            aoBoss.StateMachine.ChangeState(aoBoss.ATK1IdleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(FKBoss.AnimationTriggerType triggerType)
    {
        
    }
    public void LHandBulletSpawn()
    {
        Instantiate(aoBoss.ATK1LHBulletPref, aoBoss.ATK1LHShootPos.position, Quaternion.identity)
        .GetComponent<FKBossATK1LHBullet>().SetValue(aoBoss.ATK1LHBulletSpeed, aoBoss.ATK1LHBulletTime, aoBoss.BossStatus.bossDamage);
    }
}

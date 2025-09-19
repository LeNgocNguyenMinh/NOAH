using System.Collections;
using UnityEngine;

public class AOBossATK1AttackState : AOBossState
{
    private float timer = 0f;
    private Vector3 readyPos;
    private bool hasHitGround;
    public AOBossATK1AttackState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AttackCount++;
        timer = aoBoss.ATK1RHStayTime;
        readyPos = aoBoss.ATK1RHDirect;
        hasHitGround = false;
        aoBoss.AOBossAnimator.SetTrigger("ATK1RHGoDown");
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
            aoBoss.AOBossAnimator.SetTrigger("ATK1RHHitGround");
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
    public override void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
        
    }
    public void LHandBulletSpawn()
    {
        Debug.Log("dfdfsfsdfsdfsdfsdfsdfsdfsdfsdfsdfsd");
        Instantiate(aoBoss.ATK1LHBulletPref, aoBoss.ATK1LHShootPos[0].position, Quaternion.identity)
        .GetComponent<AOBossATK1LHBullet>().SetValue(aoBoss.ATK1LHBulletSpeed, aoBoss.ATK1LHBulletTime);
        Instantiate(aoBoss.ATK1LHBulletPref, aoBoss.ATK1LHShootPos[1].position, Quaternion.identity)
        .GetComponent<AOBossATK1LHBullet>().SetValue(aoBoss.ATK1LHBulletSpeed, aoBoss.ATK1LHBulletTime);
    }
}

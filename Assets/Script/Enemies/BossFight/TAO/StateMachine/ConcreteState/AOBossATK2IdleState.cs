using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossATK2IdleState : AOBossState
{
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    private int atkCount;
    private float delayCount;
    public AOBossATK2IdleState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        delayCount = 0.5f;
        atkCount = 0;
        aoBoss.AOBossAnimator.SetTrigger("ATK2Idle");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        delayCount -=  Time.deltaTime;
        if(delayCount <=0 && atkCount <= aoBoss.ATK2MaxAtk) 
        {
            delayCount = aoBoss.ATK2AtkDelay;
            atkCount++;
            rhDirect = (Player.Instance.transform.position - aoBoss.ATK2RHShootPos.position).normalized;
            lhDirect = (Player.Instance.transform.position - aoBoss.ATK2LHShootPos.position).normalized;
            RHandBulletSpawn();
            LHandBulletSpawn();
        }
        if(atkCount > aoBoss.ATK2MaxAtk)
        {
            aoBoss.StateMachine.ChangeState(aoBoss.ATK2EndState);
        }
    }
    public override void ExitState()
    {
        /* StopAllCoroutines(); */
        base.ExitState();
    }
    public override void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
    }
    public void RHandBulletSpawn()
    {
        Instantiate(aoBoss.ATK2RHBulletPref, aoBoss.ATK2RHShootPos.position, Quaternion.identity).GetComponent<AOBossATK2RHBullet>().SetValue(rhDirect, aoBoss.ATK2RHBulletSpeed, aoBoss.ATK2RHBoundLimit);
    }
    public void LHandBulletSpawn()
    {
        Instantiate(aoBoss.ATK2LHBulletPref, aoBoss.ATK2LHShootPos.position, Quaternion.identity).GetComponent<AOBossATK2RHBullet>().SetValue(lhDirect, aoBoss.ATK2LHBulletSpeed, aoBoss.ATK2LHBoundLimit);
    }
    /* private IEnumerator RHandLoop()
    {
        yield return new WaitForSeconds(1f); // delay ban đầu
        while (true)
        {
            atkCount++;
            if(atkCount > aoBoss.ATK2MaxAtk)
            {
                aoBoss.StateMachine.ChangeState(aoBoss.ATK2EndState);
                yield break;
            }
            RHandBulletSpawn();
            yield return new WaitForSeconds(2f); // lặp lại
        }
    }
    private IEnumerator LHandLoop()
    {
        yield return new WaitForSeconds(1f); // delay ban đầu
        while (true)
        {
            LHandBulletSpawn();
            yield return new WaitForSeconds(2f); // lặp lại
        }
    } */
}

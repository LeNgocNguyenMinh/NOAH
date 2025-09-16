using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossATK2State : AOBossState
{
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    public AOBossATK2State(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.AOBossAnimator.SetTrigger("ATK2");
        aoBoss.StartCoroutine(RHandLoop());
        aoBoss.StartCoroutine(LHandLoop());
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        rhDirect = (Player.Instance.transform.position - aoBoss.ATK2RHShootPos.position).normalized;
        lhDirect = (Player.Instance.transform.position - aoBoss.ATK2LHShootPos.position).normalized;
    }
    public override void ExitState()
    {
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
    private IEnumerator RHandLoop()
    {
        yield return new WaitForSeconds(1f); // delay ban đầu
        while (true)
        {
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
    }
}

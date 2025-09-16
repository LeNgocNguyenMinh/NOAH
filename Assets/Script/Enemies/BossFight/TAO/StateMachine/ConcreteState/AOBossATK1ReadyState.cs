using System.Collections;
using UnityEngine;

public class AOBossATK1ReadyState : AOBossState
{
    private float timer = 0f;
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    private GameObject atk1Site;
    private Vector2 size;
    private Vector3 readyPos;
    public AOBossATK1ReadyState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        timer = aoBoss.ATK1RHReadyTime;
        aoBoss.AOBossAnimator.SetTrigger("ATK1");
        aoBoss.StartCoroutine(LHandLoop());
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer -= Time.deltaTime;
        lhDirect = (Player.Instance.transform.position - aoBoss.LeftHand.position).normalized;
        if(timer <= 0)
        {
            aoBoss.ATK1RHDirect = new Vector3(readyPos.x + 2f, readyPos.y - 5f, readyPos.z);
            aoBoss.StateMachine.ChangeState(aoBoss.ATK1State);
        }
        else{
            readyPos = Player.Instance.transform.position;
            readyPos = new Vector3(readyPos.x + 2f, readyPos.y + 5f, readyPos.z);
            rhDirect = (readyPos - aoBoss.RightHand.position).normalized;
            if(Vector3.Distance(aoBoss.RightHand.position, readyPos) > 0.1f)
            {
                aoBoss.RightHand.position = 
                Vector3.MoveTowards(aoBoss.RightHand.position, readyPos, aoBoss.ATK1RHFlySpeed * Time.deltaTime);
            }
            else
            {
                aoBoss.RightHand.position = readyPos;
            }
        }
        float lAngle = Mathf.Atan2(lhDirect.y, lhDirect.x) * Mathf.Rad2Deg;
        aoBoss.LeftHand.rotation = Quaternion.Euler(0f, 0f, lAngle);
    }

    public void LHandBulletSpawn()
    {
        Instantiate(aoBoss.ATK1LHBulletPref, aoBoss.ATK1LHShootPos[0].position, Quaternion.identity)
        .GetComponent<AOBossATK1LHBullet>().SetValue(aoBoss.ATK1LHBulletSpeed, aoBoss.ATK1LHBulletTime);
        Instantiate(aoBoss.ATK1LHBulletPref, aoBoss.ATK1LHShootPos[1].position, Quaternion.identity)
        .GetComponent<AOBossATK1LHBullet>().SetValue(aoBoss.ATK1LHBulletSpeed, aoBoss.ATK1LHBulletTime);
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
    }
    private IEnumerator LHandLoop()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            LHandBulletSpawn();
            yield return new WaitForSeconds(aoBoss.ATK1LHBulletTime);
        }
    }
}

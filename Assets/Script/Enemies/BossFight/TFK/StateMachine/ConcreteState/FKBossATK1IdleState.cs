using System.Collections;
using UnityEngine;

public class FKBossATK1IdleState : FKBossState
{
    private float timer = 0f;
    private Vector3 lhDirect;
    private Vector3 readyPos;
    public FKBossATK1IdleState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        timer = aoBoss.ATK1RHReadyTime;
        aoBoss.FKBossAnimator.SetTrigger("ATK1Idle");
        aoBoss.ATK1RHBox.enabled = false;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(timer>0)
        {
            timer -= Time.deltaTime;
        }
        lhDirect = (Player.Instance.transform.position - aoBoss.LeftHand.position).normalized;
        float lAngle = Mathf.Atan2(lhDirect.y, lhDirect.x) * Mathf.Rad2Deg;
        aoBoss.LeftHand.rotation = Quaternion.Euler(0f, 0f, lAngle);
        if(timer <= 0)
        {
            if(aoBoss.AttackCount > aoBoss.ATK1MaxAtk)
            {
                aoBoss.AttackCount = 0;
                aoBoss.StateMachine.ChangeState(aoBoss.ATK1EndState);
            }
            else
            {
                aoBoss.ATK1RHDirect = new Vector3(readyPos.x, readyPos.y - 5f, readyPos.z);
                aoBoss.StateMachine.ChangeState(aoBoss.ATK1AttackState);
            }
        }
        else{
            readyPos = Player.Instance.transform.position;
            readyPos = new Vector3(readyPos.x + 1.5f, readyPos.y + 5f, readyPos.z);
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
    }

    
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(FKBoss.AnimationTriggerType triggerType)
    {
    }
}

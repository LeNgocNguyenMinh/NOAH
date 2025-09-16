using System.Collections;
using UnityEngine;

public class AOBossATK1State : AOBossState
{
    private float timer = 0f;
    private Vector3 readyPos;
    private bool hasHitGround;
    public AOBossATK1State(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        timer = aoBoss.ATK1RHStayTime;
        readyPos = aoBoss.ATK1RHDirect;
        hasHitGround = false;
        aoBoss.AOBossAnimator.SetTrigger("ATK1RHGoDown");
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
            aoBoss.StateMachine.ChangeState(aoBoss.ATK1ReadyState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
        
    }
}

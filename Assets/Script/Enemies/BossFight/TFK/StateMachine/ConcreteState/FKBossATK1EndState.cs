using UnityEngine;
using DG.Tweening;
public class FKBossATK1EndState : FKBossState
{
    Vector3 rhDirect;
    public FKBossATK1EndState(FKBoss aoBoss, FKBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.FKBossAnimator.SetTrigger("ATK1End");
        aoBoss.ATK1RHBox.enabled = false;
        aoBoss.LeftHand.DORotate(Vector3.zero, 0.5f, RotateMode.Fast);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(Vector3.Distance(aoBoss.RightHand.position, aoBoss.RightHandOriginTrans) > 0.1f)
        {
            aoBoss.RightHand.position = 
            Vector3.MoveTowards(aoBoss.RightHand.position, aoBoss.RightHandOriginTrans, 10f * Time.deltaTime);
        }
        else
        {
            aoBoss.RightHand.position = aoBoss.RightHandOriginTrans;
            aoBoss.StateMachine.ChangeState(aoBoss.ATK2ReadyState);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossAwakeState : AOBossState
{
    public AOBossAwakeState(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        aoBoss.BossCounterUI.SetActive(true);
        UIMouseAndPriority.Instance.canOpenUI = false;
        aoBoss.BossCounterUI.GetComponent<Animator>().SetTrigger("BossCounter");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(aoBoss.BossCounterUI.GetComponent<BossCounterUI>().GetAnimFinish())
        {
            aoBoss.BossCounterUI.GetComponent<BossCounterUI>().SetAnimFinishFalse();
            UIMouseAndPriority.Instance.canOpenUI = false;
            aoBoss.AOBossAnimator.SetTrigger("Awake");
            aoBoss.InFightGate.SetActive(true);
            aoBoss.HealthBarCV.SetActive(true);
            aoBoss.BossCounterUI.SetActive(false);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
        if (triggerType == AOBoss.AnimationTriggerType.AwakeAnimFinish)
        {
            aoBoss.BossIsAwake = true;
            aoBoss.StateMachine.ChangeState(aoBoss.IdleState);
        }
    }
}

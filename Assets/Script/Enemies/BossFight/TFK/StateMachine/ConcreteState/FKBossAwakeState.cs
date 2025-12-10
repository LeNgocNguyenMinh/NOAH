using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossAwakeState : FKBossState
{
    public FKBossAwakeState(FKBoss fkBoss, FKBossStateMachine fkBossStateMachine) : base(fkBoss, fkBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        fkBoss.FKBossStatusController.BossUpdateInfo();
        fkBoss.BossCounterUI.SetActive(true);
        UIMouseAndPriority.Instance.canOpenUI = false;
        fkBoss.BossCounterUI.GetComponent<Animator>().SetTrigger("BossCounter");

    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(fkBoss.BossCounterUI.GetComponent<BossCounterUI>().GetAnimFinish())
        {
            fkBoss.BossCounterUI.GetComponent<BossCounterUI>().SetAnimFinishFalse();
            UIMouseAndPriority.Instance.canOpenUI = true;
            fkBoss.FKBossAnimator.SetTrigger("Awake");
            fkBoss.InFightGate.SetActive(true);
            fkBoss.HealthBarCV.SetActive(true);
            fkBoss.BossCounterUI.SetActive(false);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(FKBoss.AnimationTriggerType triggerType)
    {
        if (triggerType == FKBoss.AnimationTriggerType.AwakeAnimFinish)
        {
            fkBoss.BossIsAwake = true;
            fkBoss.StateMachine.ChangeState(fkBoss.IdleState);
        }
    }
}

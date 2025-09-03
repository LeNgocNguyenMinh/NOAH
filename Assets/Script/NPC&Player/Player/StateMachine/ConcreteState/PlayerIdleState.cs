using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter Idle State");
        player.PlayerAnimator.SetTrigger("Idle");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.FacingDirection();
        player.CheckDashCoolDown();
        if(player.GetMoveDirect() != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.WalkState);
        }
        player.RB.velocity = Vector2.zero;
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit Idle State");
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if (triggerType == Player.AnimationTriggerType.IdleAnimFinish) {
           
        }
    }
}

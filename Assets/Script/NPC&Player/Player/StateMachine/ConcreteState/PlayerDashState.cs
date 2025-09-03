using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter Dash State");
        player.GetMoveDirect();
        player.FacingDirection();
        player.PlayerAnimator.SetTrigger("Dash");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.RB.velocity = player.MoveDirect * player.DashSpeed;
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit Dash State");
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if (triggerType == Player.AnimationTriggerType.DashAnimFinish) {
           player.DashCoolCounter = player.DashCoolDown;
           playerStateMachine.ChangeState(player.IdleState);
        }
    }
}

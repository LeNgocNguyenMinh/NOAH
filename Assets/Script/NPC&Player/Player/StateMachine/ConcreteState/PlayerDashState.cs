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
        player.GetMoveDirect();
        player.FacingDirection();
        player.PlayerAnimator.SetTrigger("Dash");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(!player.GotHit)
        {
            player.RB.velocity = player.MoveDirect * player.DashSpeed;
        }
        
    }
    public override void ExitState()
    {
        base.ExitState();
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

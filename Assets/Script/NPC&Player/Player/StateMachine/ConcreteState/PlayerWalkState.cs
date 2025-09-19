using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        player.PlayerAnimator.SetTrigger("Walk");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(player.GetMoveDirect() == Vector2.zero)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
        player.FacingDirection();
        player.CheckDashCoolDown();
        player.RB.velocity = player.MoveDirect * player.WalkSpeed;
        player.CheckDashButton();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if (triggerType == Player.AnimationTriggerType.WalkAnimFinish) {
          
        }
    }
}

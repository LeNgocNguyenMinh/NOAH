using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        player.PlayerAnimator.SetTrigger("Dead");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if (triggerType == Player.AnimationTriggerType.DeadAnimFinish) {
            PauseMenu.Instance.PauseMenuPanelShow(isDead: true);
        }
    }
}

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
        Debug.Log("Enter Dead State");
        player.PlayerAnimator.SetTrigger("Dead");
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit Dead State");
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if (triggerType == Player.AnimationTriggerType.DeadAnimFinish) {
            /* PauseMenu.Instance.GameOverMenuPanelShow(); */
        }
    }
}

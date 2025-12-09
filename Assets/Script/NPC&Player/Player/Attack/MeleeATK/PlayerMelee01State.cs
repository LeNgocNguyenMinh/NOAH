using UnityEngine;

public class PlayerMelee01State : PlayerMeleeState
{
    private bool canMelee;
    public PlayerMelee01State(PlayerMeleeATK playerMeleeATK, PlayerMeleeStateMachine playerMeleeStateMachine) 
        : base(playerMeleeATK, playerMeleeStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        canMelee = true;
    }

    public override void ExitState()
    {
        base.ExitState();
        canMelee = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(Input.GetMouseButtonDown(1) && canMelee)
        {
            canMelee = false;
            PlayerWeaponParent.Instance.ActiveMeleeATK();
            SoundControl.Instance.PlayerMeleeSoundPlay();
            playerMeleeATK.animator.SetTrigger("Melee01");
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void AnimationTriggerEvent(PlayerMeleeATK.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == PlayerMeleeATK.AnimationTriggerType.ATK1AnimFinish)
        {
            playerMeleeATK.StateMachine.ChangeState(playerMeleeATK.MeleeState02);
        }
    }
}

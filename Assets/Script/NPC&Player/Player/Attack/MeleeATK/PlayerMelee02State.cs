using UnityEngine;

public class PlayerMelee02State : PlayerMeleeState
{
    private float countDown;
    private bool canMelee;
    public PlayerMelee02State(PlayerMeleeATK playerMeleeATK, PlayerMeleeStateMachine playerMeleeStateMachine) 
        : base(playerMeleeATK, playerMeleeStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        canMelee = true;
        countDown = PlayerWeaponParent.Instance.delayMeleeCount;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else
        {
            playerMeleeATK.StateMachine.ChangeState(playerMeleeATK.MeleeState01);
        }
        if(Input.GetMouseButtonDown(1) && canMelee)
        {
            canMelee = false;
            PlayerWeaponParent.Instance.ActiveMeleeATK();
            SoundControl.Instance.PlayerMeleeSoundPlay();
            playerMeleeATK.animator.SetTrigger("Melee02");
        }
            
    }

    public override void PhysicsUpdate()
    {
    }

    public override void AnimationTriggerEvent(PlayerMeleeATK.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == PlayerMeleeATK.AnimationTriggerType.ATK2AnimFinish)
        {
            playerMeleeATK.StateMachine.ChangeState(playerMeleeATK.MeleeState01);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : MonoBehaviour
{
    protected PlayerMeleeATK playerMeleeATK;
    protected PlayerMeleeStateMachine playerMeleeStateMachine;

    public PlayerMeleeState(PlayerMeleeATK playerMeleeATK, PlayerMeleeStateMachine playerMeleeStateMachine)
    {
        this.playerMeleeATK = playerMeleeATK;
        this.playerMeleeStateMachine = playerMeleeStateMachine;
    }
    public virtual void EnterState()
    {

    }
    public virtual void ExitState()
    {

    }
    public virtual void FrameUpdate()
    {
        
    }
    public virtual void PhysicsUpdate()
    {
        
    }
    public virtual void AnimationTriggerEvent(PlayerMeleeATK.AnimationTriggerType triggerType)
    {
        
    }

}

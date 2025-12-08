using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeStateMachine : MonoBehaviour
{
    public PlayerMeleeState CurrentState { get; private set; }
    public void Initialize(PlayerMeleeState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void ChangeState(PlayerMeleeState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        /* Debug.Log("State changed to: " + CurrentState.GetType().Name); */
        CurrentState.EnterState();
    }
}

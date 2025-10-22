using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void ChangeState(PlayerState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        /* Debug.Log("State changed to: " + CurrentState.GetType().Name); */
        CurrentState.EnterState();
    }
}

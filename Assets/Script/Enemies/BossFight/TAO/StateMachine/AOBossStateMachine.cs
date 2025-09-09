using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossStateMachine : MonoBehaviour
{
    public AOBossState CurrentState { get; private set; }
    public void Initialize(AOBossState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void ChangeState(AOBossState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        Debug.Log("State changed to: " + CurrentState.GetType().Name);
        CurrentState.EnterState();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKBossStateMachine : MonoBehaviour
{
    public FKBossState CurrentState { get; private set; }
    public void Initialize(FKBossState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void ChangeState(FKBossState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        Debug.Log("FK Boss State changed to: " + CurrentState.GetType().Name);
        CurrentState.EnterState();
    }
}

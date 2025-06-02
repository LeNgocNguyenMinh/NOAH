using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    
    public BossState CurrentBossState { get; private set; }
    public void Initialize(BossState startingState)
    {
        CurrentBossState = startingState;
        CurrentBossState.EnterState();
    }
    public void ChangeState(BossState newState)
    {
        CurrentBossState.ExitState();
        CurrentBossState = newState;
        Debug.Log("State changed to: " + CurrentBossState.GetType().Name);
        CurrentBossState.EnterState();
    }
}

using UnityEngine;

public class MovingEnemyStateMachine 
{
    public MovingEnemyState CurrentEnemyState { get; private set; }
    public void Initialize(MovingEnemyState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }
    public void ChangeState(MovingEnemyState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}

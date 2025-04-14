using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyWalkState : MovingEnemyState
{
    private Vector3 targetPos;
    private Vector3 direction;
    private float walkCount;

    public MovingEnemyWalkState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Walk");
        targetPos = GetRandomPointInCircle();
        walkCount = enemy.WalkDuration;
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(enemy.IsInChaseRange)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
        direction = (targetPos - enemy.transform.position).normalized; 
        enemy.Move(direction, enemy.WalkSpeed);
        walkCount -= Time.deltaTime;
        if(walkCount <= 0 || (targetPos - enemy.transform.position).magnitude < 0.1f)
        {
            targetPos = GetRandomPointInCircle();
            walkCount = enemy.WalkDuration;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.MoveRange;
    }
}

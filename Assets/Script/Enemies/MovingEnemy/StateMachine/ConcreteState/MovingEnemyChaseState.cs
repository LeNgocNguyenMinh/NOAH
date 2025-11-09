using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyChaseState : MovingEnemyState
{
    private Vector3 direction;
    public MovingEnemyChaseState(MovingEnemy enemy, MovingEnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Walk");
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        direction = (Player.Instance.transform.position - enemy.transform.position).normalized; 
        enemy.Move(direction, enemy.ChaseSpeed);
        if(enemy.IsInAttackRange)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
        if(!enemy.IsInChaseRange)
        {
            enemy.StateMachine.ChangeState(enemy.WalkState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

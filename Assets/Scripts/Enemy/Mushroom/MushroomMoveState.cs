using UnityEngine;

public class MushroomMoveState : MushroomGroundedState
{
    public MushroomMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.facingDir * enemy.moveSpeed, rb.linearVelocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();

            stateMachine.ChangeState(enemy.idleState);
        }
    }
}

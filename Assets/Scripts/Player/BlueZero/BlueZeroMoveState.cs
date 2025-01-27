using UnityEngine;
using UnityEngine.Windows;

public class BlueZeroMoveState : PlayerState
{
    private BlueZero player;
    public BlueZeroMoveState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, BlueZero _player) : base(_playerBase, _stateMachine, _animBoolName)
    {
        this.player = _player;
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

        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);

        if (xInput == 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}

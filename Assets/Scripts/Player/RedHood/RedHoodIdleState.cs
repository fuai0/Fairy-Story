using UnityEngine;

public class RedHoodIdleState : RedHoodGroundedState
{
    public RedHoodIdleState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
        {
            player.stateMachine.ChangeState(player.moveState);
        }
    }
}

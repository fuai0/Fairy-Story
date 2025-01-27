using UnityEngine;

public class BlueZeroIdleState : PlayerState
{
    private BlueZero player;
    public BlueZeroIdleState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, BlueZero _player) : base(_playerBase, _stateMachine, _animBoolName)
    {
        this.player = _player;
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

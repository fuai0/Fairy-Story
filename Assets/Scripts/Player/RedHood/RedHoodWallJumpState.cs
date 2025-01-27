using UnityEngine;

public class RedHoodWallJumpState : PlayerState
{
    private RedHood player;
    public RedHoodWallJumpState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName)
    {
        this.player = _player;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

using UnityEngine;

public class RedHoodAirState : PlayerState
{
    private RedHood player;
    public RedHoodAirState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName)
    {
        this.player = _player;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        AudioManager.instance.PlaySfx(5);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() && player.canWallSlide)
        {
            stateMachine.ChangeState(player.wallSlide);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * xInput * .9f, rb.linearVelocity.y);
        }
    }
}

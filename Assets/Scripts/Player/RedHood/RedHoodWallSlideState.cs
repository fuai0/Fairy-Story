using UnityEngine;

public class RedHoodWallSlideState : PlayerState
{
    private RedHood player;
    public RedHoodWallSlideState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName)
    {
        this.player = _player;
    }

    public override void Enter()
    {
        base.Enter();

        player.sr.flipX = true;
    }

    public override void Exit()
    {
        base.Exit();

        player.sr.flipX = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }

        if ((xInput != 0) && (player.facingDir != xInput))
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (yInput < 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y * .7f);
        }


        if (!player.IsWallDetected())
            stateMachine.ChangeState(player.airState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}

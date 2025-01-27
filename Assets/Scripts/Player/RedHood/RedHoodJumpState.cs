using UnityEngine;

public class RedHoodJumpState : PlayerState
{
    private RedHood player;
    public RedHoodJumpState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName)
    {
        this.player = _player;
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}

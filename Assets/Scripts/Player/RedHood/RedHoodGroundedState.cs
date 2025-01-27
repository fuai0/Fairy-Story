using UnityEngine;

public class RedHoodGroundedState : PlayerState
{
    protected RedHood player;
    public RedHoodGroundedState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName)
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

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.attackState);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            stateMachine.ChangeState(player.shootState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
    }
}

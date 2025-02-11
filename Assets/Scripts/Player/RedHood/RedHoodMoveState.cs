using UnityEngine;

public class RedHoodMoveState : RedHoodGroundedState
{
    public RedHoodMoveState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySfx(3);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSfx(3);
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

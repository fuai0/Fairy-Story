using UnityEngine;

public class RedHoodShootState : RedHoodGroundedState
{
    public RedHoodShootState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.sr.flipX = true;
    }

    public override void Exit()
    {
        base.Exit();

        player.attackFinish = false;
        player.sr.flipX = false;
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        if (player.attackFinish)
            stateMachine.ChangeState(player.idleState);
    }
}

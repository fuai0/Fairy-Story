using UnityEngine;

public class RedHoodHittedState : PlayerState
{
    private RedHood player;
    public RedHoodHittedState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName)
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

        player.hitted = false;
        player.hittedFinish = false;
    }

    public override void Update()
    {
        base.Update();

        if (player.hittedFinish)
            stateMachine.ChangeState(player.idleState);
    }
}

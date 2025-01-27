using UnityEngine;

public class RedHoodAttackState : PlayerState
{
    public int comboCounter { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 2;

    private RedHood player;
    public RedHoodAttackState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName)
    {
        this.player = _player;
    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0;

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);
        player.anim.speed = 1.2f;
        float attackDir = AttackDir();

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;
    }

    private float AttackDir()
    {
        float attackDir = player.facingDir;

        if (xInput != 0)
        {
            attackDir = xInput;
        }

        return attackDir;
    }

    public override void Exit()
    {
        base.Exit();

        player.anim.speed = 1f;

        lastTimeAttacked = Time.time;
        comboCounter++;

        player.attackFinish = false;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (player.attackFinish)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}

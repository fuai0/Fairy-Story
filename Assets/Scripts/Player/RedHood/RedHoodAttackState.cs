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
        AudioManager.instance.PlaySfx(0);
        base.Enter();

        // 获取鼠标位置
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if ((mousePosition.x - player.transform.position.x) * player.facingDir < 0)
            player.Flip();

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);
        player.anim.speed = 1.2f;

        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;
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

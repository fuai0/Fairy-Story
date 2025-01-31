using UnityEngine;

public class RedHoodShootState : RedHoodGroundedState
{
    public RedHoodShootState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName, RedHood _player) : base(_playerBase, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 获取鼠标位置
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if ((mousePosition.x - player.transform.position.x) * player.facingDir < 0)
            player.Flip();

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

        player.SetVelocity(0, rb.linearVelocity.y);

        if (player.attackFinish)
            stateMachine.ChangeState(player.idleState);
    }
}

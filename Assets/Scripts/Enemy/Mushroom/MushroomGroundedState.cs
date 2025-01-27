using UnityEngine;

public class MushroomGroundedState : EnemyState
{
    protected Mushroom enemy;

    protected Transform player;
    public MushroomGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 3)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}

using UnityEngine;

public class MushroomHittedState : EnemyState
{
    private Mushroom enemy;
    public MushroomHittedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(enemy.knockbackDirection.x * enemy.hittedDir, enemy.knockbackDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.hitted = false;
        enemy.hittedFinish = false;
    }

    public override void Update()
    {
        base.Update();

        if(enemy.hittedFinish)
            stateMachine.ChangeState(enemy.battleState);
    }
}

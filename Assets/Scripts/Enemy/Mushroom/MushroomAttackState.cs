using UnityEngine;

public class MushroomAttackState : EnemyState
{
    private Mushroom enemy;
    public MushroomAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        int attackIndex = Random.Range(0, 3);

        enemy.anim.SetInteger("AttackIndex", attackIndex);
        enemy.anim.speed = 1.2f;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
        enemy.attackFinish = false;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (enemy.attackFinish)
            enemy.stateMachine.ChangeState(enemy.battleState);
    }
}

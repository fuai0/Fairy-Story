using UnityEngine;

public class MushroomDeadState : EnemyState
{
    private Mushroom enemy;
    public MushroomDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
    }
}

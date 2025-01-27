public class Mushroom : Enemy
{
    #region Ä¢¹½¹Ö×´Ì¬

    public MushroomIdleState idleState { get; private set; }
    public MushroomMoveState moveState { get; private set; }
    public MushroomBattleState battleState { get; private set; }
    public MushroomAttackState attackState { get; private set; }
    public MushroomHittedState hittedState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new MushroomIdleState(this, stateMachine, "Idle", this);
        moveState = new MushroomMoveState(this, stateMachine, "Move", this);
        battleState = new MushroomBattleState(this, stateMachine, "Move", this);
        attackState = new MushroomAttackState(this, stateMachine, "Attack", this);
        hittedState = new MushroomHittedState(this, stateMachine, "Hitted", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if(hitted)
            stateMachine.ChangeState(hittedState);
    }
}

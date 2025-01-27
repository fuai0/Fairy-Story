public class BlueZero : Player
{
    #region À¶Áã×´Ì¬

    public BlueZeroIdleState idleState { get; private set; }
    public BlueZeroMoveState moveState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new BlueZeroIdleState(this, stateMachine, "Idle", this);
        moveState = new BlueZeroMoveState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}

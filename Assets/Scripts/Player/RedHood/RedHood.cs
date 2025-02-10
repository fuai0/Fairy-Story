using UnityEngine;

public class RedHood : Player
{
    [Header("Shoot info")]
    public GameObject arrowPrefab;

    #region С��ñ״̬

    public RedHoodIdleState idleState { get; private set; }
    public RedHoodMoveState moveState { get; private set; }

    public RedHoodJumpState jumpState { get; private set; }
    public RedHoodAirState airState { get; private set; }

    public RedHoodWallSlideState wallSlide { get; private set; }
    public RedHoodWallJumpState wallJump { get; private set; }

    public RedHoodAttackState attackState { get; private set; }
    public RedHoodShootState shootState { get; private set; }
    public RedHoodHittedState hittedState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new RedHoodIdleState(this, stateMachine, "Idle", this);
        moveState = new RedHoodMoveState(this, stateMachine, "Move", this);

        jumpState = new RedHoodJumpState(this, stateMachine, "Air", this);
        airState = new RedHoodAirState(this, stateMachine, "Air", this);

        wallSlide = new RedHoodWallSlideState(this, stateMachine, "WallSlide", this);
        wallJump = new RedHoodWallJumpState(this, stateMachine, "Air", this);

        attackState = new RedHoodAttackState(this, stateMachine, "Attack", this);
        shootState = new RedHoodShootState(this, stateMachine, "Shoot", this);
        hittedState = new RedHoodHittedState(this, stateMachine, "Hitted", this);


        canShoot = false;
        canWallSlide = false;
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (hitted)
            stateMachine.ChangeState(hittedState);

        if (Input.GetKeyDown(KeyCode.E))
            UseConsumable();
    }

    public void CreateArrow()
    {
        Instantiate(arrowPrefab, transform.position, transform.rotation);
    }

    private void UseConsumable()
    {
        ItemData_Equipment consumableData = Inventory.instance.GetEquip(EquipmentType.Consumable);

        if (consumableData == null)
            return;
        else
            consumableData.Effect(null, PlayerManager.instance.player.stats as PlayerStats);
    }
}

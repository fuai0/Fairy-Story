using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    public float moveSpeed;

    [Header("Jump info")]
    public float jumpForce;

    [Header("Attack details")]
    public Vector2[] attackMovement;

    public bool canWallSlide;
    public bool canShoot;

    public PlayerStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }
}

using UnityEngine;

public class Enemy : Entity
{
    [Header("Knockback info")]
    public Vector2 knockbackDirection;

    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;

    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    public float battleTime;
    public LayerMask whatIsPlayer;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public virtual RaycastHit2D IsPlayerDetected()
    {
        RaycastHit2D isPlayer = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 20, whatIsPlayer | whatIsWall);

        if (isPlayer.collider != null)
        {
            if (((1 << isPlayer.collider.gameObject.layer) & whatIsPlayer) != 0)
            {
                return isPlayer;
            }
        }
        return new RaycastHit2D();
    }
}

using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Attack info")]
    public float attackRadius;
    public Vector2 attackPosition {  get; private set; }

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Vector2 groundCheckBox;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected Vector2 wallCheckBox;

    [HideInInspector] public bool attackFinish;
    [HideInInspector] public int hittedDir;
    [HideInInspector] public bool hitted;
    [HideInInspector] public bool hittedFinish;

    [HideInInspector] public int facingDir { get; private set; } = 1;
    [HideInInspector] public CharacterStats stats;

    #region 组件Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CapsuleCollider2D cd { get; private set; }

    #endregion

    public System.Action onFlipped;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();

        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
        attackPosition = new Vector2(transform.position.x + attackRadius * facingDir, transform.position.y);
    }
    
    #region 速度设置

    // 设置速度为0
    public virtual void SetZeroVelocity()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }


    // 设置速度
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    #endregion

    #region 翻转

    public virtual void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);

        if (onFlipped != null)
            onFlipped();
    }

    public virtual void FlipController(float _x)
    {
        if (_x < 0 && facingDir == 1)
            Flip();
        else if (_x > 0 && facingDir == -1)
            Flip();
    }

    #endregion

    #region 碰撞检测

    // 检测接地
    public virtual bool IsGroundDetected() => Physics2D.BoxCast(groundCheck.position, groundCheckBox,0f,Vector2.down, groundCheckDistance, whatIsGround);

    //检测接墙
    public virtual bool IsWallDetected() => Physics2D.BoxCast(wallCheck.position, wallCheckBox, 0f, Vector2.right, wallCheckDistance * facingDir, whatIsWall);

    //画线检测碰撞
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position + Vector3.down * (groundCheckDistance / 2), groundCheckBox);
        Gizmos.DrawWireCube(wallCheck.position + Vector3.right * (wallCheckDistance / 2) * facingDir, wallCheckBox);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + attackRadius * facingDir, transform.position.y), attackRadius);
    }

    #endregion

    public void AttackFinish() => attackFinish = true;

    public void Hitted() => hitted = true;

    public void HittedFinish() => hittedFinish = true;
}

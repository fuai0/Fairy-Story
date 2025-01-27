using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player playerBase;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    protected string animBoolName;

    protected float stateTimer;

    public PlayerState(Player _playerBase, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.playerBase = _playerBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        playerBase.anim.SetBool(animBoolName, true);
        rb = playerBase.rb;
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        playerBase.anim.SetFloat("yVelocity", rb.linearVelocity.y);

        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        playerBase.anim.SetBool(animBoolName, false);
    }
}

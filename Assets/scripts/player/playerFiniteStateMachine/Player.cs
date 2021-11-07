using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public PlayerStateMachine stateMachine { get; private set; }
  public PlayerIdleState idleState { get; private set; }
  public PlayerMoveState moveState { get; private set; }
  public PlayerJumpState jumpState { get; private set; }
  public PlayerInAirState inAirState { get; private set; }
  public PlayerLandState landState { get; private set; }

  public Animator animator { get; private set; }
  public PlayerInputHandler inputHandler { get; private set; }
  public Rigidbody2D rBody { get; private set; }
  public Vector2 currentVelocity { get; private set; }
  public int facingDirection { get; private set; }

  [SerializeField]
  private SO_PlayerData playerData;

  [SerializeField]
  private Transform groundCheck;

  [HideInInspector]
  public MovementSM movementSm;

  private BoxCollider2D bCollider;
  private Vector2 velocityWorkspace;

  private void Awake() {
    this.initializeStates();
  }

  private void Start() {
    this.animator = GetComponent<Animator>();
    this.inputHandler = GetComponent<PlayerInputHandler>();
    this.rBody = GetComponent<Rigidbody2D>();
    this.bCollider = GetComponent<BoxCollider2D>();
    this.stateMachine.Initialize(idleState);
    this.facingDirection = 1;
  }

  private void initializeStates() {
    this.stateMachine = new PlayerStateMachine();
    this.idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
    this.moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
    this.jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
    this.inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
    this.landState = new PlayerLandState(this, stateMachine, playerData, "land");
  }

  private void Update() {
    currentVelocity = rBody.velocity;
    stateMachine.currentState.LogicUpdate();
  }

  private void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  public void SetVelocityX(float velocity) {
    velocityWorkspace.Set(velocity, currentVelocity.y);
    rBody.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }

  public void SetVelocityY(float velocity) {
    velocityWorkspace.Set(currentVelocity.x, velocity);
    rBody.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }

  public bool CheckIfGrounded() {
    return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
  }

  public void CheckIfShouldFlip(int xInput) {
    if (xInput != 0 && xInput != facingDirection) {
      Flip();
    }
  }

  private void AnimationStarted() => stateMachine.currentState.AnimationStarted();
  private void AnimationFinished() => stateMachine.currentState.AnimationFinished();

  private void Flip() {
    facingDirection *= -1;
    transform.Rotate(0.0f, 180.0f, 0.0f);
  }
}

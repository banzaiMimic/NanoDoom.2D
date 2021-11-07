using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public Animator animator { get; private set; }
  public PlayerStateMachine stateMachine { get; private set; }
  public PlayerIdleState idleState { get; private set; }
  public PlayerMoveState moveState { get; private set; }
  public PlayerInputHandler inputHandler { get; private set; }
  public Rigidbody2D rBody { get; private set; }
  public Vector2 currentVelocity { get; private set; }
  public int facingDirection { get; private set; }

  [SerializeField]
  private SO_PlayerData playerData;

  [SerializeField] 
  private LayerMask jumpableGround;

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
    stateMachine = new PlayerStateMachine();
    idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
    moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
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

  public void CheckIfShouldFlip(int xInput) {
    if (xInput != 0 && xInput != facingDirection) {
      Flip();
    }
  }

  private void Flip() {
    facingDirection *= -1;
    transform.Rotate(0.0f, 180.0f, 0.0f);
  }
}
